using System.Windows.Forms;

namespace ControlDemoApp
{
    partial class MainForm : Form
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
            System.Windows.Forms.Label labelText;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.textBox = new System.Windows.Forms.TextBox();
            this.buttonGo = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxExcludeEnglishCommonWords = new System.Windows.Forms.CheckBox();
            this.checkBoxGroupSameStemWords = new System.Windows.Forms.CheckBox();
            this.cloudControl = new Gma.CodeCloud.Controls.CloudControl();
            this.linkLabelEditCustomBlacklist = new System.Windows.Forms.LinkLabel();
            labelText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelText
            // 
            labelText.AutoSize = true;
            labelText.Location = new System.Drawing.Point(12, 9);
            labelText.Name = "labelText";
            labelText.Size = new System.Drawing.Size(151, 13);
            labelText.TabIndex = 3;
            labelText.Text = "Paste a text, URL or file name:";
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(12, 49);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(701, 67);
            this.textBox.TabIndex = 4;
            this.textBox.Text = resources.GetString("textBox.Text");
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(12, 122);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(75, 23);
            this.buttonGo.TabIndex = 5;
            this.buttonGo.Text = "Go";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.ButtonGoClick);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(174, 122);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(539, 23);
            this.progressBar.TabIndex = 7;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Location = new System.Drawing.Point(93, 122);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.CancelClick);
            // 
            // checkBoxExcludeEnglishCommonWords
            // 
            this.checkBoxExcludeEnglishCommonWords.AutoSize = true;
            this.checkBoxExcludeEnglishCommonWords.Checked = true;
            this.checkBoxExcludeEnglishCommonWords.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxExcludeEnglishCommonWords.Location = new System.Drawing.Point(243, 3);
            this.checkBoxExcludeEnglishCommonWords.Name = "checkBoxExcludeEnglishCommonWords";
            this.checkBoxExcludeEnglishCommonWords.Size = new System.Drawing.Size(174, 17);
            this.checkBoxExcludeEnglishCommonWords.TabIndex = 9;
            this.checkBoxExcludeEnglishCommonWords.Text = "Exclude english common words";
            this.checkBoxExcludeEnglishCommonWords.UseVisualStyleBackColor = true;
            // 
            // checkBoxGroupSameStemWords
            // 
            this.checkBoxGroupSameStemWords.AutoSize = true;
            this.checkBoxGroupSameStemWords.Checked = true;
            this.checkBoxGroupSameStemWords.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGroupSameStemWords.Location = new System.Drawing.Point(243, 26);
            this.checkBoxGroupSameStemWords.Name = "checkBoxGroupSameStemWords";
            this.checkBoxGroupSameStemWords.Size = new System.Drawing.Size(273, 17);
            this.checkBoxGroupSameStemWords.TabIndex = 10;
            this.checkBoxGroupSameStemWords.Text = "Group same stem words (wait, wait-ed, wait-ing etc. )";
            this.checkBoxGroupSameStemWords.UseVisualStyleBackColor = true;
            // 
            // cloudControl
            // 
            this.cloudControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cloudControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cloudControl.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cloudControl.LayoutType = Gma.CodeCloud.Controls.LayoutType.Spiral;
            this.cloudControl.Location = new System.Drawing.Point(12, 151);
            this.cloudControl.MaxFontSize = 50;
            this.cloudControl.MinFontSize = 10;
            this.cloudControl.Name = "cloudControl";
            this.cloudControl.Palette = new System.Drawing.Color[] {
        System.Drawing.Color.DarkRed,
        System.Drawing.Color.DarkBlue,
        System.Drawing.Color.DarkGreen,
        System.Drawing.Color.Navy,
        System.Drawing.Color.DarkCyan,
        System.Drawing.Color.DarkOrange,
        System.Drawing.Color.DarkGoldenrod,
        System.Drawing.Color.DarkKhaki,
        System.Drawing.Color.Blue,
        System.Drawing.Color.Red,
        System.Drawing.Color.Green};
            this.cloudControl.Size = new System.Drawing.Size(705, 398);
            this.cloudControl.TabIndex = 6;
            this.cloudControl.WeightedWords = null;
            this.cloudControl.Click += new System.EventHandler(this.CloudControlClick);
            // 
            // linkLabelEditCustomBlacklist
            // 
            this.linkLabelEditCustomBlacklist.AutoSize = true;
            this.linkLabelEditCustomBlacklist.Location = new System.Drawing.Point(521, 3);
            this.linkLabelEditCustomBlacklist.Name = "linkLabelEditCustomBlacklist";
            this.linkLabelEditCustomBlacklist.Size = new System.Drawing.Size(193, 13);
            this.linkLabelEditCustomBlacklist.TabIndex = 11;
            this.linkLabelEditCustomBlacklist.TabStop = true;
            this.linkLabelEditCustomBlacklist.Text = "Add own exceptions (one word per line)";
            this.linkLabelEditCustomBlacklist.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelEditCustomBlacklistLinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 561);
            this.Controls.Add(this.linkLabelEditCustomBlacklist);
            this.Controls.Add(this.checkBoxGroupSameStemWords);
            this.Controls.Add(this.checkBoxExcludeEnglishCommonWords);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.cloudControl);
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.textBox);
            this.Controls.Add(labelText);
            this.Name = "MainForm";
            this.Text = "Word Cloud Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button buttonGo;
        private Gma.CodeCloud.Controls.CloudControl cloudControl;
        private System.Windows.Forms.ProgressBar progressBar;
        private Button buttonCancel;
        private CheckBox checkBoxExcludeEnglishCommonWords;
        private CheckBox checkBoxGroupSameStemWords;
        private LinkLabel linkLabelEditCustomBlacklist;
    }
}

