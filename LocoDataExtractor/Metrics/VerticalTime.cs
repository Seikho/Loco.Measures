using LocoDataExtractor.Metrics.Strategy;

namespace LocoDataExtractor.Metrics
{
    public class VerticalTime : Metric // Vertical time
    {
        public VerticalTime(string targetFile, int samplesPerMinute, int minutesPerBin)
            : base(targetFile, samplesPerMinute, minutesPerBin)
        {
            Strategy = new TimeStrategy();
            Writer.WriteLine("Vertical Time");
            Writer.WriteLine("Bin#\tVT(secs)\tBin timespan\t(VT = (VerticalCount / SampleFreq), Samples per minute: " + SamplesPerMinute + ", Minutes per bin: " + MinutesPerBin);
        }

        public override void Execute()
        {
            var succRear = Succ.Substring(Succ.Length - 1, 1);
            if (succRear.Equals("1")) Counter++;
        }
    }
}
