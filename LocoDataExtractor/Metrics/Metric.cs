using System;
using System.Collections.Generic;
using System.IO;

namespace LocoDataExtractor.Metrics
{
    public abstract class Metric
    {
        public int SamplesPerMinute { get; set; }
        public int Counter { get; set; }
        public int MinCount { get; set; }
        public int BinCount { get; set; }
        public int MinutesPerBin { get; set; }
        public string Pred { get; set; } // Includes rearing value
        public string PredNoRear { get; set; } // Does not inclue rearing value
        public string Succ { get; set; }// Includes rearing value
        public string SuccNoRear { get; set; } // Does not inclue rearing value
        public string TargetFile { get; set; }
        public List<string> Contents = new List<string>();
        public List<string> Output = new List<string>();
        public string OutputFile { get; set; }
        public StreamWriter Writer;

        protected Metric(string targetFile, int samplesPerMinute, int minutesPerBin = 5)
        {
            MinutesPerBin = minutesPerBin;
            TargetFile = targetFile;
            ReadFile();
            SamplesPerMinute = samplesPerMinute;
            MinCount = 0;
            Counter = 0;
            Pred = "00000000";
            Succ = "00000000";
            PredNoRear = "";
            SuccNoRear = "";
            BinCount = 1;
            NewFile(this.GetClassName());
        }

        public void Process()
        {
            foreach (var read in Contents)
            {
                UpdateValues(read);
                Execute();
            }
            Writer.Dispose();
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
            var path = Path.GetDirectoryName(TargetFile) + "\\";
            var noext = Path.GetFileNameWithoutExtension(TargetFile);
            var count = 1;
            while (File.Exists(path + noext + "_" + count + "_" + extension + ".txt")) count++;
            OutputFile = path + noext + "_" + count + "_" + extension + ".txt";
            Writer = new StreamWriter(OutputFile);
        }

        protected bool BinChange()
        {
            return MinCount == SamplesPerMinute*MinutesPerBin;
        }

        protected void UpdateValues(string input)
        {
            if (BinChange()) MinCount = 0;
            MinCount++; // when it this figure reaches binSize * 60, new bin is expected.
            Pred = Succ;
            PredNoRear = SuccNoRear;
            Succ = GetCoord(input);
            SuccNoRear = GetNonRearCoord(input);
        }

        protected void ReadFile()
        {
            Contents = new List<string>();
            using (var sr = new StreamReader(TargetFile))
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
