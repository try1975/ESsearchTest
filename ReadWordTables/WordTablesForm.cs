using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReadWordTables
{
    public partial class WordTablesForm : Form
    {
        private List<DataTable> datatables = new List<DataTable>();
        private BindingSource datatableBindingSource = new BindingSource();

        public WordTablesForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
           // textBox1.Text = TextFromWord(openFileDialog.FileName);
            if (!datatables.Any()) return;
            datatableBindingSource.DataSource = datatables[0];
            dgvTable.DataSource = datatableBindingSource;
        }

        private string TextFromWord(string filename)
        {
            var textBuilder = new StringBuilder();
            var datatable = new DataTable();
            using (var wDoc = WordprocessingDocument.Open(filename, false))
            {
                var parts = wDoc.MainDocumentPart.Document.Descendants().FirstOrDefault();
                if (parts != null)
                {
                    datatables.Clear();
                    foreach (var node in parts.ChildElements)
                    {
                        if (node is Paragraph) continue;
                        //if (node is Paragraph)
                        //{
                        //    ProcessParagraph((Paragraph)node, textBuilder);
                        //    textBuilder.AppendLine("");
                        //}

                        if (node is Table)
                        {
                           ProcessTable((Table)node, textBuilder, datatable);
                        }
                    }
                    datatables.Add(datatable);
                }
            }
            return textBuilder.ToString();
        }

        private static void ProcessTable(Table node, StringBuilder textBuilder, DataTable datatable)
        {
            //var datatable = new DataTable();
            foreach (var row in node.Descendants<TableRow>())
            {
                var addColumnCount = (row.Descendants<TableCell>().Count() - datatable.Columns.Count);
                if (addColumnCount>0)
                {
                    for (int i = 0; i < addColumnCount; i++)
                    {
                        datatable.Columns.Add($"Column{datatable.Columns.Count+1}", typeof(string));
                    }
                    
                }
                var datatableRow = datatable.NewRow();

                textBuilder.Append("| ");
                var idxColumn = 0;
                foreach (var cell in row.Descendants<TableCell>())
                {
                    var sbRow = new StringBuilder();
                    foreach (var para in cell.Descendants<Paragraph>())
                    {
                        var text = ProcessParagraph(para, textBuilder);
                        sbRow.AppendLine(text);
                    }
                    textBuilder.Append(" | ");
                    datatableRow[idxColumn] = sbRow.ToString();
                    idxColumn++;
                }
                textBuilder.AppendLine("");

                datatable.Rows.Add(datatableRow);
            }
            //return datatable;
        }

        private static string ProcessParagraph(Paragraph node, StringBuilder textBuilder)
        {
            var sbParagraph = new StringBuilder();
            foreach (var text in node.Descendants<Text>())
            {
                textBuilder.Append(text.InnerText);
                sbParagraph.Append(text.InnerText);
            }
            return sbParagraph.ToString();
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog { FileName = $"Topol.Api_{DateTime.Now:yyyyMMdd_HHmm}.xlsx" };
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            var sourceDataTable = (DataTable)datatableBindingSource.DataSource;
            var view = new DataView(sourceDataTable, dgvTable.FilterString, dgvTable.SortString, DataViewRowState.CurrentRows);
            var dataTable = view.ToTable();
            //CreateExcelFile.CreateExcelDocument(dataTable, saveFileDialog.FileName);
            if (File.Exists(saveFileDialog.FileName)) Process.Start(saveFileDialog.FileName);
        }
    }
}
