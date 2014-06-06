using System;
using System.Collections.Generic;
using System.IO;

namespace LocoDataExtractor
{
    public abstract class LocoMeasurer
    {
        public int BinSize;
        public int Counter;
        //public int StartMin;
        //public int StartSec;
        public int MinCount;
        public int BinCount;
        public string Pred { get; set; } // Includes rearing value
        public string PredNR; // Does not inclue rearing value
        public string Succ; // Includes rearing value
        public string SuccNR; // Does not inclue rearing value
        public string File;
        public int SampleFreq;
        public List<string> Contents;
        public List<string> Output = new List<string>();
        public string OutputFile;

        abstract public void Extract();

        public DateTime SampleTime(string input)
        {
            string[] split = input.Split(' ');
            return Convert.ToDateTime(split[0] + " " + split[1]);
        }

        public string GetCoord(string input)
        {
            string[] split = input.Split(' ');
            string coord = split[split.Length - 1];
            return coord;
        }

        public string GetNRCoord(string input)
        {
            string[] split = input.Split(' ');
            string coord = split[split.Length - 1].Substring(0, 7);
            return coord;
        }

        public string NewFile(string extension)
        {
            string path = Path.GetDirectoryName(File) + "\\";
            string noext = Path.GetFileNameWithoutExtension(File);
            int count = 1;
            while (System.IO.File.Exists(path + noext + "_" + count + "_" + extension + ".txt")) count++;
            return path + noext + "_" + count + "_" + extension + ".txt";
        }

        public bool BinChange()
        {
            if (MinCount == BinSize) return true;
            return false;
        }

        public void UpdateValues(string input)
        {
            if (BinChange())
            {
                MinCount = 0;
            }
            MinCount++; // when it this figure reaches binSize * 60, new bin is expected.
            //this.st = sampleTime(input);
            Pred = Succ;
            PredNR = SuccNR;
            Succ = GetCoord(input);
            SuccNR = GetNRCoord(input);
        }

        public void ReadFile()
        {
            Contents = new List<string>();
            using (var sr = new StreamReader(File))
            {
                string read;
                while ((read = sr.ReadLine()) != null)
                {
                    if (read.Length > 10) Contents.Add(read);
                }
            }
        }

        public void WriteLine(StreamWriter sw, double val)
        {
            sw.WriteLine(BinCount + "\t" + val + "\t\t(Samples: " + MinCount + ")");
        }
    }
}
