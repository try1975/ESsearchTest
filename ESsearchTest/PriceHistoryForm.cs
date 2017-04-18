using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //var dt = new DataTable();
            //dt.Columns.Add("Name", typeof(string));
            //dt.Columns.Add("Price", typeof(decimal));
            //dt.Columns.Add("CollectedAt", typeof(DateTime));


            //dt.Rows.Add("IE", 12, new DateTime(2017, 1, 18));
            //dt.Rows.Add("Chrome", 14.5, new DateTime(2017, 1, 18));
            //dt.Rows.Add("Chrome", 15, new DateTime(2017, 1, 18));
            
            //dt.Rows.Add("Chrome", 15.8, new DateTime(2017, 1, 19));

            //dt.Rows.Add("Chrome", 17.15, new DateTime(2017, 1, 20));
            //dt.Rows.Add("IE", 12.5, new DateTime(2017, 1, 20));

            //dt.Rows.Add("Chrome", 16, new DateTime(2017, 1, 21));
            


            var columnName = dt.Columns[0].ColumnName;
            Text = $"Диаграмма: {columnName}";

            //var pieDataTable = from row in dt.AsEnumerable()
            //                   group row by row.Field<string>(columnName) into grp
            //                   select new
            //                   {
            //                       Name = grp.Key,
            //                       Count = grp.Count()
            //                   };


            //chart.DataSource = pieDataTable;
            bsQuery.DataSource = dt;
            dgvSearchResult.DataSource = bsQuery;
            GridUtils.SetTablesColumns(dgvSearchResult);
            chart.DataSource = bsQuery;
            chart.Series[0].XValueMember = "Collected";
            chart.Series[0].YValueMembers = "Nprice";
        }

        private void cmbChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var chartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), cmbChartType.SelectedItem.ToString());
            chart.Series[0].ChartType = chartType;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog { FileName = "history.xlsx" };
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            var dataTable = (DataTable)bsQuery.DataSource;
            var dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);
            CreateExcelFile.CreateExcelDocument(dataSet, saveFileDialog.FileName);
            if (File.Exists(saveFileDialog.FileName)) Process.Start(saveFileDialog.FileName);
        }
    }
}
