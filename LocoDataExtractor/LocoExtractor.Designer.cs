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
            this.btExtract = new System.Windows.Forms.Button();
            this.console = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FileList = new System.Windows.Forms.ListBox();
            this.BinSize = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.settingSessNo = new System.Windows.Forms.TextBox();
            this.settingsDrug = new System.Windows.Forms.TextBox();
            this.settingsRatID = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SampleFrequency = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btRepair = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.btSelect.Location = new System.Drawing.Point(153, 41);
            this.btSelect.Name = "btSelect";
            this.btSelect.Size = new System.Drawing.Size(75, 23);
            this.btSelect.TabIndex = 0;
            this.btSelect.Text = "Select File(s)";
            this.btSelect.UseVisualStyleBackColor = true;
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // btExtract
            // 
            this.btExtract.Location = new System.Drawing.Point(363, 70);
            this.btExtract.Name = "btExtract";
            this.btExtract.Size = new System.Drawing.Size(75, 23);
            this.btExtract.TabIndex = 3;
            this.btExtract.Text = "Extract Data";
            this.btExtract.UseVisualStyleBackColor = true;
            this.btExtract.Click += new System.EventHandler(this.btExtract_Click);
            // 
            // console
            // 
            this.console.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.console.Location = new System.Drawing.Point(12, 322);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(490, 273);
            this.console.TabIndex = 4;
            this.console.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btRepair);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.FileList);
            this.groupBox1.Controls.Add(this.BinSize);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.btSelect);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(490, 148);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Extraction Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Selected Files:";
            // 
            // FileList
            // 
            this.FileList.FormattingEnabled = true;
            this.FileList.Location = new System.Drawing.Point(249, 16);
            this.FileList.Name = "FileList";
            this.FileList.Size = new System.Drawing.Size(233, 95);
            this.FileList.TabIndex = 13;
            this.FileList.SelectedIndexChanged += new System.EventHandler(this.lbFiles_SelectedIndexChanged);
            // 
            // BinSize
            // 
            this.BinSize.Location = new System.Drawing.Point(155, 119);
            this.BinSize.Name = "BinSize";
            this.BinSize.Size = new System.Drawing.Size(59, 20);
            this.BinSize.TabIndex = 12;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 122);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 13);
            this.label13.TabIndex = 11;
            this.label13.Text = "Bin Size (N of minutes):";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.settingSessNo);
            this.groupBox2.Controls.Add(this.settingsDrug);
            this.groupBox2.Controls.Add(this.btExtract);
            this.groupBox2.Controls.Add(this.settingsRatID);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.SampleFrequency);
            this.groupBox2.Location = new System.Drawing.Point(12, 164);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(490, 124);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File Info";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 99);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(475, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Note: Only sampling frequency is required. R Data will not be generated if the ot" +
    "her fields are empty.";
            // 
            // settingSessNo
            // 
            this.settingSessNo.Location = new System.Drawing.Point(370, 39);
            this.settingSessNo.Name = "settingSessNo";
            this.settingSessNo.Size = new System.Drawing.Size(63, 20);
            this.settingSessNo.TabIndex = 7;
            // 
            // settingsDrug
            // 
            this.settingsDrug.Location = new System.Drawing.Point(219, 39);
            this.settingsDrug.Name = "settingsDrug";
            this.settingsDrug.Size = new System.Drawing.Size(119, 20);
            this.settingsDrug.TabIndex = 6;
            // 
            // settingsRatID
            // 
            this.settingsRatID.Location = new System.Drawing.Point(142, 39);
            this.settingsRatID.Name = "settingsRatID";
            this.settingsRatID.Size = new System.Drawing.Size(59, 20);
            this.settingsRatID.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(359, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Session Number";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(246, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Drug Name";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(152, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Rat ID";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Sampling Frequency";
            // 
            // SampleFrequency
            // 
            this.SampleFrequency.Location = new System.Drawing.Point(10, 39);
            this.SampleFrequency.Name = "SampleFrequency";
            this.SampleFrequency.Size = new System.Drawing.Size(100, 20);
            this.SampleFrequency.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 306);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Information:";
            // 
            // btRepair
            // 
            this.btRepair.Location = new System.Drawing.Point(392, 117);
            this.btRepair.Name = "btRepair";
            this.btRepair.Size = new System.Drawing.Size(92, 23);
            this.btRepair.TabIndex = 15;
            this.btRepair.Text = "Process Files";
            this.btRepair.UseVisualStyleBackColor = true;
            this.btRepair.Click += new System.EventHandler(this.btRepair_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(274, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Generate Fixed Files:";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(97, 71);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(241, 20);
            this.textBox1.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Selected File:";
            // 
            // LocoExtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 602);
            this.Controls.Add(this.groupBox2);
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
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog fileSelect;
        private System.Windows.Forms.Button btSelect;
        private System.Windows.Forms.Button btExtract;
        private System.Windows.Forms.RichTextBox console;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox SampleFrequency;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox settingSessNo;
        private System.Windows.Forms.TextBox settingsDrug;
        private System.Windows.Forms.TextBox settingsRatID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox BinSize;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox FileList;
        private System.Windows.Forms.Button btRepair;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
    }
}

