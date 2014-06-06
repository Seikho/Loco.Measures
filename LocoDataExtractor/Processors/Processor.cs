using System;
using System.IO;

namespace LocoDataExtractor.Processors
{
    public abstract class Processor
    {
        public string FileLocation { get; set; }
        public string NewFile { get; set; }
        protected StreamWriter Target { get; set; }
        protected StreamReader Original { get; set; }
        protected Processor(string fileLocation, string newFile)
        {
            FileLocation = fileLocation;
            NewFile = newFile;
            Target = new StreamWriter(NewFile);
            Original = new StreamReader(FileLocation);
        }

        protected DateTime GetTime(string time)
        {
            var split = time.Split(' ');
            return Convert.ToDateTime(split[0] + " " + split[1] + " " + split[2]);
        }

        public void Process()
        {
            Execute();
            Target.Dispose();
            Original.Dispose();
        }

        protected abstract void Execute();
    }
}
