using System.Globalization;
using System.IO;

namespace LocoDataExtractor
{
    public class LocoIMT : LocoMeasurer // Immobile Time
    {
        public LocoIMT(string file, int binSize, int sampleFreq = 2) // default sampling frequency is 2/sec
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
            OutputFile = NewFile("IMT");
            var sw = new StreamWriter(OutputFile);
            Counter = 0;
            sw.WriteLine("Immobile Time");
            sw.WriteLine("Bin#\tIMT(sec)\tBin timespan\t(IMT = (IdleReadings / SamplingTime), Sampling Frequency/sec: " + SampleFreq + ", Bin size: " + BinSize + " samples");

            foreach (string read in Contents)
            {
                UpdateValues(read);
                if (PredNR.Length == 0) PredNR = SuccNR; // bugfix: first reading will always count as IMT, not HM.
                if (PredNR.Equals(SuccNR)) Counter++;
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
