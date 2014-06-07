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
        protected DateTime Curr;
        protected string PredByte = "";
        protected bool ReadFirst = false;
        protected string Line;
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
            while ((Line = Original.ReadLine()) != null)
            {
                Execute();
            }
            Target.Dispose();
            Original.Dispose();
        }

        protected abstract void Execute();
    }
}
