using System.Globalization;
using System.IO;

namespace LocoDataExtractor
{
    public class LocoVT : LocoMeasurer // Vertical time
    {
        public LocoVT(string file, int binSize, int sampleFreq = 2) // Sampling frequency is 2/sec, but is changeable in the future.
        {
            File = file;
            ReadFile();
            SampleFreq = sampleFreq;
            BinSize = binSize;
            MinCount = 0;
            Counter = 0;
            Pred = "";
            Succ = "";
            PredNR = "";
            SuccNR = "";
            BinCount = 1;
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
                if (BinChange())
                {
                    double div = Counter;
                    Output.Add((div / SampleFreq).ToString(CultureInfo.InvariantCulture));
                    WriteLine(sw, (div / SampleFreq));
                    BinCount++;
                    Counter = 0;
                    MinCount = 0;
                }
            }
            sw.Dispose();
        }
    }
}
