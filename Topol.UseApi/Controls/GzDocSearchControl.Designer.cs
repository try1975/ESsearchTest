﻿namespace Topol.UseApi.Controls
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbGzDocRegions = new System.Windows.Forms.TextBox();
            this.tbGzDocMonths = new System.Windows.Forms.TextBox();
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
            this.panel28.Controls.Add(this.tbGzDocMonths);
            this.panel28.Controls.Add(this.tbGzDocRegions);
            this.panel28.Controls.Add(this.label3);
            this.panel28.Controls.Add(this.label2);
            this.panel28.Controls.Add(this.btnGzDocSearch);
            this.panel28.Controls.Add(this.tbGzDocSearchKey);
            this.panel28.Controls.Add(this.label22);
            this.panel28.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel28.Location = new System.Drawing.Point(0, 0);
            this.panel28.Name = "panel28";
            this.panel28.Size = new System.Drawing.Size(854, 50);
            this.panel28.TabIndex = 1;
            // 
            // btnGzDocSearch
            // 
            this.btnGzDocSearch.Location = new System.Drawing.Point(415, 10);
            this.btnGzDocSearch.Name = "btnGzDocSearch";
            this.btnGzDocSearch.Size = new System.Drawing.Size(75, 23);
            this.btnGzDocSearch.TabIndex = 2;
            this.btnGzDocSearch.Text = "Поиск";
            this.btnGzDocSearch.UseVisualStyleBackColor = true;
            // 
            // tbGzDocSearchKey
            // 
            this.tbGzDocSearchKey.Location = new System.Drawing.Point(108, 12);
            this.tbGzDocSearchKey.Name = "tbGzDocSearchKey";
            this.tbGzDocSearchKey.Size = new System.Drawing.Size(291, 20);
            this.tbGzDocSearchKey.TabIndex = 1;
            this.tbGzDocSearchKey.Text = "попугай";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(19, 12);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(82, 13);
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
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(854, 418);
            this.panel1.TabIndex = 2;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(431, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 418);
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
            this.dgvDocs.Location = new System.Drawing.Point(37, 0);
            this.dgvDocs.Name = "dgvDocs";
            this.dgvDocs.Size = new System.Drawing.Size(397, 418);
            this.dgvDocs.TabIndex = 2;
            this.dgvDocs.TimeFilter = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(434, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(420, 418);
            this.panel3.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(420, 182);
            this.panel4.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 32);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(420, 150);
            this.panel7.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(420, 32);
            this.panel5.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Все документы контракта";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnGoGzWebsite);
            this.panel2.Controls.Add(this.btnWordTable);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(37, 418);
            this.panel2.TabIndex = 3;
            // 
            // btnGoGzWebsite
            // 
            this.btnGoGzWebsite.Location = new System.Drawing.Point(14, 28);
            this.btnGoGzWebsite.Name = "btnGoGzWebsite";
            this.btnGoGzWebsite.Size = new System.Drawing.Size(17, 19);
            this.btnGoGzWebsite.TabIndex = 7;
            this.btnGoGzWebsite.Text = "Г";
            this.btnGoGzWebsite.UseVisualStyleBackColor = true;
            // 
            // btnWordTable
            // 
            this.btnWordTable.Enabled = false;
            this.btnWordTable.Location = new System.Drawing.Point(14, 3);
            this.btnWordTable.Name = "btnWordTable";
            this.btnWordTable.Size = new System.Drawing.Size(17, 19);
            this.btnWordTable.TabIndex = 5;
            this.btnWordTable.Text = "Т";
            this.btnWordTable.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(511, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Код региона";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(670, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Код ГодМесяц";
            // 
            // tbGzDocRegions
            // 
            this.tbGzDocRegions.Location = new System.Drawing.Point(587, 8);
            this.tbGzDocRegions.Name = "tbGzDocRegions";
            this.tbGzDocRegions.Size = new System.Drawing.Size(77, 20);
            this.tbGzDocRegions.TabIndex = 5;
            this.tbGzDocRegions.Text = "50,77";
            // 
            // tbGzDocMonths
            // 
            this.tbGzDocMonths.Location = new System.Drawing.Point(756, 8);
            this.tbGzDocMonths.Name = "tbGzDocMonths";
            this.tbGzDocMonths.Size = new System.Drawing.Size(77, 20);
            this.tbGzDocMonths.TabIndex = 6;
            this.tbGzDocMonths.Text = "201902";
            // 
            // GzDocSearchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel28);
            this.Name = "GzDocSearchControl";
            this.Size = new System.Drawing.Size(854, 468);
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
        private System.Windows.Forms.TextBox tbGzDocRegions;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}
