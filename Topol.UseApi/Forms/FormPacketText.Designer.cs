namespace Topol.UseApi.Forms
{
    partial class FormPacketText
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
            this.gbPacketText = new System.Windows.Forms.GroupBox();
            this.tbTruItems = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.gbPacketText.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPacketText
            // 
            this.gbPacketText.Controls.Add(this.tbTruItems);
            this.gbPacketText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPacketText.Location = new System.Drawing.Point(0, 0);
            this.gbPacketText.Name = "gbPacketText";
            this.gbPacketText.Size = new System.Drawing.Size(677, 416);
            this.gbPacketText.TabIndex = 6;
            this.gbPacketText.TabStop = false;
            this.gbPacketText.Text = "Пакет ТРУ";
            // 
            // tbTruItems
            // 
            this.tbTruItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTruItems.Location = new System.Drawing.Point(3, 16);
            this.tbTruItems.Multiline = true;
            this.tbTruItems.Name = "tbTruItems";
            this.tbTruItems.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbTruItems.Size = new System.Drawing.Size(671, 397);
            this.tbTruItems.TabIndex = 1;
            this.tbTruItems.WordWrap = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 416);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(677, 42);
            this.panel1.TabIndex = 2;
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
            // FormPacketText
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(677, 458);
            this.Controls.Add(this.gbPacketText);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FormPacketText";
            this.Text = "Текст пакета";
            this.gbPacketText.ResumeLayout(false);
            this.gbPacketText.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPacketText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox tbTruItems;
        private System.Windows.Forms.Button button2;
    }
}