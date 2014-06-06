using System.Globalization;
using System.IO;

namespace LocoDataExtractor.Metrics
{
    public class VerticalMovement : Metric // Vertical movement
    {
        public VerticalMovement(string file, int binSize, int sampleFreq = 2) // Sampling frequency is 2/sec, but is changeable in the future.
        {
            File = file;
            ReadFile();
            SampleFreq = sampleFreq;
            BinSize = binSize;
            MinCount = 0;
            Counter = 0;
            Pred = "";
            Succ = "";
            PredNoRear = "";
            SuccNoRear = "";
            BinCount = 1;
        }

        public override void Extract()
        {
            OutputFile = NewFile("VM");
            var sw = new StreamWriter(OutputFile);
            Counter = 0;
            sw.WriteLine("Vertical Movement");
            sw.WriteLine("Bin#\tVM(breaks)\tBin timespan\t(VM = (RearCount per Bin), Sampling Frequency/sec: " + SampleFreq + ", Bin size: " + BinSize + " samples");

            foreach (string read in Contents)
            {
                UpdateValues(read);
                if (Pred.Length == 0) Pred = "00000000"; // Pred == null on first iteration
                var predRear = Pred.Substring(Pred.Length - 1, 1);
                var succRear = Succ.Substring(Succ.Length - 1, 1);
                if ((succRear.Equals("1")) && (predRear.Equals("0"))) Counter++;
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
