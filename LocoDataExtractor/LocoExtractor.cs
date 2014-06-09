using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;

namespace LocoDataExtractor
{
    public partial class LocoExtractor : Form
    {
        protected Dictionary<string, string> RawFiles = new Dictionary<string, string>();
        public int Frequency = 1;
        public string ChosenFile = "";
        public string ChosenFolder = "";
        public LocoExtractor()
        {
            InitializeComponent();
            FileProcessor.SelectedIndex = 0;
            fileSelect.InitialDirectory = "C:\\Files\\EncData\\";
            BinSize.Text = @"10";
            SampleFrequency.Text = Frequency.ToString(CultureInfo.InvariantCulture);
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            fileSelect.ShowDialog();
        }

        private void fileSelect_FileOk(object sender, CancelEventArgs e)
        {
            RawFiles.Clear();
            foreach (var file in fileSelect.FileNames)
            {
                if (!String.IsNullOrEmpty(file))
                {
                    RawFiles.Add(Path.GetFileName(file), file);
                }
            }
            PopulateFileList();
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
                var lrGen = new LocoReader("", Frequency);
                var lrName = lrGen.GenerateFixedFile(FileProcessor.SelectedIndex);
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
            foreach (var line in RawFiles)
            {
                int samplesPerMin;
                Int32.TryParse(SamplesPerMinute.Text, out samplesPerMin);
                var lr = new LocoReader(line.Value, Frequency);
                lr.GenerateFixedFile(FileProcessor.SelectedIndex, samplesPerMin);
            }
            AddText("All files have been repaired.");

        }

        private void lbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChosenFile = FileList.Text;
            textBox1.Text = ChosenFile;
        }

        private void SelectFolders(object sender, EventArgs e)
        {
            folderSelect.ShowDialog();
            if (String.IsNullOrEmpty(folderSelect.SelectedPath)) return;
            FileList.Items.Clear();
            ChosenFolder = folderSelect.SelectedPath;
            var dirs = Directory.GetDirectories(ChosenFolder);
            foreach (var dir in dirs)
            {
                foreach (var file in Directory.GetFiles(dir))
                {
                    if (!file.Contains(".txt") || file.Contains("Fixed")) continue;
                    RawFiles.Add(Path.GetFileNameWithoutExtension(file), file);
                }
            }
            PopulateFileList();
        }

        private void FileProcessor_SelectedValueChanged(object sender, EventArgs e)
        {
            var state = FileProcessor.SelectedIndex == 2;
            samplesLabel.Visible = state;
            SamplesPerMinute.Visible = state;
        }

        private void folderSelect_HelpRequest(object sender, EventArgs e)
        {

        }

        private void PopulateFileList()
        {
            FileList.Items.Clear();
            foreach (var row in RawFiles)
            {
                FileList.Items.Add(row.Key);
            }
        }

        private void GenMetricFiles_Click(object sender, EventArgs e)
        {

        }
    }
}
