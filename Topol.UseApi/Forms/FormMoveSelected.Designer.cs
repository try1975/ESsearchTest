namespace Topol.UseApi.Forms
{
    partial class FormMoveSelected
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbNew = new System.Windows.Forms.RadioButton();
            this.rbExist = new System.Windows.Forms.RadioButton();
            this.lblName = new System.Windows.Forms.Label();
            this.lblExtId = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbExtId = new System.Windows.Forms.TextBox();
            this.tbId = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 189);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(660, 42);
            this.panel1.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(147, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Отменить";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(42, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Подтвердить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbId);
            this.panel2.Controls.Add(this.tbExtId);
            this.panel2.Controls.Add(this.tbName);
            this.panel2.Controls.Add(this.lblId);
            this.panel2.Controls.Add(this.lblExtId);
            this.panel2.Controls.Add(this.lblName);
            this.panel2.Controls.Add(this.rbExist);
            this.panel2.Controls.Add(this.rbNew);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(660, 154);
            this.panel2.TabIndex = 4;
            // 
            // rbNew
            // 
            this.rbNew.AutoSize = true;
            this.rbNew.Checked = true;
            this.rbNew.Location = new System.Drawing.Point(39, 33);
            this.rbNew.Name = "rbNew";
            this.rbNew.Size = new System.Drawing.Size(96, 17);
            this.rbNew.TabIndex = 0;
            this.rbNew.TabStop = true;
            this.rbNew.Text = "новый запрос";
            this.rbNew.UseVisualStyleBackColor = true;
            this.rbNew.CheckedChanged += new System.EventHandler(this.rbNew_CheckedChanged);
            // 
            // rbExist
            // 
            this.rbExist.AutoSize = true;
            this.rbExist.Location = new System.Drawing.Point(39, 93);
            this.rbExist.Name = "rbExist";
            this.rbExist.Size = new System.Drawing.Size(141, 17);
            this.rbExist.TabIndex = 0;
            this.rbExist.TabStop = true;
            this.rbExist.Text = "существующий запрос";
            this.rbExist.UseVisualStyleBackColor = true;
            this.rbExist.CheckedChanged += new System.EventHandler(this.rbExist_CheckedChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(261, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(83, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Наименование";
            // 
            // lblExtId
            // 
            this.lblExtId.AutoSize = true;
            this.lblExtId.Location = new System.Drawing.Point(514, 9);
            this.lblExtId.Name = "lblExtId";
            this.lblExtId.Size = new System.Drawing.Size(31, 13);
            this.lblExtId.TabIndex = 2;
            this.lblExtId.Text = "ExtId";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Enabled = false;
            this.lblId.Location = new System.Drawing.Point(261, 75);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(87, 13);
            this.lblId.TabIndex = 3;
            this.lblId.Text = "Идентификатор";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(264, 33);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(203, 20);
            this.tbName.TabIndex = 4;
            // 
            // tbExtId
            // 
            this.tbExtId.Location = new System.Drawing.Point(517, 33);
            this.tbExtId.Name = "tbExtId";
            this.tbExtId.Size = new System.Drawing.Size(110, 20);
            this.tbExtId.TabIndex = 5;
            // 
            // tbId
            // 
            this.tbId.Enabled = false;
            this.tbId.Location = new System.Drawing.Point(264, 93);
            this.tbId.Name = "tbId";
            this.tbId.Size = new System.Drawing.Size(203, 20);
            this.tbId.TabIndex = 6;
            // 
            // FormSelectSeacrhItem
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(660, 231);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FormMoveSelected";
            this.Text = "Выбор варианта";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.RadioButton rbNew;
        public System.Windows.Forms.RadioButton rbExist;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblExtId;
        private System.Windows.Forms.Label lblName;
        public System.Windows.Forms.TextBox tbId;
        public System.Windows.Forms.TextBox tbExtId;
        public System.Windows.Forms.TextBox tbName;
    }
}