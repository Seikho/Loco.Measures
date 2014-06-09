using System.Globalization;

namespace LocoDataExtractor.Metrics
{
    public class VerticalMovement : Metric // Vertical movement
    {
        public VerticalMovement(string targetFile, int binSize, int sampleFreq = 2) : base(targetFile, binSize, sampleFreq)
        {
            Writer.WriteLine("Vertical Movement");
            Writer.WriteLine("Bin#\tVM(breaks)\tBin timespan\t(VM = (RearCount per Bin), Sampling Frequency/sec: " + SampleFreq + ", Bin size: " + BinSize + " samples");
        }

        public override void Execute()
        {
            var predRear = Pred.Substring(Pred.Length - 1, 1);
            var succRear = Succ.Substring(Succ.Length - 1, 1);
            if ((succRear.Equals("1")) && (predRear.Equals("0"))) Counter++;
            if (!BinChange()) return;
            Output.Add(Counter.ToString(CultureInfo.InvariantCulture));
            WriteLine(Counter);
            BinCount++;
            Counter = 0;
            MinCount = 0;
        }
    }
}
