using System.Globalization;

namespace LocoDataExtractor.Metrics
{
    public class HorizontalMovement : Metric // Horizontal movement
    {
        public HorizontalMovement(string targetFile, int binSize) : base(targetFile, binSize)
        {
            Writer.WriteLine("Horizontal Movement");
            Writer.WriteLine("Bin#\tHM(breaks)\tBin timespan\t(HM = (MovementCount per Bin), Sampling Frequency/sec: 2, Bin size: " + BinSize + " samples");
        }

        public override void Execute()
        {
            if (PredNoRear.Length == 0) PredNoRear = SuccNoRear; // bugfix: first reading will always count as IMT, not HM.
            if (!PredNoRear.Equals(SuccNoRear)) Counter++;
            if (!BinChange()) return;
            Output.Add(Counter.ToString(CultureInfo.InvariantCulture));
            WriteLine(Counter);
            BinCount++;
            Counter = 0;
            MinCount = 0;
        }
    }
}
