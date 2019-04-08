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
            this.btnGzDocSearch = new System.Windows.Forms.Button();
            this.tbGzDocSearchKey = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvItems = new ADGV.AdvancedDataGridView();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnWordTable = new System.Windows.Forms.Button();
            this.panel28.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel28
            // 
            this.panel28.Controls.Add(this.btnGzDocSearch);
            this.panel28.Controls.Add(this.tbGzDocSearchKey);
            this.panel28.Controls.Add(this.label22);
            this.panel28.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel28.Location = new System.Drawing.Point(0, 0);
            this.panel28.Name = "panel28";
            this.panel28.Size = new System.Drawing.Size(573, 50);
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
            this.panel1.Controls.Add(this.dgvItems);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(573, 437);
            this.panel1.TabIndex = 2;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItems.AutoGenerateContextFilters = true;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.DateWithTime = false;
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Location = new System.Drawing.Point(37, 0);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.Size = new System.Drawing.Size(536, 437);
            this.dgvItems.TabIndex = 2;
            this.dgvItems.TimeFilter = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnWordTable);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(37, 437);
            this.panel2.TabIndex = 3;
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
            // GzDocSearchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel28);
            this.Name = "GzDocSearchControl";
            this.Size = new System.Drawing.Size(573, 487);
            this.panel28.ResumeLayout(false);
            this.panel28.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel28;
        private System.Windows.Forms.Button btnGzDocSearch;
        private System.Windows.Forms.TextBox tbGzDocSearchKey;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel1;
        private ADGV.AdvancedDataGridView dgvItems;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnWordTable;
    }
}
