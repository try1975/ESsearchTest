using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ESsearchTest
{
    public partial class PriceHistoryForm : Form
    {
        public PriceHistoryForm(DataTable dt)
        {
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            var columnName = dt.Columns[0].ColumnName;
            Text = $"Диаграмма: {columnName}";

            bsQuery.DataSource = dt;
            dgvSearchResult.DataSource = bsQuery;
            GridUtils.SetTablesColumns(dgvSearchResult);
            chart.DataSource = bsQuery;
            chart.Series[0].XValueMember = "Collected";
            chart.Series[0].YValueMembers = "Nprice";
        }

        private void cmbChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var chartType = (SeriesChartType) Enum.Parse(typeof(SeriesChartType), cmbChartType.SelectedItem.ToString());
            chart.Series[0].ChartType = chartType;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog {FileName = "history.xlsx"};
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            var dataTable = (DataTable) bsQuery.DataSource;
            var dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);
            CreateExcelFile.CreateExcelDocument(dataSet, saveFileDialog.FileName);
            if (File.Exists(saveFileDialog.FileName)) Process.Start(saveFileDialog.FileName);
        }
    }
}