using System.Globalization;

namespace LocoDataExtractor.Metrics.Strategy
{
    public class CountStrategy : Strategy
    {
        public override string Process(int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
