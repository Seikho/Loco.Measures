using System.Globalization;
using System.IO;

namespace LocoDataExtractor
{
    public class LocoHM : LocoMeasurer // Horizontal movement
    {
        public LocoHM(string file, int binSize, int sampleFreq = 2) // Sampling frequency is 2/sec, but is changeable in the future.
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
            OutputFile = NewFile("HM");
            var sw = new StreamWriter(OutputFile);
            Counter = 0;
            sw.WriteLine("Horizontal Movement");
            sw.WriteLine("Bin#\tHM(breaks)\tBin timespan\t(HM = (MovementCount per Bin), Sampling Frequency/sec: " + SampleFreq + ", Bin size: " + BinSize + " samples");

            foreach (string read in Contents)
            {
                UpdateValues(read);
                if (PredNR.Length == 0) PredNR = SuccNR; // bugfix: first reading will always count as IMT, not HM.
                if (!PredNR.Equals(SuccNR)) Counter++;
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
