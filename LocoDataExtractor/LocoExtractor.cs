﻿using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;

namespace LocoDataExtractor
{
    public partial class LocoExtractor : Form
    {
        protected Dictionary<string, string> RawFiles = new Dictionary<string, string>();
        public int Frequency = 0;
        public int BinMinutes = 0;
        public int SamplesPerMinute = 0;
        public string ChosenFile = "";
        public string ChosenFolder = "";
        public LocoExtractor()
        {
            InitializeComponent();
            FileProcessor.SelectedIndex = 0;
            fileSelect.InitialDirectory = "C:\\Files\\EncData\\";
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
                Int32.TryParse(SamplesPerMinuteInput.Text, out samplesPerMin);
                var lr = new LocoReader(line.Value, BinMinutes, SamplesPerMinute);
                lr.GenerateFixedFile(FileProcessor.SelectedIndex, samplesPerMin);
            }
            AddText("All files have been repaired.");

        }

        private void SelectFolders(object sender, EventArgs e)
        {
            folderSelect.SelectedPath = "";
            folderSelect.ShowDialog();
            if (String.IsNullOrEmpty(folderSelect.SelectedPath)) return;
            AddText(folderSelect.SelectedPath);
            RawFiles.Clear();
            ChosenFolder = folderSelect.SelectedPath;
            var dirs = Directory.GetDirectories(ChosenFolder);
            foreach (var dir in dirs)
            {
                foreach (var file in Directory.GetFiles(dir))
                {
                    if (!file.Contains(".txt") || file.Contains("Fixed") || file.Contains("_")) continue;
                    RawFiles.Add(Path.GetFileNameWithoutExtension(file), file);
                }
            }
            PopulateFileList();
        }

        private void FileProcessor_SelectedValueChanged(object sender, EventArgs e)
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
            int binSize,samplesPerMin;
            Int32.TryParse(BinSizeInput.Text, out binSize);
            Int32.TryParse(SamplesPerMinuteInput.Text, out samplesPerMin);
            if (binSize == 0)
            {
                AddText("------");
                AddText("The 'Bin Size' is invalid. Please set a Bin Size (Default: 5)");
                AddText("------");
                return;
            }
            BinMinutes = binSize;
            SamplesPerMinute = samplesPerMin;
            GenerateMetrics();
        }

        private void GenerateMetrics()
        {
            foreach (var rawFile in RawFiles)
            {
                var drugName = GetParentFolder(rawFile.Value);
                var ratId = GetRatId(rawFile.Key);
                var sessionNumber = GetSessionNumber(rawFile.Key);
                if (String.IsNullOrEmpty(ratId) || String.IsNullOrEmpty(sessionNumber))
                {
                    AddText("Could not generate metrics for: " + rawFile.Key + ": Filename does not meet criteria: [RatID]-[SessionNumber].txt");
                    AddText("File location: " + rawFile.Value);
                    AddText("");
                }
                var lr = new LocoReader(rawFile.Value, BinMinutes, SamplesPerMinute);
                
                AddText(String.Format("File: {0} Drug: {1} Rat: {2} Session: {3}"
                    ,rawFile.Key,drugName,ratId,sessionNumber));
                lr.GenerateMetrics();
                lr.GenRData(ratId, sessionNumber, drugName);
            }
        }

        private string GetParentFolder(string file)
        {
            var split = Path.GetFullPath(file).Split("\\".ToCharArray()[0]);
            return split[split.Length - 2];
        }

        private string GetRatId(string fileName)
        {
            if (String.IsNullOrEmpty(fileName)) return "";
            var split = fileName.Split('-');
            return split[0];
        }

        private string GetSessionNumber(string fileName)
        {
            if (String.IsNullOrEmpty(fileName)) return "";
            var split = fileName.Split('-');
            if (split.Length < 2) return "";
            return split[1];
        }
    }
}
