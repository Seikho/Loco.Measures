using System.Globalization;
using System.IO;

namespace LocoDataExtractor
{
    public class LocoCV : LocoMeasurer // Center-Vertical movement
    {
        public LocoCV(string file, int binSize, int sampleFreq = 2) // Sampling frequency is 2/sec, but is changeable in the future.
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
            OutputFile = NewFile("CV");
            var sw = new StreamWriter(OutputFile);
            Counter = 0;
            sw.WriteLine("Center-Rearing Movement");
            sw.WriteLine("Bin#\tVM(breaks)\tBin timespan\t(CV = (Central RearCount per Bin), Sampling Frequency/sec: " + SampleFreq + ", Bin size: " + BinSize + " samples");

            foreach (string read in Contents)
            {
                UpdateValues(read);
                if (Pred.Length == 0) Pred = "00000000"; // Pred == null on first iteration
                string succRear = Succ.Substring(Succ.Length - 1, 1);
                string predRear = Pred.Substring(Pred.Length - 1, 1);
                string succX1 = Succ.Substring(1, 1);
                string succX2 = Succ.Substring(2, 1);
                string succY2 = Succ.Substring(5, 1);
                if (((succRear.Equals("1")) && (predRear.Equals("0"))) && (succY2.Equals("1")) && ((succX1.Equals("1")) || (succX2.Equals("1")))) Counter++;
                if (BinChange())
                {
                    Output.Add(Counter.ToString(CultureInfo.InvariantCulture));
                    WriteLine(sw, Counter);
                    BinCount++;
                    Counter = 0;
                    MinCount = 0;
                }
            }
            //WriteLine(sw, counter);
            sw.Dispose();
        }
    }
}
