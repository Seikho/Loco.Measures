using System.Globalization;

namespace LocoDataExtractor.Metrics
{
    public class VerticalTime : Metric // Vertical time
    {
        public VerticalTime(string targetFile, int samplesPerMinute, int minutesPerBin)
            : base(targetFile, samplesPerMinute, minutesPerBin)
        {
            Writer.WriteLine("Vertical Time");
            Writer.WriteLine("Bin#\tVT(secs)\tBin timespan\t(VT = (VerticalCount / SampleFreq), Samples per minute: " + SamplesPerMinute + ", Minutes per bin: " + MinutesPerBin);
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
