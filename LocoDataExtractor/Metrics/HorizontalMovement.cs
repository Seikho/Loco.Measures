using System.Globalization;
using System.IO;

namespace LocoDataExtractor.Metrics
{
    public class HorizontalMovement : Metric // Horizontal movement
    {
        public HorizontalMovement(string file, int binSize, int sampleFreq = 2) : base(file, binSize, sampleFreq)
        {
        }

        public override void Extract()
        {
            OutputFile = NewFile("HM");
            var sw = new StreamWriter(OutputFile);
            Counter = 0;
            sw.WriteLine("Horizontal Movement");
            sw.WriteLine("Bin#\tHM(breaks)\tBin timespan\t(HM = (MovementCount per Bin), Sampling Frequency/sec: " + SampleFreq + ", Bin size: " + BinSize + " samples");

            foreach (var read in Contents)
            {
                UpdateValues(read);
                if (PredNoRear.Length == 0) PredNoRear = SuccNoRear; // bugfix: first reading will always count as IMT, not HM.
                if (!PredNoRear.Equals(SuccNoRear)) Counter++;
                if (!BinChange()) continue;
                Output.Add(Counter.ToString(CultureInfo.InvariantCulture));
                WriteLine(sw, Counter);
                BinCount++;
                Counter = 0;
                MinCount = 0;
            }
            sw.Dispose();
        }
    }
}
