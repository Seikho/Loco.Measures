using System;

namespace LocoDataExtractor.Processors
{
    public class CustomBlankShift : Processor
    {
        protected int MinuteSamples = 0;
        protected DateTime Curr;
        protected int MaxSamplesPerMinute;


        public CustomBlankShift(int maxSamples, string fileLocation, string newFile) : base(fileLocation, newFile)
        {
            MaxSamplesPerMinute = maxSamples;
        }

        public CustomBlankShift(string fileLocation, string newFile) : base(fileLocation, newFile)
        {
        }

        protected override void Execute()
        {
            if (!ReadFirst)
            {
                GetInitialValues();
                return;
            }
            WriteLine(Curr, ReadByte);
            MinuteSamples++;
            if (MinuteSamples != MaxSamplesPerMinute) return;
            MinuteSamples = 0;
            Curr = Curr.AddMinutes(1);
        }

        private void GetInitialValues()
        {
            ReadFirst = true;
            PredByte = ReadByte;
            Curr = ReadTime.AddSeconds(-1*ReadTime.Second);
            Pred = ReadTime;
            MinuteSamples = 1;
            WriteLine(Curr, ReadByte);
        }
    }
}
