using System;
using System.Collections.Generic;
using System.IO;

namespace LocoDataExtractor.Metrics
{
    public abstract class Metric
    {
        public int BinSize;
        public int Counter;
        public int MinCount;
        public int BinCount;
        public string Pred { get; set; } // Includes rearing value
        public string PredNoRear; // Does not inclue rearing value
        public string Succ; // Includes rearing value
        public string SuccNoRear; // Does not inclue rearing value
        public string File;
        public int SampleFreq;
        public List<string> Contents;
        public List<string> Output = new List<string>();
        public string OutputFile;
        public StreamWriter Writer;

        protected Metric(string file, int binSize, int sampleFreq = 2)
        {
            File = file;
            ReadFile();
            SampleFreq = sampleFreq;
            BinSize = binSize;
            MinCount = 0;
            Counter = 0;
            Pred = "";
            Succ = "";
            PredNoRear = "";
            SuccNoRear = "";
            BinCount = 1;
        }

        public void Process()
        {
            foreach (var read in Contents)
            {
                UpdateValues(read);
                Execute();
            }
        }

        abstract public void Execute();

        protected DateTime SampleTime(string input)
        {
            var split = input.Split(' ');
            return Convert.ToDateTime(split[0] + " " + split[1]);
        }

        protected string GetCoord(string input)
        {
            var split = input.Split(' ');
            var coord = split[split.Length - 1];
            return coord;
        }

        protected string GetNonRearCoord(string input)
        {
            var split = input.Split(' ');
            var coord = split[split.Length - 1].Substring(0, 7);
            return coord;
        }

        protected void NewFile(string extension)
        {
            var path = Path.GetDirectoryName(File) + "\\";
            var noext = Path.GetFileNameWithoutExtension(File);
            var count = 1;
            while (System.IO.File.Exists(path + noext + "_" + count + "_" + extension + ".txt")) count++;
            OutputFile = path + noext + "_" + count + "_" + extension + ".txt";
            Writer = new StreamWriter(OutputFile);
        }

        protected bool BinChange()
        {
            return MinCount == BinSize;
        }

        protected void UpdateValues(string input)
        {
            if (BinChange())
            {
                MinCount = 0;
            }
            MinCount++; // when it this figure reaches binSize * 60, new bin is expected.
            //this.st = sampleTime(input);
            Pred = Succ;
            PredNoRear = SuccNoRear;
            Succ = GetCoord(input);
            SuccNoRear = GetNonRearCoord(input);
        }

        protected void ReadFile()
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

        protected void WriteLine(double val)
        {
            Writer.WriteLine(BinCount + "\t" + val + "\t\t(Samples: " + MinCount + ")");
        }
    }
}
