using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocoDataExtractor
{
    public partial class LocoExtractor : Form
    {

        int _sampleFreq = 1;
        private string FilePath = "";
        private string ChosenFile = "";
        public LocoExtractor()
        {
            InitializeComponent();
            fileSelect.InitialDirectory = "C:\\Files\\EncData\\";
            tbBinSize.Text = "10";
            settingSF.Text = _sampleFreq.ToString();
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
                if (FilePath.Length == 0) FilePath = Path.GetDirectoryName(file);
                lbFiles.Items.Add(Path.GetFileName(file));
            }
            
        }

        private void btExtract_Click(object sender, EventArgs e)
        {
            var bin = 0;
            Int32.TryParse(tbBinSize.Text, out bin);
            var bins = new int[] { bin, bin, bin, bin, bin }; // leaving this legacy code in case decision is made to have unique bin sizes.
            _sampleFreq = 0;
            Int32.TryParse(settingSF.Text, out _sampleFreq);
            foreach (int binsize in bins)
            {
                if (binsize <= 0)
                {
                    AddText("ERROR: One of the supplied 'Bin Sizes' is invalid (less than or equal to zero). Please fix and try again.");
                    return;
                }
                if (_sampleFreq <= 0)
                {
                    AddText("ERROR: 'Sample Frequency' is invalid (less than or equal to zero). Please fix and try again.");
                    return;
                }
            }
            try
            {
                //foreach (var lr in from string line in fileSelect.FileNames select new LocoReader(line, _sampleFreq))
                if (ChosenFile.Length == 0)
                {
                    AddText("Please select a file from the Selected File(s) list box.");
                    return;
                }

                var lrGen = new LocoReader(FilePath + "\\" + ChosenFile, _sampleFreq);
                string lrName = lrGen.GenerateFixedFile();
                var lr = new LocoReader(lrName);
                AddText("Save location: " + Path.GetDirectoryName(lrName));
                lr.ImmobileTime(bins[0]);
                AddText("'Immobile Time' saved to: " + Path.GetFileName(lr.LM.OutputFile));
                lr.HorizontalMovement(bins[2]);
                AddText("'Horizontal Movement' saved to: " + Path.GetFileName(lr.LM.OutputFile));
                lr.VerticalMovement(bins[1]);
                AddText("'Vertical Movement' saved to: " + Path.GetFileName(lr.LM.OutputFile));
                lr.CenterVertical(bins[4]);
                AddText("'Central-Vertical Movement' saved to: " + Path.GetFileName(lr.LM.OutputFile));
                lr.VerticalTime(bins[3]);
                AddText("'Vertical Time' saved to: " + Path.GetFileName(lr.LM.OutputFile));
                if (settingsDrug.Text.Length > 0 && settingsDrug.Text.Length > 0 && settingsRatID.Text.Length > 0)
                {
                    if (lr.GenRData(settingsRatID.Text, settingSessNo.Text, settingsDrug.Text))
                        AddText("'R Data' has been saved to: " + Path.GetFileName(lr.OutputFile));
                    else
                        AddText(
                            "'R Data' has not been generated: The number of bins is not equal across all constructs.");
                }
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
            foreach (string line in fileSelect.FileNames)
            {
                var lr = new LocoReader(line, _sampleFreq);
                lr.GenerateFixedFile(false);
            }
        }

        private void lbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChosenFile = lbFiles.Text;
            textBox1.Text = ChosenFile;
        }
    }
}
