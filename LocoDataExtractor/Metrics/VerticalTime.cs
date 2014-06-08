using System.Globalization;
using System.IO;

namespace LocoDataExtractor.Metrics
{
    public class VerticalTime : Metric // Vertical time
    {
        public VerticalTime(string file, int binSize, int sampleFreq = 2) : base(file, binSize, sampleFreq)
        {
        }

        public override void Extract()
        {
            OutputFile = NewFile("VT");
            var sw = new StreamWriter(OutputFile);
            Counter = 0;
            sw.WriteLine("Vertical Time");
            sw.WriteLine("Bin#\tVT(secs)\tBin timespan\t(VT = (VerticalCount / SampleFreq), Sampling Frequency/sec: " + SampleFreq + ", Bin size: " + BinSize + " samples");

            foreach (var read in Contents)
            {
                UpdateValues(read);
                var succRear = Succ.Substring(Succ.Length - 1, 1);
                if (succRear.Equals("1")) Counter++;
                if (!BinChange()) continue;
                double div = Counter;
                Output.Add((div / SampleFreq).ToString(CultureInfo.InvariantCulture));
                WriteLine(sw, (div / SampleFreq));
                BinCount++;
                Counter = 0;
                MinCount = 0;
            }
            sw.Dispose();
        }
    }
}
