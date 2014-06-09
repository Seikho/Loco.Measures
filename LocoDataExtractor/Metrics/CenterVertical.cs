using System.Globalization;

namespace LocoDataExtractor.Metrics
{
    public class CenterVertical : Metric // Center-Vertical movement
    {
        public CenterVertical(string targetFile, int binSize, int sampleFreq = 2) : base(targetFile, binSize, sampleFreq)
        {
            Writer.WriteLine("Center-Rearing Movement");
            Writer.WriteLine("Bin#\tVM(breaks)\tBin timespan\t(CV = (Central RearCount per Bin), Sampling Frequency/sec: " + SampleFreq + ", Bin size: " + BinSize + " samples");
        }

        public override void Execute()
        {
            if (Pred.Length == 0) Pred = "00000000"; // Pred == null on first iteration
            var succRear = Succ.Substring(Succ.Length - 1, 1);
            var predRear = Pred.Substring(Pred.Length - 1, 1);
            var succX1 = Succ.Substring(1, 1);
            var succX2 = Succ.Substring(2, 1);
            var succY2 = Succ.Substring(5, 1);
            if (((succRear.Equals("1")) && (predRear.Equals("0"))) && (succY2.Equals("1")) &&
                ((succX1.Equals("1")) || (succX2.Equals("1")))) Counter++;
            if (!BinChange()) return;
            Output.Add(Counter.ToString(CultureInfo.InvariantCulture));
            WriteLine(Counter);
            BinCount++;
            Counter = 0;
            MinCount = 0;
        }
    }
}
