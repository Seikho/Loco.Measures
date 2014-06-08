using System.Globalization;

namespace LocoDataExtractor.Metrics
{
    public class VerticalMovement : Metric // Vertical movement
    {
        public VerticalMovement(string file, int binSize, int sampleFreq = 2) : base(file, binSize, sampleFreq)
        {
            NewFile("VM");
            Writer.WriteLine("Vertical Movement");
            Writer.WriteLine("Bin#\tVM(breaks)\tBin timespan\t(VM = (RearCount per Bin), Sampling Frequency/sec: " + SampleFreq + ", Bin size: " + BinSize + " samples");
        }

        public override void Execute()
        {
            if (Pred.Length == 0) Pred = "00000000"; // Pred == null on first iteration
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
