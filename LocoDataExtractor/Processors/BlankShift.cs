﻿using System;

namespace LocoDataExtractor.Processors
{
    /// <summary>
    /// The Blank Shift file processor will assume that the micro controller accurately samples at 2Hz, but messages transmitted via USB can be delayed.
    /// Therefore every second of sampling should have 2 samples. Blanks are filled by the next available sample.
    /// </summary>
    public class BlankShift : Processor
    {
        protected int SecondSamples = 0;
        protected DateTime Curr;
        public BlankShift(string fileLocation, string newFile) : base(fileLocation, newFile)
        {
        }

        protected override void Execute()
        {
            if (!ReadFirst) GetInitialValues();

        }

        private void GetInitialValues()
        {
            PredByte = ReadByte;
            Curr = ReadTime;
            Pred = ReadTime;
            SecondSamples = 1;

        }
    }
}
