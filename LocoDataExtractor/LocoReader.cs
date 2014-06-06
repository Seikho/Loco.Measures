﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace LocoDataExtractor
{
    public class LocoReader
    {
        public string Filename = "";
        public LocoMeasurer Measurer;
        protected int SampleFreq = 1; // Samples per second
        public string OutputFile = "";
        protected List<string>[] MetricOutput = new List<string>[5];
        public LocoReader(string file, int sampleFreq = 1)
        {
            SampleFreq = sampleFreq;
            if (!File.Exists(file)) throw new IOException("Unable to find file '" + file + "'");
            Filename = file;
            OutputFile = NewFile("RData", true);
        }

        public string GenerateFixedFile(bool delete = true)
        {
            string tempName = NewFile((delete?"Temp":"Fixed"));
            bool readFirst = false;
            var pred = new DateTime();
            var succ = new DateTime();
            string predByte = ""; // previous sample byte
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

        public void ImmobileTime(int binSize = 1) // Default bin size: 1 minutes
        {
            Measurer = new LocoIMT(Filename, binSize * (60 * SampleFreq), SampleFreq); 
            Measurer.Extract();
            MetricOutput[1] = Measurer.Output;
        }

        public void HorizontalMovement(int binSize = 5) // Default bin size: 5 minutes
        {
            Measurer = new LocoHM(Filename, binSize * (60 * SampleFreq), SampleFreq); 
            Measurer.Extract();
            MetricOutput[0] = Measurer.Output;
        }

        public void VerticalMovement(int binSize = 5)
        {
            Measurer = new LocoVM(Filename, binSize * (60 * SampleFreq), SampleFreq);
            Measurer.Extract();
            MetricOutput[2] = Measurer.Output;
        }

        public void CenterVertical(int binSize = 5)
        {
            Measurer = new LocoCV(Filename, binSize * (60 * SampleFreq), SampleFreq);
            Measurer.Extract();
            MetricOutput[4] = Measurer.Output;
        }

        public void VerticalTime(int binSize = 5)
        {
            Measurer = new LocoVT(Filename, binSize * (60 * SampleFreq), SampleFreq);
            Measurer.Extract();
            MetricOutput[3] = Measurer.Output;
        }

        public bool GenRData(string ratId, string sessNo, string drug)
        {
            int allLen = MetricOutput[0].Count;
            if (MetricOutput.Any(list => list.Count != allLen))
            {
                return false;
            }
            using (var sw = new StreamWriter(OutputFile))
            {
                sw.WriteLine("Rat ID,Bin#,Drug,Session#,HM,IMT,VM,VT,CV");
                for (int count = 0; count < allLen; count++)
                {
                    string newLine = ratId + "," + (count + 1) + "," + drug + "," + sessNo + "," + MetricOutput[0][count] + "," + MetricOutput[1][count] + "," + MetricOutput[2][count] + "," + MetricOutput[3][count] + "," + MetricOutput[4][count];
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
