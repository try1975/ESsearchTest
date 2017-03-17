namespace ESsearchTest
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
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbScreenshotLink = new System.Windows.Forms.CheckBox();
            this.cbOpenLink = new System.Windows.Forms.CheckBox();
            this.gbNorm = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbNorm = new System.Windows.Forms.CheckBox();
            this.cbSearch = new System.Windows.Forms.CheckBox();
            this.cbExpert = new System.Windows.Forms.CheckBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.dgvSearchResult = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.panel1.Controls.Add(this.gbNorm);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1065, 329);
            this.panel1.TabIndex = 26;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpSearch);
            this.tabControl1.Controls.Add(this.tpExpert);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 166);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1063, 114);
            this.tabControl1.TabIndex = 38;
            // 
            // tpSearch
            // 
            this.tpSearch.Controls.Add(this.label14);
            this.tpSearch.Controls.Add(this.clbSellers);
            this.tpSearch.Controls.Add(this.label1);
            this.tpSearch.Controls.Add(this.tbExact);
            this.tpSearch.Controls.Add(this.label2);
            this.tpSearch.Controls.Add(this.tbExclude);
            this.tpSearch.Controls.Add(this.label6);
            this.tpSearch.Controls.Add(this.tbQuery);
            this.tpSearch.Location = new System.Drawing.Point(4, 22);
            this.tpSearch.Name = "tpSearch";
            this.tpSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tpSearch.Size = new System.Drawing.Size(1055, 88);
            this.tpSearch.TabIndex = 1;
            this.tpSearch.Text = "Условия поиска";
            this.tpSearch.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(460, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 13);
            this.label14.TabIndex = 39;
            this.label14.Text = "Поставщики:";
            // 
            // clbSellers
            // 
            this.clbSellers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.clbSellers.FormattingEnabled = true;
            this.clbSellers.Location = new System.Drawing.Point(463, 21);
            this.clbSellers.Name = "clbSellers";
            this.clbSellers.Size = new System.Drawing.Size(255, 34);
            this.clbSellers.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Точное совпадение:";
            // 
            // tbExact
            // 
            this.tbExact.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbExact.Location = new System.Drawing.Point(10, 19);
            this.tbExact.Multiline = true;
            this.tbExact.Name = "tbExact";
            this.tbExact.Size = new System.Drawing.Size(134, 63);
            this.tbExact.TabIndex = 0;
            this.tbExact.TextChanged += new System.EventHandler(this.SearchCondition_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Частичное совпадение:";
            // 
            // tbExclude
            // 
            this.tbExclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbExclude.Location = new System.Drawing.Point(310, 19);
            this.tbExclude.Multiline = true;
            this.tbExclude.Name = "tbExclude";
            this.tbExclude.Size = new System.Drawing.Size(134, 63);
            this.tbExclude.TabIndex = 35;
            this.tbExclude.TextChanged += new System.EventHandler(this.SearchCondition_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(307, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Не содержит слов:";
            // 
            // tbQuery
            // 
            this.tbQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbQuery.Location = new System.Drawing.Point(160, 19);
            this.tbQuery.Multiline = true;
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.Size = new System.Drawing.Size(134, 63);
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
            this.tpExpert.Location = new System.Drawing.Point(4, 22);
            this.tpExpert.Name = "tpExpert";
            this.tpExpert.Padding = new System.Windows.Forms.Padding(3);
            this.tpExpert.Size = new System.Drawing.Size(1055, 88);
            this.tpExpert.TabIndex = 2;
            this.tpExpert.Text = "Условия поиска экспертных оценок";
            this.tpExpert.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Точное совпадение:";
            // 
            // tbExactExpert
            // 
            this.tbExactExpert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbExactExpert.Location = new System.Drawing.Point(10, 19);
            this.tbExactExpert.Multiline = true;
            this.tbExactExpert.Name = "tbExactExpert";
            this.tbExactExpert.Size = new System.Drawing.Size(134, 78);
            this.tbExactExpert.TabIndex = 37;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(157, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(127, 13);
            this.label8.TabIndex = 40;
            this.label8.Text = "Частичное совпадение:";
            // 
            // tbExcludeExpert
            // 
            this.tbExcludeExpert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbExcludeExpert.Location = new System.Drawing.Point(310, 19);
            this.tbExcludeExpert.Multiline = true;
            this.tbExcludeExpert.Name = "tbExcludeExpert";
            this.tbExcludeExpert.Size = new System.Drawing.Size(134, 78);
            this.tbExcludeExpert.TabIndex = 41;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(307, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 13);
            this.label12.TabIndex = 42;
            this.label12.Text = "Не содержит слов:";
            // 
            // tbQueryExpert
            // 
            this.tbQueryExpert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbQueryExpert.Location = new System.Drawing.Point(160, 19);
            this.tbQueryExpert.Multiline = true;
            this.tbQueryExpert.Name = "tbQueryExpert";
            this.tbQueryExpert.Size = new System.Drawing.Size(134, 78);
            this.tbQueryExpert.TabIndex = 39;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnSearch);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.numericUpDown1);
            this.panel5.Controls.Add(this.groupBox1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 280);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1063, 47);
            this.panel5.TabIndex = 40;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Location = new System.Drawing.Point(8, 7);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(134, 33);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Найти";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(151, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Показать не более:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown1.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(264, 17);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(42, 20);
            this.numericUpDown1.TabIndex = 29;
            this.numericUpDown1.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbScreenshotLink);
            this.groupBox1.Controls.Add(this.cbOpenLink);
            this.groupBox1.Location = new System.Drawing.Point(338, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(330, 38);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "При двойном клике на ссылку";
            // 
            // cbScreenshotLink
            // 
            this.cbScreenshotLink.AutoSize = true;
            this.cbScreenshotLink.Location = new System.Drawing.Point(154, 19);
            this.cbScreenshotLink.Name = "cbScreenshotLink";
            this.cbScreenshotLink.Size = new System.Drawing.Size(175, 17);
            this.cbScreenshotLink.TabIndex = 1;
            this.cbScreenshotLink.Text = "сделать скриншот по ссылке";
            this.cbScreenshotLink.UseVisualStyleBackColor = true;
            // 
            // cbOpenLink
            // 
            this.cbOpenLink.AutoSize = true;
            this.cbOpenLink.Checked = true;
            this.cbOpenLink.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOpenLink.Location = new System.Drawing.Point(6, 19);
            this.cbOpenLink.Name = "cbOpenLink";
            this.cbOpenLink.Size = new System.Drawing.Size(132, 17);
            this.cbOpenLink.TabIndex = 0;
            this.cbOpenLink.Text = "показать в браузере";
            this.cbOpenLink.UseVisualStyleBackColor = true;
            // 
            // gbNorm
            // 
            this.gbNorm.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbNorm.Location = new System.Drawing.Point(0, 51);
            this.gbNorm.Name = "gbNorm";
            this.gbNorm.Size = new System.Drawing.Size(1063, 115);
            this.gbNorm.TabIndex = 43;
            this.gbNorm.TabStop = false;
            this.gbNorm.Text = "Нормализация";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbNorm);
            this.groupBox3.Controls.Add(this.cbSearch);
            this.groupBox3.Controls.Add(this.cbExpert);
            this.groupBox3.Controls.Add(this.tbName);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1063, 51);
            this.groupBox3.TabIndex = 39;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Введите наименование";
            // 
            // cbNorm
            // 
            this.cbNorm.AutoSize = true;
            this.cbNorm.Checked = true;
            this.cbNorm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNorm.Location = new System.Drawing.Point(952, 19);
            this.cbNorm.Name = "cbNorm";
            this.cbNorm.Size = new System.Drawing.Size(100, 17);
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
            this.cbSearch.Location = new System.Drawing.Point(747, 19);
            this.cbSearch.Name = "cbSearch";
            this.cbSearch.Size = new System.Drawing.Size(56, 17);
            this.cbSearch.TabIndex = 41;
            this.cbSearch.Text = "поиск";
            this.cbSearch.UseVisualStyleBackColor = true;
            this.cbSearch.CheckedChanged += new System.EventHandler(this.cbSearch_CheckedChanged);
            // 
            // cbExpert
            // 
            this.cbExpert.AutoSize = true;
            this.cbExpert.Location = new System.Drawing.Point(819, 19);
            this.cbExpert.Name = "cbExpert";
            this.cbExpert.Size = new System.Drawing.Size(126, 17);
            this.cbExpert.TabIndex = 40;
            this.cbExpert.Text = "экспертные оценки";
            this.cbExpert.UseVisualStyleBackColor = true;
            this.cbExpert.CheckedChanged += new System.EventHandler(this.cbExpert_CheckedChanged);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(17, 16);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(705, 20);
            this.tbName.TabIndex = 33;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            this.tbName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbName_KeyDown);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 329);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1065, 3);
            this.splitter1.TabIndex = 30;
            this.splitter1.TabStop = false;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.dgvSearchResult);
            this.pnlSearch.Controls.Add(this.panel3);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearch.Location = new System.Drawing.Point(0, 332);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1065, 190);
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
            this.dgvSearchResult.Location = new System.Drawing.Point(0, 78);
            this.dgvSearchResult.MultiSelect = false;
            this.dgvSearchResult.Name = "dgvSearchResult";
            this.dgvSearchResult.Size = new System.Drawing.Size(1065, 112);
            this.dgvSearchResult.TabIndex = 29;
            this.dgvSearchResult.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearchResult_CellContentClick);
            this.dgvSearchResult.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            this.dgvSearchResult.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSearchResult_CellValueChanged);
            // 
            // panel3
            // 
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
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1065, 78);
            this.panel3.TabIndex = 30;
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.Location = new System.Drawing.Point(279, 49);
            this.btnExcelExport.Name = "btnExcelExport";
            this.btnExcelExport.Size = new System.Drawing.Size(136, 23);
            this.btnExcelExport.TabIndex = 43;
            this.btnExcelExport.Text = "в Excel";
            this.btnExcelExport.UseVisualStyleBackColor = true;
            this.btnExcelExport.Click += new System.EventHandler(this.btnExcelExport_Click);
            // 
            // lblLastUpdated
            // 
            this.lblLastUpdated.AutoSize = true;
            this.lblLastUpdated.Location = new System.Drawing.Point(672, 32);
            this.lblLastUpdated.Name = "lblLastUpdated";
            this.lblLastUpdated.Size = new System.Drawing.Size(75, 13);
            this.lblLastUpdated.TabIndex = 42;
            this.lblLastUpdated.Text = "Обновлено: 0";
            // 
            // lblSellerCount
            // 
            this.lblSellerCount.AutoSize = true;
            this.lblSellerCount.Location = new System.Drawing.Point(672, 19);
            this.lblSellerCount.Name = "lblSellerCount";
            this.lblSellerCount.Size = new System.Drawing.Size(89, 13);
            this.lblSellerCount.TabIndex = 41;
            this.lblSellerCount.Text = "Поставщиков: 0";
            // 
            // btnInvertSelection
            // 
            this.btnInvertSelection.Location = new System.Drawing.Point(124, 49);
            this.btnInvertSelection.Name = "btnInvertSelection";
            this.btnInvertSelection.Size = new System.Drawing.Size(149, 23);
            this.btnInvertSelection.TabIndex = 1;
            this.btnInvertSelection.Text = "Инвертировать отметку";
            this.btnInvertSelection.UseVisualStyleBackColor = true;
            this.btnInvertSelection.Click += new System.EventHandler(this.btnInvertSelection_Click);
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(468, 6);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(144, 37);
            this.btnCalc.TabIndex = 14;
            this.btnCalc.Text = "Расчет";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Visible = false;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(15, 49);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(103, 23);
            this.btnSelectAll.TabIndex = 0;
            this.btnSelectAll.Text = "Отметить все";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // lblIndexDocCount
            // 
            this.lblIndexDocCount.AutoSize = true;
            this.lblIndexDocCount.Location = new System.Drawing.Point(672, 6);
            this.lblIndexDocCount.Name = "lblIndexDocCount";
            this.lblIndexDocCount.Size = new System.Drawing.Size(53, 13);
            this.lblIndexDocCount.TabIndex = 13;
            this.lblIndexDocCount.Text = "В базе: 0";
            // 
            // lblSelectedSellerCount
            // 
            this.lblSelectedSellerCount.AutoSize = true;
            this.lblSelectedSellerCount.Location = new System.Drawing.Point(410, 6);
            this.lblSelectedSellerCount.Name = "lblSelectedSellerCount";
            this.lblSelectedSellerCount.Size = new System.Drawing.Size(13, 13);
            this.lblSelectedSellerCount.TabIndex = 11;
            this.lblSelectedSellerCount.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(248, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(156, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Поставщиков по отмеченым:";
            // 
            // lblSelectedAveragePrice
            // 
            this.lblSelectedAveragePrice.AutoSize = true;
            this.lblSelectedAveragePrice.Location = new System.Drawing.Point(410, 32);
            this.lblSelectedAveragePrice.Name = "lblSelectedAveragePrice";
            this.lblSelectedAveragePrice.Size = new System.Drawing.Size(13, 13);
            this.lblSelectedAveragePrice.TabIndex = 9;
            this.lblSelectedAveragePrice.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(283, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "НМЦК  по отмеченым:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(300, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Записей отмечено:";
            // 
            // lblSelectedNameCount
            // 
            this.lblSelectedNameCount.AutoSize = true;
            this.lblSelectedNameCount.Location = new System.Drawing.Point(410, 19);
            this.lblSelectedNameCount.Name = "lblSelectedNameCount";
            this.lblSelectedNameCount.Size = new System.Drawing.Size(13, 13);
            this.lblSelectedNameCount.TabIndex = 6;
            this.lblSelectedNameCount.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(67, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Записей найдено:";
            // 
            // lblAllNameCount
            // 
            this.lblAllNameCount.AutoSize = true;
            this.lblAllNameCount.Location = new System.Drawing.Point(171, 19);
            this.lblAllNameCount.Name = "lblAllNameCount";
            this.lblAllNameCount.Size = new System.Drawing.Size(13, 13);
            this.lblAllNameCount.TabIndex = 2;
            this.lblAllNameCount.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Поставщиков по найденым:";
            // 
            // lblAllSellerCount
            // 
            this.lblAllSellerCount.AutoSize = true;
            this.lblAllSellerCount.Location = new System.Drawing.Point(171, 6);
            this.lblAllSellerCount.Name = "lblAllSellerCount";
            this.lblAllSellerCount.Size = new System.Drawing.Size(13, 13);
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
            this.gbExpert.Location = new System.Drawing.Point(0, 525);
            this.gbExpert.Name = "gbExpert";
            this.gbExpert.Size = new System.Drawing.Size(1065, 175);
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
            this.dgvExpert.Location = new System.Drawing.Point(3, 16);
            this.dgvExpert.MultiSelect = false;
            this.dgvExpert.Name = "dgvExpert";
            this.dgvExpert.ReadOnly = true;
            this.dgvExpert.Size = new System.Drawing.Size(1059, 156);
            this.dgvExpert.TabIndex = 0;
            this.dgvExpert.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExpert_CellContentDoubleClick);
            // 
            // splExpert
            // 
            this.splExpert.BackColor = System.Drawing.SystemColors.HotTrack;
            this.splExpert.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splExpert.Location = new System.Drawing.Point(0, 522);
            this.splExpert.Name = "splExpert";
            this.splExpert.Size = new System.Drawing.Size(1065, 3);
            this.splExpert.TabIndex = 32;
            this.splExpert.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 700);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.splExpert);
            this.Controls.Add(this.gbExpert);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.NumericUpDown numericUpDown1;
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
    }
}

