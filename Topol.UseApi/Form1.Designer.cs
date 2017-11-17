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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbTruItems = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnLoad = new System.Windows.Forms.Button();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvPacketItems = new ADGV.AdvancedDataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvContentItems = new ADGV.AdvancedDataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacketItems)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContentItems)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1228, 177);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbTruItems);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1228, 92);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Пакет ТРУ";
            // 
            // tbTruItems
            // 
            this.tbTruItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTruItems.Location = new System.Drawing.Point(3, 16);
            this.tbTruItems.Multiline = true;
            this.tbTruItems.Name = "tbTruItems";
            this.tbTruItems.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbTruItems.Size = new System.Drawing.Size(1222, 73);
            this.tbTruItems.TabIndex = 1;
            this.tbTruItems.Text = resources.GetString("tbTruItems.Text");
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnLoad);
            this.panel5.Controls.Add(this.tbFileName);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1228, 42);
            this.panel5.TabIndex = 4;
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
            this.tbFileName.Size = new System.Drawing.Size(935, 20);
            this.tbFileName.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 134);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1228, 43);
            this.panel3.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(250, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Запросить APi c текстом пакета";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvPacketItems);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 177);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1228, 138);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Список ТРУ из пакетов";
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
            this.dgvPacketItems.Size = new System.Drawing.Size(1222, 119);
            this.dgvPacketItems.TabIndex = 2;
            this.dgvPacketItems.TimeFilter = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvContentItems);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 315);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1228, 277);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Результаты поиска по ТРУ";
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
            this.dgvContentItems.Location = new System.Drawing.Point(3, 16);
            this.dgvContentItems.Name = "dgvContentItems";
            this.dgvContentItems.ReadOnly = true;
            this.dgvContentItems.Size = new System.Drawing.Size(1222, 258);
            this.dgvContentItems.TabIndex = 2;
            this.dgvContentItems.TimeFilter = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1228, 315);
            this.panel2.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 592);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacketItems)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvContentItems)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbTruItems;
        private System.Windows.Forms.GroupBox groupBox2;
        private ADGV.AdvancedDataGridView dgvPacketItems;
        private System.Windows.Forms.GroupBox groupBox3;
        private ADGV.AdvancedDataGridView dgvContentItems;
        private System.Windows.Forms.Panel panel2;
    }
}

