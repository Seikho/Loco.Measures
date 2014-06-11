using System.Globalization;

namespace LocoDataExtractor.Metrics
{
    public class VerticalTime : Metric // Vertical time
    {
        public VerticalTime(string targetFile, int binSize) : base(targetFile, binSize)
        {
            Writer.WriteLine("Vertical Time");
            Writer.WriteLine("Bin#\tVT(secs)\tBin timespan\t(VT = (VerticalCount / SampleFreq), Sampling Frequency/sec: 2, Bin size: " + BinSize + " samples");
        }

        public override void Execute()
        {
            var succRear = Succ.Substring(Succ.Length - 1, 1);
            if (succRear.Equals("1")) Counter++;
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
