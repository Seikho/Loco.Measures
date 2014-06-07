using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using LocoDataExtractor.Metrics;
using LocoDataExtractor.Processors;

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

        public string GenerateFixedFile(int fileProcessor, bool delete = true)
        {
            var tempName = NewFile((delete?"Temp":"Fixed"));
            switch (fileProcessor)
            {
                case 0:
                    new BlankFiller(Filename, tempName).Process();
                    break;
                case 1:
                    new BlankShift(Filename, tempName).Process();
                    break;
            }
            return tempName;
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
