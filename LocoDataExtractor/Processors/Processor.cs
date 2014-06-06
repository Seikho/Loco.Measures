namespace LocoDataExtractor.Processors
{
    public abstract class Processor
    {
        public string FileLocation { get; set; }
        protected Processor(string fileLocation)
        {
            FileLocation = fileLocation;
        }

        public abstract void Process();
    }
}
