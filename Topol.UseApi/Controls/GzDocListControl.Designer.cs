namespace Topol.UseApi.Controls
{
    partial class GzDocListControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbGzDocs = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnWordTable = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbGzDocs
            // 
            this.lbGzDocs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbGzDocs.FormattingEnabled = true;
            this.lbGzDocs.Location = new System.Drawing.Point(33, 0);
            this.lbGzDocs.Name = "lbGzDocs";
            this.lbGzDocs.Size = new System.Drawing.Size(319, 155);
            this.lbGzDocs.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnWordTable);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(33, 155);
            this.panel1.TabIndex = 5;
            // 
            // btnWordTable
            // 
            this.btnWordTable.Location = new System.Drawing.Point(6, 3);
            this.btnWordTable.Name = "btnWordTable";
            this.btnWordTable.Size = new System.Drawing.Size(17, 19);
            this.btnWordTable.TabIndex = 5;
            this.btnWordTable.Text = "Т";
            this.btnWordTable.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(352, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(33, 155);
            this.panel2.TabIndex = 6;
            // 
            // GzDocListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbGzDocs);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "GzDocListControl";
            this.Size = new System.Drawing.Size(385, 155);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbGzDocs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnWordTable;
        private System.Windows.Forms.Panel panel2;
    }
}
