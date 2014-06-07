using System;
using System.IO;

namespace LocoDataExtractor.Processors
{
    public abstract class Processor
    {
        protected StreamWriter Target { get; set; }
        protected StreamReader Original { get; set; }
        protected DateTime Pred = new DateTime();
        protected DateTime Succ = new DateTime();
        protected string PredByte = "";
        protected bool ReadFirst = false;
        protected string ReadLine;
        protected DateTime ReadTime;
        protected string ReadEnclosure;
        protected string ReadByte;

        protected Processor(string fileLocation, string newFile)
        {
            Target = new StreamWriter(newFile);
            Original = new StreamReader(fileLocation);
        }

        protected DateTime GetTime(string time)
        {
            var split = time.Split(' ');
            return Convert.ToDateTime(split[0] + " " + split[1] + " " + split[2]);
        }

        public void Process()
        {
            while ((ReadLine = Original.ReadLine()) != null)
            {
                if (ReadLine.Length < 5) continue;
                SetReadValues();
                Execute();
            }
            Target.Dispose();
            Original.Dispose();
        }

        private void SetReadValues()
        {
            var split = ReadLine.Split(' ');
            ReadTime = GetTime(ReadLine);
            ReadEnclosure = split[3];
            ReadByte = split[4];
        }

        protected abstract void Execute();
    }
}
