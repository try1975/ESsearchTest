using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReadWordTables
{
    public partial class Form1 : Form
    {
        private List<DataTable> datatables = new List<DataTable>();
        private BindingSource datatableBindingSource = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            textBox1.Text = TextFromWord(openFileDialog.FileName);
            if (datatables.Any())
            {
                datatableBindingSource.DataSource = datatables[0];
                dgvTable.DataSource = datatableBindingSource;
            }
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
                    var sb = new StringBuilder();
                    foreach (var para in cell.Descendants<Paragraph>())
                    {
                        var text = ProcessParagraph(para, textBuilder);
                        sb.AppendLine(text);
                    }
                    textBuilder.Append(" | ");
                    datatableRow[idxColumn] = sb.ToString();
                    idxColumn++;
                }
                textBuilder.AppendLine("");

                datatable.Rows.Add(datatableRow);
            }
            //return datatable;
        }

        private static string ProcessParagraph(Paragraph node, StringBuilder textBuilder)
        {
            var sb = new StringBuilder();
            foreach (var text in node.Descendants<Text>())
            {
                textBuilder.Append(text.InnerText);
                sb.Append(text.InnerText);
            }
            return sb.ToString();
        }

    }
}
