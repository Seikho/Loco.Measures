using System.Globalization;

namespace LocoDataExtractor.Metrics
{
    public class VerticalTime : Metric // Vertical time
    {
        public VerticalTime(string file, int binSize, int sampleFreq = 2) : base(file, binSize, sampleFreq)
        {
            NewFile("VT");
            Writer.WriteLine("Vertical Time");
            Writer.WriteLine("Bin#\tVT(secs)\tBin timespan\t(VT = (VerticalCount / SampleFreq), Sampling Frequency/sec: " + SampleFreq + ", Bin size: " + BinSize + " samples");
        }

        public override void Execute()
        {
            var succRear = Succ.Substring(Succ.Length - 1, 1);
            if (succRear.Equals("1")) Counter++;
            if (!BinChange()) return;
            double div = Counter;
            Output.Add((div/SampleFreq).ToString(CultureInfo.InvariantCulture));
            WriteLine((div/SampleFreq));
            BinCount++;
            Counter = 0;
            MinCount = 0;
        }
    }
}
