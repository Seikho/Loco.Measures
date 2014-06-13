using System.Globalization;

namespace LocoDataExtractor.Metrics
{
    public class ImmobileTime : Metric // Immobile Time
    {
        public ImmobileTime(string targetFile, int samplesPerMinute, int minutesPerBin)
            : base(targetFile, samplesPerMinute, minutesPerBin)
        {
            Writer.WriteLine("Immobile Time");
            Writer.WriteLine("Bin#\tIMT(sec)\tBin timespan\t(IMT = (IdleReadings / SamplingTime), Samples per minute: " + SamplesPerMinute + ", Minutes per bin: " + MinutesPerBin);
        }

        public override void Execute()
        {
            // bugfix: first reading will always count as IMT, not HM.
            if (PredNoRear.Length == 0) PredNoRear = SuccNoRear; 
            if (PredNoRear.Equals(SuccNoRear)) Counter++;
            if (!BinChange()) return;
            double div = Counter;
            Output.Add((div/2).ToString(CultureInfo.InvariantCulture));
            WriteLine((div/2));
            BinCount++;
            Counter = 0;
            MinCount = 0;
        }
    }
}
