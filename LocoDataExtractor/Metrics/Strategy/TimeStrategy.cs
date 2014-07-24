using System.Globalization;

namespace LocoDataExtractor.Metrics.Strategy
{
    class TimeStrategy : Strategy
    {
        public override string Process(int value)
        {
            double val = value;
            return (val/2).ToString(CultureInfo.InvariantCulture);
        }
    }
}
