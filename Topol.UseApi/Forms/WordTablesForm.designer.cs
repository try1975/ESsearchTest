namespace Topol.UseApi.Forms
{
    partial class WordTablesForm
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExcelExport = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvTable = new ADGV.AdvancedDataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.tbGzDocSearchKey = new System.Windows.Forms.TextBox();
            this.btnGzDocSearch = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnGzDocSearch);
            this.panel1.Controls.Add(this.tbGzDocSearchKey);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnExcelExport);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 49);
            this.panel1.TabIndex = 0;
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.Location = new System.Drawing.Point(24, 12);
            this.btnExcelExport.Name = "btnExcelExport";
            this.btnExcelExport.Size = new System.Drawing.Size(75, 23);
            this.btnExcelExport.TabIndex = 0;
            this.btnExcelExport.Text = "в Excel";
            this.btnExcelExport.UseVisualStyleBackColor = true;
            this.btnExcelExport.Click += new System.EventHandler(this.btnExcelExport_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvTable);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 401);
            this.panel2.TabIndex = 1;
            // 
            // dgvTable
            // 
            this.dgvTable.AutoGenerateContextFilters = true;
            this.dgvTable.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTable.DateWithTime = false;
            this.dgvTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTable.Location = new System.Drawing.Point(0, 0);
            this.dgvTable.Name = "dgvTable";
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTable.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTable.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTable.Size = new System.Drawing.Size(800, 401);
            this.dgvTable.TabIndex = 0;
            this.dgvTable.TimeFilter = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Строка поиска";
            // 
            // tbGzDocSearchKey
            // 
            this.tbGzDocSearchKey.Location = new System.Drawing.Point(216, 14);
            this.tbGzDocSearchKey.Name = "tbGzDocSearchKey";
            this.tbGzDocSearchKey.Size = new System.Drawing.Size(256, 20);
            this.tbGzDocSearchKey.TabIndex = 2;
            // 
            // btnGzDocSearch
            // 
            this.btnGzDocSearch.Location = new System.Drawing.Point(478, 12);
            this.btnGzDocSearch.Name = "btnGzDocSearch";
            this.btnGzDocSearch.Size = new System.Drawing.Size(75, 23);
            this.btnGzDocSearch.TabIndex = 3;
            this.btnGzDocSearch.Text = "Поиск";
            this.btnGzDocSearch.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(559, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Искать дальше";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // WordTablesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "WordTablesForm";
            this.Text = "Таблицы";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private ADGV.AdvancedDataGridView dgvTable;
        private System.Windows.Forms.Button btnExcelExport;
        private System.Windows.Forms.TextBox tbGzDocSearchKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGzDocSearch;
        private System.Windows.Forms.Button button1;
    }
}

