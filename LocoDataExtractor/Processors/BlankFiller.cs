﻿namespace LocoDataExtractor.Processors
{
    public class BlankFiller : Processor
    {
        public BlankFiller(string fileLocation, string newFile) : base(fileLocation, newFile)
        {
        }

        protected override void Execute()
        {
            if (!ReadFirst) GenerateInitialValues();
            if (Pred == ReadTime) return;
            // we're missing a second or two. fill in the gap(s).
            if (Succ != ReadTime) FillGapWithLastByte();
            Succ = ReadTime.AddSeconds(1);
            Pred = ReadTime;
            PredByte = ReadByte;
            Target.WriteLine(ReadLine);
        }

        private void FillGapWithLastByte()
        {
            while (Succ != ReadTime)
            {
                WriteLine(Succ, PredByte);
                Succ = Succ.AddSeconds(1);
            }
        }

        private void GenerateInitialValues()
        {
            ReadFirst = true;
            PredByte = ReadByte;
            Pred = ReadTime.AddSeconds(-1);
            Succ = ReadTime;
        }
    }
}
