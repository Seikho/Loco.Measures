﻿using LocoDataExtractor.Metrics.Strategy;

namespace LocoDataExtractor.Metrics
{
    public class HorizontalMovement : Metric // Horizontal movement
    {
        public HorizontalMovement(string targetFile, int samplesPerMinute, int minutesPerBin)
            : base(targetFile, samplesPerMinute, minutesPerBin)
        {
            Strategy = new CountStrategy();
            Writer.WriteLine("Horizontal Movement");
            Writer.WriteLine("Bin#\tHM(breaks)\tBin timespan\t(HM = (MovementCount per Bin), Samples per minute: " + SamplesPerMinute + ", Minutes per bin: " + MinutesPerBin);
        }

        public override void Execute()
        {
            if (PredNoRear.Length == 0) PredNoRear = SuccNoRear; // bugfix: first reading will always count as IMT, not HM.
            if (!PredNoRear.Equals(SuccNoRear)) Counter++;
        }
    }
}
