namespace Topol.UseApi.Controls
{
    partial class GzDocSearchControl
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel28 = new System.Windows.Forms.Panel();
            this.tbGzDocMonths = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGzDocSearch = new System.Windows.Forms.Button();
            this.tbGzDocSearchKey = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.dgvDocs = new ADGV.AdvancedDataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnGoGzWebsite = new System.Windows.Forms.Button();
            this.btnWordTable = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.clbRegions = new System.Windows.Forms.CheckedListBox();
            this.panel28.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocs)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel28
            // 
            this.panel28.Controls.Add(this.clbRegions);
            this.panel28.Controls.Add(this.tbGzDocMonths);
            this.panel28.Controls.Add(this.label3);
            this.panel28.Controls.Add(this.btnGzDocSearch);
            this.panel28.Controls.Add(this.tbGzDocSearchKey);
            this.panel28.Controls.Add(this.label22);
            this.panel28.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel28.Location = new System.Drawing.Point(0, 0);
            this.panel28.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel28.Name = "panel28";
            this.panel28.Size = new System.Drawing.Size(1281, 116);
            this.panel28.TabIndex = 1;
            // 
            // tbGzDocMonths
            // 
            this.tbGzDocMonths.Location = new System.Drawing.Point(1134, 12);
            this.tbGzDocMonths.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbGzDocMonths.Name = "tbGzDocMonths";
            this.tbGzDocMonths.Size = new System.Drawing.Size(114, 26);
            this.tbGzDocMonths.TabIndex = 6;
            this.tbGzDocMonths.Text = "201902";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1005, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Код ГодМесяц";
            // 
            // btnGzDocSearch
            // 
            this.btnGzDocSearch.Location = new System.Drawing.Point(622, 15);
            this.btnGzDocSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGzDocSearch.Name = "btnGzDocSearch";
            this.btnGzDocSearch.Size = new System.Drawing.Size(112, 35);
            this.btnGzDocSearch.TabIndex = 2;
            this.btnGzDocSearch.Text = "Поиск";
            this.btnGzDocSearch.UseVisualStyleBackColor = true;
            // 
            // tbGzDocSearchKey
            // 
            this.tbGzDocSearchKey.Location = new System.Drawing.Point(162, 18);
            this.tbGzDocSearchKey.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbGzDocSearchKey.Name = "tbGzDocSearchKey";
            this.tbGzDocSearchKey.Size = new System.Drawing.Size(434, 26);
            this.tbGzDocSearchKey.TabIndex = 1;
            this.tbGzDocSearchKey.Text = "попугай";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(28, 18);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(120, 20);
            this.label22.TabIndex = 0;
            this.label22.Text = "Строка поиска";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.dgvDocs);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 116);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1281, 604);
            this.panel1.TabIndex = 2;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(647, 0);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 604);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // dgvDocs
            // 
            this.dgvDocs.AllowUserToAddRows = false;
            this.dgvDocs.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvDocs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDocs.AutoGenerateContextFilters = true;
            this.dgvDocs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDocs.DateWithTime = false;
            this.dgvDocs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDocs.Location = new System.Drawing.Point(56, 0);
            this.dgvDocs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvDocs.Name = "dgvDocs";
            this.dgvDocs.Size = new System.Drawing.Size(595, 604);
            this.dgvDocs.TabIndex = 2;
            this.dgvDocs.TimeFilter = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(651, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(630, 604);
            this.panel3.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(630, 280);
            this.panel4.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 49);
            this.panel7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(630, 231);
            this.panel7.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(630, 49);
            this.panel5.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Все документы контракта";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnGoGzWebsite);
            this.panel2.Controls.Add(this.btnWordTable);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(56, 604);
            this.panel2.TabIndex = 3;
            // 
            // btnGoGzWebsite
            // 
            this.btnGoGzWebsite.Location = new System.Drawing.Point(21, 43);
            this.btnGoGzWebsite.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGoGzWebsite.Name = "btnGoGzWebsite";
            this.btnGoGzWebsite.Size = new System.Drawing.Size(26, 29);
            this.btnGoGzWebsite.TabIndex = 7;
            this.btnGoGzWebsite.Text = "Г";
            this.btnGoGzWebsite.UseVisualStyleBackColor = true;
            // 
            // btnWordTable
            // 
            this.btnWordTable.Enabled = false;
            this.btnWordTable.Location = new System.Drawing.Point(21, 5);
            this.btnWordTable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnWordTable.Name = "btnWordTable";
            this.btnWordTable.Size = new System.Drawing.Size(26, 29);
            this.btnWordTable.TabIndex = 5;
            this.btnWordTable.Text = "Т";
            this.btnWordTable.UseVisualStyleBackColor = true;
            // 
            // clbRegions
            // 
            this.clbRegions.FormattingEnabled = true;
            this.clbRegions.Location = new System.Drawing.Point(774, 15);
            this.clbRegions.Name = "clbRegions";
            this.clbRegions.Size = new System.Drawing.Size(224, 88);
            this.clbRegions.TabIndex = 7;
            // 
            // GzDocSearchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel28);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "GzDocSearchControl";
            this.Size = new System.Drawing.Size(1281, 720);
            this.panel28.ResumeLayout(false);
            this.panel28.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocs)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel28;
        private System.Windows.Forms.Button btnGzDocSearch;
        private System.Windows.Forms.TextBox tbGzDocSearchKey;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel1;
        private ADGV.AdvancedDataGridView dgvDocs;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnWordTable;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGoGzWebsite;
        private System.Windows.Forms.TextBox tbGzDocMonths;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox clbRegions;
    }
}
