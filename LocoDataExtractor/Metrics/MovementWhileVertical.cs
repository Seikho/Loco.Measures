using System;
using System.Globalization;

namespace LocoDataExtractor.Metrics
{
    public class MovementWhileVertical : Metric
    {
        public MovementWhileVertical(string file, int binSize, int sampleFreq = 2) : base(file, binSize, sampleFreq)
        {
        }

        public override void Execute()
        {
            if (String.IsNullOrEmpty(Pred)) Pred = "00000000";
            var predRear = Pred.Substring(Pred.Length-1, 1);
            var succRear = Succ.Substring(Succ.Length - 1, 1);
            if (!predRear.Equals("1") || !succRear.Equals("1")) return;
            var predHorz = Pred.Substring(0, Pred.Length - 1);
            var succHorz = Succ.Substring(0, Succ.Length - 1);
            if (predHorz.Equals(succHorz)) return;
            Counter++;
            if (!BinChange()) return;
            Output.Add(Counter.ToString(CultureInfo.InvariantCulture));
            WriteLine(Counter);
            BinCount++;
            Counter = 0;
        }
    }
}
