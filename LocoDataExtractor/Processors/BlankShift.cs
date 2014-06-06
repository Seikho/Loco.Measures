namespace LocoDataExtractor.Processors
{
    public class BlankShift : Processor
    {
        /// <summary>
        /// The Blank Shift file processor will assume that the micro controller accurately samples at 2Hz, but messages transmitted via USB can be delayed.
        /// Therefore every second of sampling should have 2 samples. Blanks are filled by the next available sample.
        /// </summary>
        /// <param name="fileLocation"></param>
        /// <param name="newFile"></param>
        public BlankShift(string fileLocation, string newFile) : base(fileLocation, newFile)
        {
        }

        protected override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
