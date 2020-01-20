namespace Topol.UseApi.Controls
{
    partial class MonitoringControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbMonitiringSchedulePrices = new System.Windows.Forms.GroupBox();
            this.advancedDataGridView2 = new ADGV.AdvancedDataGridView();
            this.panel30 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.gbMonitoringSchedule = new System.Windows.Forms.GroupBox();
            this.dgvShedules = new ADGV.AdvancedDataGridView();
            this.panel28 = new System.Windows.Forms.Panel();
            this.btnGetSchedules = new System.Windows.Forms.Button();
            this.btnAddSchedule = new System.Windows.Forms.Button();
            this.gbMonitiringSchedulePrices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView2)).BeginInit();
            this.gbMonitoringSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShedules)).BeginInit();
            this.panel28.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMonitiringSchedulePrices
            // 
            this.gbMonitiringSchedulePrices.Controls.Add(this.advancedDataGridView2);
            this.gbMonitiringSchedulePrices.Controls.Add(this.panel30);
            this.gbMonitiringSchedulePrices.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbMonitiringSchedulePrices.Location = new System.Drawing.Point(0, 394);
            this.gbMonitiringSchedulePrices.Name = "gbMonitiringSchedulePrices";
            this.gbMonitiringSchedulePrices.Size = new System.Drawing.Size(886, 268);
            this.gbMonitiringSchedulePrices.TabIndex = 2;
            this.gbMonitiringSchedulePrices.TabStop = false;
            this.gbMonitiringSchedulePrices.Text = "Цены по расписанию";
            // 
            // advancedDataGridView2
            // 
            this.advancedDataGridView2.AllowUserToAddRows = false;
            this.advancedDataGridView2.AllowUserToDeleteRows = false;
            this.advancedDataGridView2.AutoGenerateContextFilters = true;
            this.advancedDataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.advancedDataGridView2.DateWithTime = false;
            this.advancedDataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advancedDataGridView2.Location = new System.Drawing.Point(3, 63);
            this.advancedDataGridView2.Name = "advancedDataGridView2";
            this.advancedDataGridView2.ReadOnly = true;
            this.advancedDataGridView2.Size = new System.Drawing.Size(880, 202);
            this.advancedDataGridView2.TabIndex = 2;
            this.advancedDataGridView2.TimeFilter = false;
            // 
            // panel30
            // 
            this.panel30.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel30.Location = new System.Drawing.Point(3, 16);
            this.panel30.Name = "panel30";
            this.panel30.Size = new System.Drawing.Size(880, 47);
            this.panel30.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 391);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(886, 3);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // gbMonitoringSchedule
            // 
            this.gbMonitoringSchedule.Controls.Add(this.dgvShedules);
            this.gbMonitoringSchedule.Controls.Add(this.panel28);
            this.gbMonitoringSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMonitoringSchedule.Location = new System.Drawing.Point(0, 0);
            this.gbMonitoringSchedule.Name = "gbMonitoringSchedule";
            this.gbMonitoringSchedule.Size = new System.Drawing.Size(886, 391);
            this.gbMonitoringSchedule.TabIndex = 4;
            this.gbMonitoringSchedule.TabStop = false;
            this.gbMonitoringSchedule.Text = "Расписание";
            // 
            // dgvShedules
            // 
            this.dgvShedules.AllowUserToAddRows = false;
            this.dgvShedules.AllowUserToDeleteRows = false;
            this.dgvShedules.AutoGenerateContextFilters = true;
            this.dgvShedules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShedules.DateWithTime = false;
            this.dgvShedules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShedules.Location = new System.Drawing.Point(3, 56);
            this.dgvShedules.Name = "dgvShedules";
            this.dgvShedules.ReadOnly = true;
            this.dgvShedules.Size = new System.Drawing.Size(880, 332);
            this.dgvShedules.TabIndex = 1;
            this.dgvShedules.TimeFilter = false;
            // 
            // panel28
            // 
            this.panel28.Controls.Add(this.btnAddSchedule);
            this.panel28.Controls.Add(this.btnGetSchedules);
            this.panel28.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel28.Location = new System.Drawing.Point(3, 16);
            this.panel28.Name = "panel28";
            this.panel28.Size = new System.Drawing.Size(880, 40);
            this.panel28.TabIndex = 0;
            // 
            // btnGetSchedules
            // 
            this.btnGetSchedules.Location = new System.Drawing.Point(43, 11);
            this.btnGetSchedules.Name = "btnGetSchedules";
            this.btnGetSchedules.Size = new System.Drawing.Size(75, 23);
            this.btnGetSchedules.TabIndex = 0;
            this.btnGetSchedules.Text = "получить список";
            this.btnGetSchedules.UseVisualStyleBackColor = true;
            // 
            // btnAddSchedule
            // 
            this.btnAddSchedule.Location = new System.Drawing.Point(154, 11);
            this.btnAddSchedule.Name = "btnAddSchedule";
            this.btnAddSchedule.Size = new System.Drawing.Size(75, 23);
            this.btnAddSchedule.TabIndex = 1;
            this.btnAddSchedule.Text = "добавить";
            this.btnAddSchedule.UseVisualStyleBackColor = true;
            // 
            // MonitoringControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbMonitoringSchedule);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.gbMonitiringSchedulePrices);
            this.Name = "MonitoringControl";
            this.Size = new System.Drawing.Size(886, 662);
            this.gbMonitiringSchedulePrices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView2)).EndInit();
            this.gbMonitoringSchedule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShedules)).EndInit();
            this.panel28.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbMonitiringSchedulePrices;
        private ADGV.AdvancedDataGridView advancedDataGridView2;
        private System.Windows.Forms.Panel panel30;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.GroupBox gbMonitoringSchedule;
        private ADGV.AdvancedDataGridView dgvShedules;
        private System.Windows.Forms.Panel panel28;
        private System.Windows.Forms.Button btnGetSchedules;
        private System.Windows.Forms.Button btnAddSchedule;
    }
}
