﻿namespace ESsearchTest
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpSearch = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.clbSellers = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbExact = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbExclude = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbQuery = new System.Windows.Forms.TextBox();
            this.tpExpert = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.tbExactExpert = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbExcludeExpert = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbQueryExpert = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.udResultCount = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbScreenshotLink = new System.Windows.Forms.CheckBox();
            this.cbOpenLink = new System.Windows.Forms.CheckBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.gbNorm = new System.Windows.Forms.GroupBox();
            this.pnlNorm = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbNorm = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbNorm = new System.Windows.Forms.CheckBox();
            this.cbSearch = new System.Windows.Forms.CheckBox();
            this.cbExpert = new System.Windows.Forms.CheckBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.dgvSearchResult = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnUpdatePrices = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnExcelExport = new System.Windows.Forms.Button();
            this.lblLastUpdated = new System.Windows.Forms.Label();
            this.lblSellerCount = new System.Windows.Forms.Label();
            this.btnInvertSelection = new System.Windows.Forms.Button();
            this.btnCalc = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.lblIndexDocCount = new System.Windows.Forms.Label();
            this.lblSelectedSellerCount = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblSelectedAveragePrice = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSelectedNameCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblAllNameCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblAllSellerCount = new System.Windows.Forms.Label();
            this.timerAnalyze = new System.Windows.Forms.Timer(this.components);
            this.gbExpert = new System.Windows.Forms.GroupBox();
            this.dgvExpert = new System.Windows.Forms.DataGridView();
            this.splExpert = new System.Windows.Forms.Splitter();
            this.bsQuery = new System.Windows.Forms.BindingSource(this.components);
            this.bsExpert = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpSearch.SuspendLayout();
            this.tpExpert.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udResultCount)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbNorm.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResult)).BeginInit();
            this.panel3.SuspendLayout();
            this.gbExpert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsExpert)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.splitter2);
            this.panel1.Controls.Add(this.gbNorm);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1598, 505);
            this.panel1.TabIndex = 26;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpSearch);
            this.tabControl1.Controls.Add(this.tpExpert);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 276);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1596, 155);
            this.tabControl1.TabIndex = 38;
            // 
            // tpSearch
            // 
            this.tpSearch.AutoScroll = true;
            this.tpSearch.Controls.Add(this.label14);
            this.tpSearch.Controls.Add(this.clbSellers);
            this.tpSearch.Controls.Add(this.label1);
            this.tpSearch.Controls.Add(this.tbExact);
            this.tpSearch.Controls.Add(this.label2);
            this.tpSearch.Controls.Add(this.tbExclude);
            this.tpSearch.Controls.Add(this.label6);
            this.tpSearch.Controls.Add(this.tbQuery);
            this.tpSearch.Location = new System.Drawing.Point(4, 29);
            this.tpSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpSearch.Name = "tpSearch";
            this.tpSearch.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpSearch.Size = new System.Drawing.Size(1588, 122);
            this.tpSearch.TabIndex = 1;
            this.tpSearch.Text = "Условия поиска";
            this.tpSearch.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(690, 5);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(108, 20);
            this.label14.TabIndex = 39;
            this.label14.Text = "Поставщики:";
            // 
            // clbSellers
            // 
            this.clbSellers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbSellers.Location = new System.Drawing.Point(694, 29);
            this.clbSellers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clbSellers.MultiColumn = true;
            this.clbSellers.Name = "clbSellers";
            this.clbSellers.Size = new System.Drawing.Size(600, 46);
            this.clbSellers.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Точное совпадение:";
            // 
            // tbExact
            // 
            this.tbExact.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbExact.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tbExact.Location = new System.Drawing.Point(15, 29);
            this.tbExact.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbExact.Multiline = true;
            this.tbExact.Name = "tbExact";
            this.tbExact.Size = new System.Drawing.Size(199, 75);
            this.tbExact.TabIndex = 0;
            this.tbExact.TextChanged += new System.EventHandler(this.SearchCondition_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 20);
            this.label2.TabIndex = 30;
            this.label2.Text = "Частичное совпадение:";
            // 
            // tbExclude
            // 
            this.tbExclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbExclude.ForeColor = System.Drawing.Color.Red;
            this.tbExclude.Location = new System.Drawing.Point(465, 29);
            this.tbExclude.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbExclude.Multiline = true;
            this.tbExclude.Name = "tbExclude";
            this.tbExclude.Size = new System.Drawing.Size(199, 75);
            this.tbExclude.TabIndex = 35;
            this.tbExclude.TextChanged += new System.EventHandler(this.SearchCondition_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(460, 5);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(153, 20);
            this.label6.TabIndex = 36;
            this.label6.Text = "Не содержит слов:";
            // 
            // tbQuery
            // 
            this.tbQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbQuery.Location = new System.Drawing.Point(240, 29);
            this.tbQuery.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbQuery.Multiline = true;
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.Size = new System.Drawing.Size(199, 75);
            this.tbQuery.TabIndex = 28;
            this.tbQuery.TextChanged += new System.EventHandler(this.SearchCondition_TextChanged);
            // 
            // tpExpert
            // 
            this.tpExpert.Controls.Add(this.label4);
            this.tpExpert.Controls.Add(this.tbExactExpert);
            this.tpExpert.Controls.Add(this.label8);
            this.tpExpert.Controls.Add(this.tbExcludeExpert);
            this.tpExpert.Controls.Add(this.label12);
            this.tpExpert.Controls.Add(this.tbQueryExpert);
            this.tpExpert.Location = new System.Drawing.Point(4, 29);
            this.tpExpert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpExpert.Name = "tpExpert";
            this.tpExpert.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpExpert.Size = new System.Drawing.Size(1586, 122);
            this.tpExpert.TabIndex = 2;
            this.tpExpert.Text = "Условия поиска экспертных оценок";
            this.tpExpert.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 5);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 20);
            this.label4.TabIndex = 38;
            this.label4.Text = "Точное совпадение:";
            // 
            // tbExactExpert
            // 
            this.tbExactExpert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbExactExpert.Location = new System.Drawing.Point(15, 29);
            this.tbExactExpert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbExactExpert.Multiline = true;
            this.tbExactExpert.Name = "tbExactExpert";
            this.tbExactExpert.Size = new System.Drawing.Size(199, 118);
            this.tbExactExpert.TabIndex = 37;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(236, 5);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(191, 20);
            this.label8.TabIndex = 40;
            this.label8.Text = "Частичное совпадение:";
            // 
            // tbExcludeExpert
            // 
            this.tbExcludeExpert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbExcludeExpert.Location = new System.Drawing.Point(465, 29);
            this.tbExcludeExpert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbExcludeExpert.Multiline = true;
            this.tbExcludeExpert.Name = "tbExcludeExpert";
            this.tbExcludeExpert.Size = new System.Drawing.Size(199, 118);
            this.tbExcludeExpert.TabIndex = 41;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(460, 5);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(153, 20);
            this.label12.TabIndex = 42;
            this.label12.Text = "Не содержит слов:";
            // 
            // tbQueryExpert
            // 
            this.tbQueryExpert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbQueryExpert.Location = new System.Drawing.Point(240, 29);
            this.tbQueryExpert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbQueryExpert.Multiline = true;
            this.tbQueryExpert.Name = "tbQueryExpert";
            this.tbQueryExpert.Size = new System.Drawing.Size(199, 118);
            this.tbQueryExpert.TabIndex = 39;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnSearch);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.udResultCount);
            this.panel5.Controls.Add(this.groupBox1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 431);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1596, 72);
            this.panel5.TabIndex = 40;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Location = new System.Drawing.Point(12, 11);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(201, 51);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Найти";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(226, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 20);
            this.label3.TabIndex = 32;
            this.label3.Text = "Показать не более:";
            // 
            // udResultCount
            // 
            this.udResultCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.udResultCount.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udResultCount.Location = new System.Drawing.Point(396, 26);
            this.udResultCount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.udResultCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.udResultCount.Name = "udResultCount";
            this.udResultCount.Size = new System.Drawing.Size(63, 26);
            this.udResultCount.TabIndex = 29;
            this.udResultCount.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbScreenshotLink);
            this.groupBox1.Controls.Add(this.cbOpenLink);
            this.groupBox1.Location = new System.Drawing.Point(507, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(495, 58);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "При двойном клике на ссылку";
            // 
            // cbScreenshotLink
            // 
            this.cbScreenshotLink.AutoSize = true;
            this.cbScreenshotLink.Location = new System.Drawing.Point(231, 29);
            this.cbScreenshotLink.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbScreenshotLink.Name = "cbScreenshotLink";
            this.cbScreenshotLink.Size = new System.Drawing.Size(258, 24);
            this.cbScreenshotLink.TabIndex = 1;
            this.cbScreenshotLink.Text = "сделать скриншот по ссылке";
            this.cbScreenshotLink.UseVisualStyleBackColor = true;
            // 
            // cbOpenLink
            // 
            this.cbOpenLink.AutoSize = true;
            this.cbOpenLink.Checked = true;
            this.cbOpenLink.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOpenLink.Location = new System.Drawing.Point(9, 29);
            this.cbOpenLink.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbOpenLink.Name = "cbOpenLink";
            this.cbOpenLink.Size = new System.Drawing.Size(191, 24);
            this.cbOpenLink.TabIndex = 0;
            this.cbOpenLink.Text = "показать в браузере";
            this.cbOpenLink.UseVisualStyleBackColor = true;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(0, 271);
            this.splitter2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(1596, 5);
            this.splitter2.TabIndex = 0;
            this.splitter2.TabStop = false;
            // 
            // gbNorm
            // 
            this.gbNorm.Controls.Add(this.pnlNorm);
            this.gbNorm.Controls.Add(this.panel2);
            this.gbNorm.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbNorm.Location = new System.Drawing.Point(0, 69);
            this.gbNorm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbNorm.Name = "gbNorm";
            this.gbNorm.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbNorm.Size = new System.Drawing.Size(1596, 202);
            this.gbNorm.TabIndex = 43;
            this.gbNorm.TabStop = false;
            this.gbNorm.Text = "Нормализация";
            // 
            // pnlNorm
            // 
            this.pnlNorm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNorm.Location = new System.Drawing.Point(4, 70);
            this.pnlNorm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlNorm.Name = "pnlNorm";
            this.pnlNorm.Size = new System.Drawing.Size(1588, 127);
            this.pnlNorm.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmbNorm);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 24);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1588, 46);
            this.panel2.TabIndex = 1;
            // 
            // cmbNorm
            // 
            this.cmbNorm.FormattingEnabled = true;
            this.cmbNorm.Location = new System.Drawing.Point(8, 5);
            this.cmbNorm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbNorm.Name = "cmbNorm";
            this.cmbNorm.Size = new System.Drawing.Size(835, 28);
            this.cmbNorm.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbNorm);
            this.groupBox3.Controls.Add(this.cbSearch);
            this.groupBox3.Controls.Add(this.cbExpert);
            this.groupBox3.Controls.Add(this.tbName);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(1596, 69);
            this.groupBox3.TabIndex = 39;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Наименование предмета закупки";
            // 
            // cbNorm
            // 
            this.cbNorm.AutoSize = true;
            this.cbNorm.Checked = true;
            this.cbNorm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNorm.Location = new System.Drawing.Point(1428, 29);
            this.cbNorm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbNorm.Name = "cbNorm";
            this.cbNorm.Size = new System.Drawing.Size(145, 24);
            this.cbNorm.TabIndex = 42;
            this.cbNorm.Text = "нормализация";
            this.cbNorm.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cbNorm.UseVisualStyleBackColor = true;
            this.cbNorm.CheckedChanged += new System.EventHandler(this.cbNorm_CheckedChanged);
            // 
            // cbSearch
            // 
            this.cbSearch.AutoSize = true;
            this.cbSearch.Checked = true;
            this.cbSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSearch.Location = new System.Drawing.Point(1120, 29);
            this.cbSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbSearch.Name = "cbSearch";
            this.cbSearch.Size = new System.Drawing.Size(78, 24);
            this.cbSearch.TabIndex = 41;
            this.cbSearch.Text = "поиск";
            this.cbSearch.UseVisualStyleBackColor = true;
            this.cbSearch.CheckedChanged += new System.EventHandler(this.cbSearch_CheckedChanged);
            // 
            // cbExpert
            // 
            this.cbExpert.AutoSize = true;
            this.cbExpert.Location = new System.Drawing.Point(1228, 29);
            this.cbExpert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbExpert.Name = "cbExpert";
            this.cbExpert.Size = new System.Drawing.Size(182, 24);
            this.cbExpert.TabIndex = 40;
            this.cbExpert.Text = "экспертные оценки";
            this.cbExpert.UseVisualStyleBackColor = true;
            this.cbExpert.CheckedChanged += new System.EventHandler(this.cbExpert_CheckedChanged);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(26, 25);
            this.tbName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(1056, 26);
            this.tbName.TabIndex = 33;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            this.tbName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbName_KeyDown);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 505);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1598, 5);
            this.splitter1.TabIndex = 30;
            this.splitter1.TabStop = false;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.dgvSearchResult);
            this.pnlSearch.Controls.Add(this.panel3);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearch.Location = new System.Drawing.Point(0, 510);
            this.pnlSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1598, 293);
            this.pnlSearch.TabIndex = 31;
            // 
            // dgvSearchResult
            // 
            this.dgvSearchResult.AllowUserToAddRows = false;
            this.dgvSearchResult.AllowUserToDeleteRows = false;
            this.dgvSearchResult.AllowUserToOrderColumns = true;
            this.dgvSearchResult.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSearchResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSearchResult.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSearchResult.Location = new System.Drawing.Point(0, 120);
            this.dgvSearchResult.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvSearchResult.MultiSelect = false;
            this.dgvSearchResult.Name = "dgvSearchResult";
            this.dgvSearchResult.Size = new System.Drawing.Size(1598, 173);
            this.dgvSearchResult.TabIndex = 29;
            this.dgvSearchResult.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearchResult_CellContentClick);
            this.dgvSearchResult.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            this.dgvSearchResult.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearchResult_CellValueChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnUpdatePrices);
            this.panel3.Controls.Add(this.btnHistory);
            this.panel3.Controls.Add(this.btnExcelExport);
            this.panel3.Controls.Add(this.lblLastUpdated);
            this.panel3.Controls.Add(this.lblSellerCount);
            this.panel3.Controls.Add(this.btnInvertSelection);
            this.panel3.Controls.Add(this.btnCalc);
            this.panel3.Controls.Add(this.btnSelectAll);
            this.panel3.Controls.Add(this.lblIndexDocCount);
            this.panel3.Controls.Add(this.lblSelectedSellerCount);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.lblSelectedAveragePrice);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.lblSelectedNameCount);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.lblAllNameCount);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.lblAllSellerCount);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1598, 120);
            this.panel3.TabIndex = 30;
            // 
            // btnUpdatePrices
            // 
            this.btnUpdatePrices.Enabled = false;
            this.btnUpdatePrices.Location = new System.Drawing.Point(1226, 74);
            this.btnUpdatePrices.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnUpdatePrices.Name = "btnUpdatePrices";
            this.btnUpdatePrices.Size = new System.Drawing.Size(140, 35);
            this.btnUpdatePrices.TabIndex = 45;
            this.btnUpdatePrices.Text = "Обновить цены";
            this.btnUpdatePrices.UseVisualStyleBackColor = true;
            this.btnUpdatePrices.Click += new System.EventHandler(this.btnUpdatePrices_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(1012, 74);
            this.btnHistory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(204, 35);
            this.btnHistory.TabIndex = 44;
            this.btnHistory.Text = "История";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.Location = new System.Drawing.Point(418, 75);
            this.btnExcelExport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExcelExport.Name = "btnExcelExport";
            this.btnExcelExport.Size = new System.Drawing.Size(204, 35);
            this.btnExcelExport.TabIndex = 43;
            this.btnExcelExport.Text = "в Excel";
            this.btnExcelExport.UseVisualStyleBackColor = true;
            this.btnExcelExport.Click += new System.EventHandler(this.btnExcelExport_Click);
            // 
            // lblLastUpdated
            // 
            this.lblLastUpdated.AutoSize = true;
            this.lblLastUpdated.Location = new System.Drawing.Point(1008, 49);
            this.lblLastUpdated.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLastUpdated.Name = "lblLastUpdated";
            this.lblLastUpdated.Size = new System.Drawing.Size(111, 20);
            this.lblLastUpdated.TabIndex = 42;
            this.lblLastUpdated.Text = "Обновлено: 0";
            // 
            // lblSellerCount
            // 
            this.lblSellerCount.AutoSize = true;
            this.lblSellerCount.Location = new System.Drawing.Point(1008, 29);
            this.lblSellerCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSellerCount.Name = "lblSellerCount";
            this.lblSellerCount.Size = new System.Drawing.Size(130, 20);
            this.lblSellerCount.TabIndex = 41;
            this.lblSellerCount.Text = "Поставщиков: 0";
            // 
            // btnInvertSelection
            // 
            this.btnInvertSelection.Location = new System.Drawing.Point(186, 75);
            this.btnInvertSelection.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnInvertSelection.Name = "btnInvertSelection";
            this.btnInvertSelection.Size = new System.Drawing.Size(224, 35);
            this.btnInvertSelection.TabIndex = 1;
            this.btnInvertSelection.Text = "Инвертировать отметку";
            this.btnInvertSelection.UseVisualStyleBackColor = true;
            this.btnInvertSelection.Click += new System.EventHandler(this.btnInvertSelection_Click);
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(702, 9);
            this.btnCalc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(216, 57);
            this.btnCalc.TabIndex = 14;
            this.btnCalc.Text = "Расчет";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Visible = false;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(22, 75);
            this.btnSelectAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(154, 35);
            this.btnSelectAll.TabIndex = 0;
            this.btnSelectAll.Text = "Отметить все";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // lblIndexDocCount
            // 
            this.lblIndexDocCount.AutoSize = true;
            this.lblIndexDocCount.Location = new System.Drawing.Point(1008, 9);
            this.lblIndexDocCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIndexDocCount.Name = "lblIndexDocCount";
            this.lblIndexDocCount.Size = new System.Drawing.Size(76, 20);
            this.lblIndexDocCount.TabIndex = 13;
            this.lblIndexDocCount.Text = "В базе: 0";
            // 
            // lblSelectedSellerCount
            // 
            this.lblSelectedSellerCount.AutoSize = true;
            this.lblSelectedSellerCount.Location = new System.Drawing.Point(615, 9);
            this.lblSelectedSellerCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedSellerCount.Name = "lblSelectedSellerCount";
            this.lblSelectedSellerCount.Size = new System.Drawing.Size(18, 20);
            this.lblSelectedSellerCount.TabIndex = 11;
            this.lblSelectedSellerCount.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(372, 9);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(230, 20);
            this.label11.TabIndex = 10;
            this.label11.Text = "Поставщиков по отмеченым:";
            // 
            // lblSelectedAveragePrice
            // 
            this.lblSelectedAveragePrice.AutoSize = true;
            this.lblSelectedAveragePrice.Location = new System.Drawing.Point(615, 49);
            this.lblSelectedAveragePrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedAveragePrice.Name = "lblSelectedAveragePrice";
            this.lblSelectedAveragePrice.Size = new System.Drawing.Size(18, 20);
            this.lblSelectedAveragePrice.TabIndex = 9;
            this.lblSelectedAveragePrice.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(424, 49);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(177, 20);
            this.label10.TabIndex = 8;
            this.label10.Text = "НМЦК  по отмеченым:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(450, 29);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(155, 20);
            this.label9.TabIndex = 7;
            this.label9.Text = "Записей отмечено:";
            // 
            // lblSelectedNameCount
            // 
            this.lblSelectedNameCount.AutoSize = true;
            this.lblSelectedNameCount.Location = new System.Drawing.Point(615, 29);
            this.lblSelectedNameCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedNameCount.Name = "lblSelectedNameCount";
            this.lblSelectedNameCount.Size = new System.Drawing.Size(18, 20);
            this.lblSelectedNameCount.TabIndex = 6;
            this.lblSelectedNameCount.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(100, 29);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "Записей найдено:";
            // 
            // lblAllNameCount
            // 
            this.lblAllNameCount.AutoSize = true;
            this.lblAllNameCount.Location = new System.Drawing.Point(256, 29);
            this.lblAllNameCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAllNameCount.Name = "lblAllNameCount";
            this.lblAllNameCount.Size = new System.Drawing.Size(18, 20);
            this.lblAllNameCount.TabIndex = 2;
            this.lblAllNameCount.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(221, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "Поставщиков по найденым:";
            // 
            // lblAllSellerCount
            // 
            this.lblAllSellerCount.AutoSize = true;
            this.lblAllSellerCount.Location = new System.Drawing.Point(256, 9);
            this.lblAllSellerCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAllSellerCount.Name = "lblAllSellerCount";
            this.lblAllSellerCount.Size = new System.Drawing.Size(18, 20);
            this.lblAllSellerCount.TabIndex = 0;
            this.lblAllSellerCount.Text = "0";
            // 
            // timerAnalyze
            // 
            this.timerAnalyze.Interval = 1200;
            this.timerAnalyze.Tick += new System.EventHandler(this.timerAnalyze_Tick);
            // 
            // gbExpert
            // 
            this.gbExpert.Controls.Add(this.dgvExpert);
            this.gbExpert.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbExpert.Location = new System.Drawing.Point(0, 808);
            this.gbExpert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbExpert.Name = "gbExpert";
            this.gbExpert.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbExpert.Size = new System.Drawing.Size(1598, 269);
            this.gbExpert.TabIndex = 31;
            this.gbExpert.TabStop = false;
            this.gbExpert.Text = "Экспертные оценки";
            this.gbExpert.Visible = false;
            // 
            // dgvExpert
            // 
            this.dgvExpert.AllowUserToAddRows = false;
            this.dgvExpert.AllowUserToDeleteRows = false;
            this.dgvExpert.AllowUserToOrderColumns = true;
            this.dgvExpert.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvExpert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvExpert.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvExpert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExpert.Location = new System.Drawing.Point(4, 24);
            this.dgvExpert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvExpert.MultiSelect = false;
            this.dgvExpert.Name = "dgvExpert";
            this.dgvExpert.ReadOnly = true;
            this.dgvExpert.Size = new System.Drawing.Size(1590, 240);
            this.dgvExpert.TabIndex = 0;
            this.dgvExpert.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExpert_CellContentDoubleClick);
            // 
            // splExpert
            // 
            this.splExpert.BackColor = System.Drawing.SystemColors.HotTrack;
            this.splExpert.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splExpert.Location = new System.Drawing.Point(0, 803);
            this.splExpert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splExpert.Name = "splExpert";
            this.splExpert.Size = new System.Drawing.Size(1598, 5);
            this.splExpert.TabIndex = 32;
            this.splExpert.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1598, 1077);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.splExpert);
            this.Controls.Add(this.gbExpert);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Аваланч - многопараметрический поиск";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpSearch.ResumeLayout(false);
            this.tpSearch.PerformLayout();
            this.tpExpert.ResumeLayout(false);
            this.tpExpert.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udResultCount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbNorm.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResult)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.gbExpert.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsExpert)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbExact;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.BindingSource bsQuery;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TextBox tbQuery;
        private System.Windows.Forms.NumericUpDown udResultCount;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblAllSellerCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbExclude;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblAllNameCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSelectedNameCount;
        private System.Windows.Forms.Label lblSelectedAveragePrice;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblSelectedSellerCount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblIndexDocCount;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Timer timerAnalyze;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbScreenshotLink;
        private System.Windows.Forms.CheckBox cbOpenLink;
        private System.Windows.Forms.DataGridView dgvSearchResult;
        private System.Windows.Forms.Button btnInvertSelection;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.BindingSource bsExpert;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpSearch;
        private System.Windows.Forms.TabPage tpExpert;
        private System.Windows.Forms.GroupBox gbExpert;
        private System.Windows.Forms.DataGridView dgvExpert;
        private System.Windows.Forms.Splitter splExpert;
        private System.Windows.Forms.CheckBox cbExpert;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbExactExpert;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbExcludeExpert;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbQueryExpert;
        private System.Windows.Forms.CheckBox cbSearch;
        private System.Windows.Forms.CheckedListBox clbSellers;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblSellerCount;
        private System.Windows.Forms.Label lblLastUpdated;
        private System.Windows.Forms.CheckBox cbNorm;
        private System.Windows.Forms.GroupBox gbNorm;
        private System.Windows.Forms.Button btnExcelExport;
        private System.Windows.Forms.ComboBox cmbNorm;
        private System.Windows.Forms.Panel pnlNorm;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnUpdatePrices;
    }
}

