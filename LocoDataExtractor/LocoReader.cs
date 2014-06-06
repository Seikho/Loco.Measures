using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using LocoDataExtractor.Metrics;

namespace LocoDataExtractor
{
    public class LocoReader
    {
        public string Filename = "";
        public Metric Measurer;
        protected int SampleFreq = 1; // Samples per second
        public string OutputFile = "";
        protected List<IEnumerable<string>> MetricOutput = new List<IEnumerable<string>>();
        public LocoReader(string file, int sampleFreq = 1)
        {
            SampleFreq = sampleFreq;
            if (!File.Exists(file)) throw new IOException("Unable to find file '" + file + "'");
            Filename = file;
            OutputFile = NewFile("RData", true);
        }

        public string GenerateFixedFile(bool delete = true)
        {
            var tempName = NewFile((delete?"Temp":"Fixed"));
            var readFirst = false;
            var pred = new DateTime();
            var succ = new DateTime();
            var predByte = ""; // previous sample byte
            using (var sw = new StreamWriter(tempName))
            {
                using (var sr = new StreamReader(Filename))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Length < 5) continue;
                        var split = line.Split(' ');
                        DateTime curr; // current sample time
                        if (!readFirst) // run once.. first time reading a line, generate our initial values
                        {
                            readFirst = true;
                            predByte = split[4];
                            curr = GetTime(line);
                            pred = curr.AddSeconds(-1);
                            succ = curr;
                        }
                        else
                        {
                            curr = GetTime(line);
                        }
                        if (pred != curr)
                        {
                            if (succ != curr) // we're missing a second or two. fill in the gap(s).
                            {
                                while (succ != curr)
                                {
                                    sw.WriteLine("{0} {1} {2} {3}", succ.ToShortDateString(), succ.ToLongTimeString(), split[3], predByte);
                                    succ = succ.AddSeconds(1);
                                }
                            }
                            // we do this regardless.
                            succ = curr.AddSeconds(1);
                            pred = curr;
                            predByte = split[4];
                            sw.WriteLine(line);                            
                        }
                    }
                }
            }
            return tempName;
        }

        private DateTime GetTime(string time)
        {
            var split = time.Split(' ');
            return Convert.ToDateTime(split[0] + " " + split[1] + " " + split[2]);
        }

        public void GenerateMetrics(int binSize = 1)
        {
            GenerateMetric(new HorizontalMovement(Filename, binSize * (60 * SampleFreq), SampleFreq), binSize);
            GenerateMetric(new ImmobileTime(Filename, binSize * (60 * SampleFreq), SampleFreq), binSize);
            GenerateMetric(new VerticalMovement(Filename, binSize * (60 * SampleFreq), SampleFreq), binSize);
            GenerateMetric(new VerticalTime(Filename, binSize * (60 * SampleFreq), SampleFreq), binSize);
            GenerateMetric(new CenterVertical(Filename, binSize * (60 * SampleFreq), SampleFreq), binSize);
        }

        public void GenerateMetric(Metric metric, int binSize)
        {
            metric.Extract();
            MetricOutput.Add(metric.Output);
        }

        public bool GenRData(string ratId, string sessNo, string drug)
        {
            var allLen = MetricOutput[0].Count();
            if (MetricOutput.Any(list => list.Count() != allLen))
            {
                return false;
            }
            using (var sw = new StreamWriter(OutputFile))
            {
                sw.WriteLine("Rat ID,Bin#,Drug,Session#,HM,IMT,VM,VT,CV");
                for (var count = 0; count < allLen; count++)
                {
                    var newLine = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", 
                        ratId,
                        count+1,
                        drug,
                        sessNo,
                        MetricOutput[0].ElementAt(count),
                        MetricOutput[1].ElementAt(count),
                        MetricOutput[2].ElementAt(count),
                        MetricOutput[3].ElementAt(count),
                        MetricOutput[4].ElementAt(count));
                    sw.WriteLine(newLine);
                }
            }
            return true;
        }

        private string NewFile(string extension, bool asCsv = false)
        {
            var path = Path.GetDirectoryName(Filename) + "\\";
            var noext = Path.GetFileNameWithoutExtension(Filename);
            var count = 1;
            while (File.Exists(path + noext + "_" + count + "_" + extension + (asCsv?".csv":".txt"))) count++;
            return path + noext + "_" + count + "_" + extension + (asCsv?".csv":".txt");
        }
    }
}
