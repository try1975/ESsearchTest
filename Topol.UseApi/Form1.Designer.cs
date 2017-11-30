namespace Topol.UseApi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pnlPacket = new System.Windows.Forms.Panel();
            this.gbPacketText = new System.Windows.Forms.GroupBox();
            this.tbTruItems = new System.Windows.Forms.TextBox();
            this.pnlLoadPacket = new System.Windows.Forms.Panel();
            this.btnLoad = new System.Windows.Forms.Button();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.pnlCallPacket = new System.Windows.Forms.Panel();
            this.cbElasticIndexName = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.gbPacketItems = new System.Windows.Forms.GroupBox();
            this.dgvPacketItems = new ADGV.AdvancedDataGridView();
            this.gbContentItems = new System.Windows.Forms.GroupBox();
            this.dgvContentItems = new ADGV.AdvancedDataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tcApiSelect = new System.Windows.Forms.TabControl();
            this.tpPacketTru = new System.Windows.Forms.TabPage();
            this.tpSingleTru = new System.Windows.Forms.TabPage();
            this.gbSource = new System.Windows.Forms.GroupBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlPacket.SuspendLayout();
            this.gbPacketText.SuspendLayout();
            this.pnlLoadPacket.SuspendLayout();
            this.pnlCallPacket.SuspendLayout();
            this.gbPacketItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacketItems)).BeginInit();
            this.gbContentItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContentItems)).BeginInit();
            this.panel2.SuspendLayout();
            this.tcApiSelect.SuspendLayout();
            this.tpPacketTru.SuspendLayout();
            this.tpSingleTru.SuspendLayout();
            this.gbSource.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel11.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPacket
            // 
            this.pnlPacket.Controls.Add(this.gbPacketText);
            this.pnlPacket.Controls.Add(this.pnlLoadPacket);
            this.pnlPacket.Controls.Add(this.pnlCallPacket);
            this.pnlPacket.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPacket.Location = new System.Drawing.Point(3, 3);
            this.pnlPacket.Name = "pnlPacket";
            this.pnlPacket.Size = new System.Drawing.Size(1200, 177);
            this.pnlPacket.TabIndex = 0;
            // 
            // gbPacketText
            // 
            this.gbPacketText.Controls.Add(this.tbTruItems);
            this.gbPacketText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPacketText.Location = new System.Drawing.Point(0, 42);
            this.gbPacketText.Name = "gbPacketText";
            this.gbPacketText.Size = new System.Drawing.Size(1200, 92);
            this.gbPacketText.TabIndex = 5;
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
            this.tbTruItems.Size = new System.Drawing.Size(1194, 73);
            this.tbTruItems.TabIndex = 1;
            this.tbTruItems.Text = resources.GetString("tbTruItems.Text");
            this.tbTruItems.WordWrap = false;
            // 
            // pnlLoadPacket
            // 
            this.pnlLoadPacket.Controls.Add(this.btnLoad);
            this.pnlLoadPacket.Controls.Add(this.tbFileName);
            this.pnlLoadPacket.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLoadPacket.Location = new System.Drawing.Point(0, 0);
            this.pnlLoadPacket.Name = "pnlLoadPacket";
            this.pnlLoadPacket.Size = new System.Drawing.Size(1200, 42);
            this.pnlLoadPacket.TabIndex = 4;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 10);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(250, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Загрузить ТРУ из файла и запросить API";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // tbFileName
            // 
            this.tbFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileName.Location = new System.Drawing.Point(281, 12);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(907, 20);
            this.tbFileName.TabIndex = 0;
            // 
            // pnlCallPacket
            // 
            this.pnlCallPacket.Controls.Add(this.button2);
            this.pnlCallPacket.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCallPacket.Location = new System.Drawing.Point(0, 134);
            this.pnlCallPacket.Name = "pnlCallPacket";
            this.pnlCallPacket.Size = new System.Drawing.Size(1200, 43);
            this.pnlCallPacket.TabIndex = 2;
            // 
            // cbElasticIndexName
            // 
            this.cbElasticIndexName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbElasticIndexName.FormattingEnabled = true;
            this.cbElasticIndexName.Items.AddRange(new object[] {
            "md5",
            "md_med",
            "md_prod",
            "gz",
            "internet"});
            this.cbElasticIndexName.Location = new System.Drawing.Point(19, 19);
            this.cbElasticIndexName.Name = "cbElasticIndexName";
            this.cbElasticIndexName.Size = new System.Drawing.Size(243, 21);
            this.cbElasticIndexName.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(250, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Запросить API c текстом пакета";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // gbPacketItems
            // 
            this.gbPacketItems.Controls.Add(this.dgvPacketItems);
            this.gbPacketItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPacketItems.Location = new System.Drawing.Point(0, 0);
            this.gbPacketItems.Name = "gbPacketItems";
            this.gbPacketItems.Size = new System.Drawing.Size(1214, 115);
            this.gbPacketItems.TabIndex = 4;
            this.gbPacketItems.TabStop = false;
            this.gbPacketItems.Text = "Список ТРУ";
            // 
            // dgvPacketItems
            // 
            this.dgvPacketItems.AllowUserToAddRows = false;
            this.dgvPacketItems.AllowUserToDeleteRows = false;
            this.dgvPacketItems.AllowUserToOrderColumns = true;
            this.dgvPacketItems.AutoGenerateContextFilters = true;
            this.dgvPacketItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPacketItems.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvPacketItems.DateWithTime = false;
            this.dgvPacketItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPacketItems.Location = new System.Drawing.Point(3, 16);
            this.dgvPacketItems.Name = "dgvPacketItems";
            this.dgvPacketItems.ReadOnly = true;
            this.dgvPacketItems.Size = new System.Drawing.Size(1208, 96);
            this.dgvPacketItems.TabIndex = 2;
            this.dgvPacketItems.TimeFilter = true;
            // 
            // gbContentItems
            // 
            this.gbContentItems.Controls.Add(this.dgvContentItems);
            this.gbContentItems.Controls.Add(this.panel4);
            this.gbContentItems.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbContentItems.Location = new System.Drawing.Point(0, 118);
            this.gbContentItems.Name = "gbContentItems";
            this.gbContentItems.Size = new System.Drawing.Size(1214, 274);
            this.gbContentItems.TabIndex = 5;
            this.gbContentItems.TabStop = false;
            this.gbContentItems.Text = "Результаты поиска";
            // 
            // dgvContentItems
            // 
            this.dgvContentItems.AllowUserToAddRows = false;
            this.dgvContentItems.AllowUserToDeleteRows = false;
            this.dgvContentItems.AllowUserToOrderColumns = true;
            this.dgvContentItems.AutoGenerateContextFilters = true;
            this.dgvContentItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContentItems.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvContentItems.DateWithTime = false;
            this.dgvContentItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvContentItems.Location = new System.Drawing.Point(3, 33);
            this.dgvContentItems.Name = "dgvContentItems";
            this.dgvContentItems.ReadOnly = true;
            this.dgvContentItems.Size = new System.Drawing.Size(1208, 238);
            this.dgvContentItems.TabIndex = 2;
            this.dgvContentItems.TimeFilter = true;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 16);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1208, 17);
            this.panel4.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel11);
            this.panel2.Controls.Add(this.gbSource);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1228, 684);
            this.panel2.TabIndex = 6;
            // 
            // tcApiSelect
            // 
            this.tcApiSelect.Controls.Add(this.tpPacketTru);
            this.tcApiSelect.Controls.Add(this.tpSingleTru);
            this.tcApiSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.tcApiSelect.Location = new System.Drawing.Point(3, 3);
            this.tcApiSelect.Name = "tcApiSelect";
            this.tcApiSelect.SelectedIndex = 0;
            this.tcApiSelect.Size = new System.Drawing.Size(1214, 207);
            this.tcApiSelect.TabIndex = 2;
            // 
            // tpPacketTru
            // 
            this.tpPacketTru.Controls.Add(this.pnlPacket);
            this.tpPacketTru.Location = new System.Drawing.Point(4, 22);
            this.tpPacketTru.Name = "tpPacketTru";
            this.tpPacketTru.Padding = new System.Windows.Forms.Padding(3);
            this.tpPacketTru.Size = new System.Drawing.Size(1206, 181);
            this.tpPacketTru.TabIndex = 0;
            this.tpPacketTru.Text = "Пакет ТРУ";
            this.tpPacketTru.UseVisualStyleBackColor = true;
            // 
            // tpSingleTru
            // 
            this.tpSingleTru.Controls.Add(this.panel1);
            this.tpSingleTru.Location = new System.Drawing.Point(4, 22);
            this.tpSingleTru.Name = "tpSingleTru";
            this.tpSingleTru.Padding = new System.Windows.Forms.Padding(3);
            this.tpSingleTru.Size = new System.Drawing.Size(1206, 181);
            this.tpSingleTru.TabIndex = 1;
            this.tpSingleTru.Text = "Один ТРУ";
            this.tpSingleTru.UseVisualStyleBackColor = true;
            // 
            // gbSource
            // 
            this.gbSource.Controls.Add(this.linkLabel1);
            this.gbSource.Controls.Add(this.cbElasticIndexName);
            this.gbSource.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbSource.Location = new System.Drawing.Point(0, 0);
            this.gbSource.Name = "gbSource";
            this.gbSource.Size = new System.Drawing.Size(1228, 50);
            this.gbSource.TabIndex = 2;
            this.gbSource.TabStop = false;
            this.gbSource.Text = "Выбор источника";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.button1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 83);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1200, 43);
            this.panel6.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(250, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Запросить API";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Controls.Add(this.panel9);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1200, 83);
            this.panel7.TabIndex = 4;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.comboBox1);
            this.panel8.Controls.Add(this.label3);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 32);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1200, 32);
            this.panel8.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.textBox2);
            this.panel9.Controls.Add(this.label2);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(1200, 32);
            this.panel9.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Наименование";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Нормализатор";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(104, 6);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(296, 20);
            this.textBox2.TabIndex = 2;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "лек_средства:основной"});
            this.comboBox1.Location = new System.Drawing.Point(104, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(296, 21);
            this.comboBox1.TabIndex = 4;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(288, 22);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(241, 13);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Описание API: http://144.76.54.166:52620/help";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 115);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(1214, 3);
            this.splitter2.TabIndex = 0;
            this.splitter2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 131);
            this.panel1.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gbPacketItems);
            this.panel3.Controls.Add(this.splitter2);
            this.panel3.Controls.Add(this.gbContentItems);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 210);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1214, 392);
            this.panel3.TabIndex = 3;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.tabControl1);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(0, 50);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(1228, 634);
            this.panel11.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1228, 634);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.splitter1);
            this.tabPage1.Controls.Add(this.tcApiSelect);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1220, 608);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(3, 602);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1214, 3);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 684);
            this.Controls.Add(this.panel2);
            this.Name = "Form1";
            this.Text = "Пример использования API Тополь";
            this.pnlPacket.ResumeLayout(false);
            this.gbPacketText.ResumeLayout(false);
            this.gbPacketText.PerformLayout();
            this.pnlLoadPacket.ResumeLayout(false);
            this.pnlLoadPacket.PerformLayout();
            this.pnlCallPacket.ResumeLayout(false);
            this.gbPacketItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacketItems)).EndInit();
            this.gbContentItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvContentItems)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tcApiSelect.ResumeLayout(false);
            this.tpPacketTru.ResumeLayout(false);
            this.tpSingleTru.ResumeLayout(false);
            this.gbSource.ResumeLayout(false);
            this.gbSource.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPacket;
        private System.Windows.Forms.Panel pnlCallPacket;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel pnlLoadPacket;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.GroupBox gbPacketText;
        private System.Windows.Forms.TextBox tbTruItems;
        private System.Windows.Forms.GroupBox gbPacketItems;
        private ADGV.AdvancedDataGridView dgvPacketItems;
        private System.Windows.Forms.GroupBox gbContentItems;
        private ADGV.AdvancedDataGridView dgvContentItems;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbElasticIndexName;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabControl tcApiSelect;
        private System.Windows.Forms.TabPage tpPacketTru;
        private System.Windows.Forms.TabPage tpSingleTru;
        private System.Windows.Forms.GroupBox gbSource;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

