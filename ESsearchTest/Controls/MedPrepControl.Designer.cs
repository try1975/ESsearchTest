namespace ESsearchTest.Controls
{
    partial class MedPrepControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbLekForm = new System.Windows.Forms.ComboBox();
            this.gbLekForma = new System.Windows.Forms.GroupBox();
            this.gbUpak = new System.Windows.Forms.GroupBox();
            this.tbUpak = new System.Windows.Forms.TextBox();
            this.gbDoz = new System.Windows.Forms.GroupBox();
            this.cmbDoz = new System.Windows.Forms.ComboBox();
            this.tbDoz = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFirstWords = new System.Windows.Forms.TextBox();
            this.clbSyn = new System.Windows.Forms.CheckedListBox();
            this.gbSyn = new System.Windows.Forms.GroupBox();
            this.gbLekForma.SuspendLayout();
            this.gbUpak.SuspendLayout();
            this.gbDoz.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbSyn.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbLekForm
            // 
            this.cmbLekForm.FormattingEnabled = true;
            this.cmbLekForm.Location = new System.Drawing.Point(6, 19);
            this.cmbLekForm.Name = "cmbLekForm";
            this.cmbLekForm.Size = new System.Drawing.Size(139, 21);
            this.cmbLekForm.TabIndex = 0;
            this.cmbLekForm.SelectedIndexChanged += new System.EventHandler(this.cmbLekForm_SelectedIndexChanged);
            // 
            // gbLekForma
            // 
            this.gbLekForma.Controls.Add(this.cmbLekForm);
            this.gbLekForma.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbLekForma.Location = new System.Drawing.Point(0, 38);
            this.gbLekForma.Name = "gbLekForma";
            this.gbLekForma.Size = new System.Drawing.Size(151, 56);
            this.gbLekForma.TabIndex = 1;
            this.gbLekForma.TabStop = false;
            this.gbLekForma.Text = "Форма";
            // 
            // gbUpak
            // 
            this.gbUpak.Controls.Add(this.tbUpak);
            this.gbUpak.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbUpak.Location = new System.Drawing.Point(151, 38);
            this.gbUpak.Name = "gbUpak";
            this.gbUpak.Size = new System.Drawing.Size(101, 56);
            this.gbUpak.TabIndex = 2;
            this.gbUpak.TabStop = false;
            this.gbUpak.Text = "Упаковка";
            // 
            // tbUpak
            // 
            this.tbUpak.Location = new System.Drawing.Point(6, 20);
            this.tbUpak.Name = "tbUpak";
            this.tbUpak.Size = new System.Drawing.Size(87, 20);
            this.tbUpak.TabIndex = 0;
            this.tbUpak.TextChanged += new System.EventHandler(this.tbUpak_TextChanged);
            // 
            // gbDoz
            // 
            this.gbDoz.Controls.Add(this.cmbDoz);
            this.gbDoz.Controls.Add(this.tbDoz);
            this.gbDoz.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbDoz.Location = new System.Drawing.Point(252, 38);
            this.gbDoz.Name = "gbDoz";
            this.gbDoz.Size = new System.Drawing.Size(200, 56);
            this.gbDoz.TabIndex = 3;
            this.gbDoz.TabStop = false;
            this.gbDoz.Text = "Дозировка";
            // 
            // cmbDoz
            // 
            this.cmbDoz.FormattingEnabled = true;
            this.cmbDoz.Location = new System.Drawing.Point(92, 20);
            this.cmbDoz.Name = "cmbDoz";
            this.cmbDoz.Size = new System.Drawing.Size(82, 21);
            this.cmbDoz.TabIndex = 1;
            this.cmbDoz.SelectedIndexChanged += new System.EventHandler(this.cmbDoz_SelectedIndexChanged);
            // 
            // tbDoz
            // 
            this.tbDoz.Location = new System.Drawing.Point(16, 20);
            this.tbDoz.Name = "tbDoz";
            this.tbDoz.Size = new System.Drawing.Size(70, 20);
            this.tbDoz.TabIndex = 0;
            this.tbDoz.TextChanged += new System.EventHandler(this.tbDoz_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbFirstWords);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(705, 38);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Наименование";
            // 
            // tbFirstWords
            // 
            this.tbFirstWords.Location = new System.Drawing.Point(104, 7);
            this.tbFirstWords.Name = "tbFirstWords";
            this.tbFirstWords.Size = new System.Drawing.Size(391, 20);
            this.tbFirstWords.TabIndex = 0;
            this.tbFirstWords.TextChanged += new System.EventHandler(this.tbFirstWords_TextChanged);
            // 
            // clbSyn
            // 
            this.clbSyn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbSyn.FormattingEnabled = true;
            this.clbSyn.Location = new System.Drawing.Point(3, 16);
            this.clbSyn.MultiColumn = true;
            this.clbSyn.Name = "clbSyn";
            this.clbSyn.Size = new System.Drawing.Size(247, 37);
            this.clbSyn.TabIndex = 5;
            this.clbSyn.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbSyn_ItemCheck);
            // 
            // gbSyn
            // 
            this.gbSyn.Controls.Add(this.clbSyn);
            this.gbSyn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSyn.Location = new System.Drawing.Point(452, 38);
            this.gbSyn.Name = "gbSyn";
            this.gbSyn.Size = new System.Drawing.Size(253, 56);
            this.gbSyn.TabIndex = 6;
            this.gbSyn.TabStop = false;
            this.gbSyn.Text = "Аналоги";
            // 
            // MedPrepControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbSyn);
            this.Controls.Add(this.gbDoz);
            this.Controls.Add(this.gbUpak);
            this.Controls.Add(this.gbLekForma);
            this.Controls.Add(this.panel1);
            this.Name = "MedPrepControl";
            this.Size = new System.Drawing.Size(705, 94);
            this.gbLekForma.ResumeLayout(false);
            this.gbUpak.ResumeLayout(false);
            this.gbUpak.PerformLayout();
            this.gbDoz.ResumeLayout(false);
            this.gbDoz.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbSyn.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbLekForm;
        private System.Windows.Forms.GroupBox gbLekForma;
        private System.Windows.Forms.GroupBox gbUpak;
        private System.Windows.Forms.GroupBox gbDoz;
        private System.Windows.Forms.TextBox tbDoz;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFirstWords;
        private System.Windows.Forms.TextBox tbUpak;
        private System.Windows.Forms.ComboBox cmbDoz;
        private System.Windows.Forms.CheckedListBox clbSyn;
        private System.Windows.Forms.GroupBox gbSyn;
    }
}
