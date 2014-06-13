﻿using System.Globalization;

namespace LocoDataExtractor.Metrics
{
    public class MovementWhileVertical : Metric
    {
        public MovementWhileVertical(string targetFile, int samplesPerMinute, int minutesPerBin)
            : base(targetFile, samplesPerMinute, minutesPerBin)
        {
            Writer.WriteLine("Vertical Time");
            Writer.WriteLine("Bin#\tVT(secs)\tBin timespan\t(MWV = (VerticalCount / SampleFreq), Samples per minute: " + SamplesPerMinute + ", Minutes per bin: " + MinutesPerBin);
        }

        public override void Execute()
        {
            var predRear = Pred.Substring(Pred.Length-1, 1);
            var succRear = Succ.Substring(Succ.Length - 1, 1);
            var predHorz = Pred.Substring(0, Pred.Length - 1);
            var succHorz = Succ.Substring(0, Succ.Length - 1);
            if (predRear.Equals("1") && succRear.Equals("1") && !predHorz.Equals(succHorz)) Counter++;
            if (!BinChange()) return;
            Output.Add(Counter.ToString(CultureInfo.InvariantCulture));
            WriteLine(Counter);
            BinCount++;
            Counter = 0;
        }
    }
}
