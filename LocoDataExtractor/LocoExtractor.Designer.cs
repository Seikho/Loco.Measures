namespace LocoDataExtractor
{
    partial class LocoExtractor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fileSelect = new System.Windows.Forms.OpenFileDialog();
            this.btSelect = new System.Windows.Forms.Button();
            this.console = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BinSizeInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GenMetricFiles = new System.Windows.Forms.Button();
            this.samplesLabel = new System.Windows.Forms.Label();
            this.SamplesPerMinute = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.FileProcessor = new System.Windows.Forms.ComboBox();
            this.btRepair = new System.Windows.Forms.Button();
            this.FileList = new System.Windows.Forms.ListBox();
            this.BinSize = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.folderSelect = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileSelect
            // 
            this.fileSelect.FileName = "*.txt";
            this.fileSelect.Multiselect = true;
            this.fileSelect.Title = "Please select your data files";
            this.fileSelect.FileOk += new System.ComponentModel.CancelEventHandler(this.fileSelect_FileOk);
            // 
            // btSelect
            // 
            this.btSelect.Location = new System.Drawing.Point(153, 98);
            this.btSelect.Name = "btSelect";
            this.btSelect.Size = new System.Drawing.Size(90, 23);
            this.btSelect.TabIndex = 0;
            this.btSelect.Text = "Select File(s)";
            this.btSelect.UseVisualStyleBackColor = true;
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // console
            // 
            this.console.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.console.Location = new System.Drawing.Point(12, 295);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(490, 295);
            this.console.TabIndex = 4;
            this.console.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BinSizeInput);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.GenMetricFiles);
            this.groupBox1.Controls.Add(this.samplesLabel);
            this.groupBox1.Controls.Add(this.SamplesPerMinute);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.FileProcessor);
            this.groupBox1.Controls.Add(this.btRepair);
            this.groupBox1.Controls.Add(this.FileList);
            this.groupBox1.Controls.Add(this.BinSize);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.btSelect);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(490, 230);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Extraction Settings";
            // 
            // BinSizeInput
            // 
            this.BinSizeInput.Location = new System.Drawing.Point(294, 203);
            this.BinSizeInput.Name = "BinSizeInput";
            this.BinSizeInput.Size = new System.Drawing.Size(42, 20);
            this.BinSizeInput.TabIndex = 24;
            this.BinSizeInput.Text = "5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Bin Size (in minutes):";
            // 
            // GenMetricFiles
            // 
            this.GenMetricFiles.Location = new System.Drawing.Point(362, 201);
            this.GenMetricFiles.Name = "GenMetricFiles";
            this.GenMetricFiles.Size = new System.Drawing.Size(120, 23);
            this.GenMetricFiles.TabIndex = 22;
            this.GenMetricFiles.Text = "Generate Metric Files";
            this.GenMetricFiles.UseVisualStyleBackColor = true;
            this.GenMetricFiles.Click += new System.EventHandler(this.GenMetricFiles_Click);
            // 
            // samplesLabel
            // 
            this.samplesLabel.AutoSize = true;
            this.samplesLabel.ForeColor = System.Drawing.Color.Maroon;
            this.samplesLabel.Location = new System.Drawing.Point(9, 73);
            this.samplesLabel.Name = "samplesLabel";
            this.samplesLabel.Size = new System.Drawing.Size(71, 13);
            this.samplesLabel.TabIndex = 21;
            this.samplesLabel.Text = "Samples/min:";
            this.samplesLabel.Visible = false;
            // 
            // SamplesPerMinute
            // 
            this.SamplesPerMinute.BackColor = System.Drawing.Color.DarkOrange;
            this.SamplesPerMinute.Location = new System.Drawing.Point(86, 70);
            this.SamplesPerMinute.Name = "SamplesPerMinute";
            this.SamplesPerMinute.Size = new System.Drawing.Size(43, 20);
            this.SamplesPerMinute.TabIndex = 20;
            this.SamplesPerMinute.Text = "120";
            this.SamplesPerMinute.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(153, 127);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Select Folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SelectFolders);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "File Processing Method:";
            // 
            // FileProcessor
            // 
            this.FileProcessor.FormattingEnabled = true;
            this.FileProcessor.Items.AddRange(new object[] {
            "Blank Fill Method",
            "Blank Shift Method",
            "Custom Blank Shift Method"});
            this.FileProcessor.Location = new System.Drawing.Point(6, 43);
            this.FileProcessor.Name = "FileProcessor";
            this.FileProcessor.Size = new System.Drawing.Size(184, 21);
            this.FileProcessor.TabIndex = 17;
            this.FileProcessor.SelectedValueChanged += new System.EventHandler(this.FileProcessor_SelectedValueChanged);
            // 
            // btRepair
            // 
            this.btRepair.Location = new System.Drawing.Point(362, 167);
            this.btRepair.Name = "btRepair";
            this.btRepair.Size = new System.Drawing.Size(120, 23);
            this.btRepair.TabIndex = 15;
            this.btRepair.Text = "Process/Repair Files";
            this.btRepair.UseVisualStyleBackColor = true;
            this.btRepair.Click += new System.EventHandler(this.btRepair_Click);
            // 
            // FileList
            // 
            this.FileList.FormattingEnabled = true;
            this.FileList.Location = new System.Drawing.Point(249, 16);
            this.FileList.Name = "FileList";
            this.FileList.Size = new System.Drawing.Size(233, 134);
            this.FileList.TabIndex = 13;
            // 
            // BinSize
            // 
            this.BinSize.Location = new System.Drawing.Point(131, 164);
            this.BinSize.Name = "BinSize";
            this.BinSize.Size = new System.Drawing.Size(59, 20);
            this.BinSize.TabIndex = 12;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 167);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 13);
            this.label13.TabIndex = 11;
            this.label13.Text = "Bin Size (N of minutes):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 270);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Information:";
            // 
            // folderSelect
            // 
            this.folderSelect.SelectedPath = "C:\\";
            // 
            // LocoExtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 602);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.console);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LocoExtractor";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "LocoMotor Data Extractor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog fileSelect;
        private System.Windows.Forms.Button btSelect;
        private System.Windows.Forms.RichTextBox console;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox BinSize;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ListBox FileList;
        private System.Windows.Forms.Button btRepair;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox FileProcessor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog folderSelect;
        private System.Windows.Forms.Label samplesLabel;
        private System.Windows.Forms.TextBox SamplesPerMinute;
        private System.Windows.Forms.Button GenMetricFiles;
        private System.Windows.Forms.TextBox BinSizeInput;
        private System.Windows.Forms.Label label1;
    }
}

