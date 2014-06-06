using System;
using System.Globalization;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;

namespace LocoDataExtractor
{
    public partial class LocoExtractor : Form
    {

        public int Frequency = 1;
        public string FilePath = "";
        public string ChosenFile = "";
        public LocoExtractor()
        {
            InitializeComponent();
            fileSelect.InitialDirectory = "C:\\Files\\EncData\\";
            BinSize.Text = @"10";
            SampleFrequency.Text = Frequency.ToString(CultureInfo.InvariantCulture);
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            FilePath = "";
            fileSelect.ShowDialog();
        }

        private void fileSelect_FileOk(object sender, CancelEventArgs e)
        {
            foreach (var file in fileSelect.FileNames)
            {
                if (String.IsNullOrEmpty(FilePath)) FilePath = Path.GetDirectoryName(file);
                if (!String.IsNullOrEmpty(file)) FileList.Items.Add(Path.GetFileName(file));
            }
            
        }

        private void btExtract_Click(object sender, EventArgs e)
        {
            int bin;
            Int32.TryParse(BinSize.Text, out bin);
            Frequency = 0;
            Int32.TryParse(SampleFrequency.Text, out Frequency);
            if (bin <= 0)
            {
                AddText("ERROR: One of the supplied 'Bin Sizes' is invalid (less than or equal to zero). Please fix and try again.");
                return;
            }
            if (Frequency <= 0)
            {
                AddText("ERROR: 'Sample Frequency' is invalid (less than or equal to zero). Please fix and try again.");
                return;
            }
            if (ChosenFile.Length == 0)
            {
                AddText("Please select a file from the Selected File(s) list box.");
                return;
            }
            GetMetrics(bin);
        }

        private void GetMetrics(int bin)
        {
            try
            {
                var lrGen = new LocoReader(FilePath + "\\" + ChosenFile, Frequency);
                var lrName = lrGen.GenerateFixedFile();
                var lr = new LocoReader(lrName);
                lr.GenerateMetrics(bin);
                AddText("Save location: " + Path.GetDirectoryName(lrName));
                if (settingsDrug.Text.Length > 0 && settingsDrug.Text.Length > 0 && settingsRatID.Text.Length > 0)
                {
                    lr.GenRData(settingsRatID.Text, settingSessNo.Text, settingsDrug.Text);
                    AddText("'R Data' has been saved to: " + Path.GetFileName(lr.OutputFile));
                }
                else AddText("'R Data' has not been generated due to missing settings");
                AddText("Data has been successfully extracted.");
                File.Delete(lrName);
            }
            catch (Exception err)
            {
                AddText("[LocoReader] ERROR: " + err.Message);
            }
        }

        private void AddText(string text)
        {
            console.Invoke((MethodInvoker)delegate
            {
                console.AppendText(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " " + text + "\n");
                console.ScrollToCaret();
            });
        }

        private void btRepair_Click(object sender, EventArgs e)
        {
            foreach (var line in fileSelect.FileNames)
            {
                var lr = new LocoReader(line, Frequency);
                lr.GenerateFixedFile(false);
            }
        }

        private void lbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChosenFile = FileList.Text;
            textBox1.Text = ChosenFile;
        }
    }
}
