using LocoDataExtractor.Metrics.Strategy;

namespace LocoDataExtractor.Metrics
{
    public class RepetitiousMovement : Metric
    {
        protected string FirstPosition = "";
        protected string SecondPosition = "";
        protected int RepetitiveFlag = 0;
        public RepetitiousMovement(string targetFile, int samplesPerMinute, int minutesPerBin = 5) : base(targetFile, samplesPerMinute, minutesPerBin)
        {
            Strategy = new CountStrategy();
            Writer.WriteLine("Repetitious Movement");
            Writer.WriteLine("Bin#\tIMT(sec)\tBin timespan\t(IMT = (IdleReadings / SamplingTime), Samples per minute: " + SamplesPerMinute + ", Minutes per bin: " + MinutesPerBin);
        }

        public override void Execute()
        {
            ProcessMovements();
        }

        private void ProcessMovements()
        {
            if (!IsRearing())
            {
                ResetPositions();
                return;
            }
            if (FirstPosition.Length == 0)
            {
                FirstPosition = Succ;
                return;
            }
            if (SecondPosition.Length == 0)
            {
                SetSecondPosition();
                return;
            }
            if (!Succ.Equals(FirstPosition) && !Succ.Equals(SecondPosition))
            {
                ResetPositions();
                return;
            }
            var startCount = RepetitiveFlag;
            if (Succ.Equals(FirstPosition) && Pred.Equals(SecondPosition)) RepetitiveFlag++;
            if (Succ.Equals(SecondPosition) && Pred.Equals(FirstPosition) && RepetitiveFlag > 0) RepetitiveFlag++;
            if (RepetitiveFlag > 1 && startCount < RepetitiveFlag) Counter++;
        }

        private void ResetPositions()
        {
            FirstPosition = "";
            SecondPosition = "";
            RepetitiveFlag = 0;
        }

        private void SetSecondPosition()
        {
            var changeCount = GetChangeCount();
            if (changeCount > 1) ResetPositions();
            if (changeCount == 1) SecondPosition = Succ;
        }

        private int GetChangeCount()
        {
            var count = 0;
            var first = FirstPosition.ToCharArray(0, 7);
            var second = Succ.ToCharArray(0, 7);
            for (var x = 0; x < 7; x++)
            {
                if (first[x] != second[x]) count++;
            }
            return count;
        }
    }
}
