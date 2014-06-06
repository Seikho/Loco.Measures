using System.Globalization;
using System.IO;

namespace LocoDataExtractor
{
    public class LocoIMT : Metric // Immobile Time
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
            PredNoRear = "";
            SuccNoRear = "";
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
                if (PredNoRear.Length == 0) PredNoRear = SuccNoRear; // bugfix: first reading will always count as IMT, not HM.
                if (PredNoRear.Equals(SuccNoRear)) Counter++;
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
