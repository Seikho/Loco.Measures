using LocoDataExtractor.Metrics.Strategy;

namespace LocoDataExtractor.Metrics
{
    public class VerticalMovement : Metric // Vertical movement
    {
        public VerticalMovement(string targetFile, int samplesPerMinute, int minutesPerBin)
            : base(targetFile, samplesPerMinute, minutesPerBin)
        {
            Strategy = new CountStrategy();
            Writer.WriteLine("Vertical Movement");
            Writer.WriteLine("Bin#\tVM(breaks)\tBin timespan\t(VM = (RearCount per Bin), Samples per minute: " + SamplesPerMinute + ", Minutes per bin: " + MinutesPerBin);
        }

        public override void Execute()
        {
            var predRear = Pred.Substring(Pred.Length - 1, 1);
            var succRear = Succ.Substring(Succ.Length - 1, 1);
            if ((succRear.Equals("1")) && (predRear.Equals("0"))) Counter++;
        }
    }
}
