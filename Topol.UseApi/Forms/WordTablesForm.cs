using ComLog.WinForms.Utils;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Topol.UseApi.Utils;

namespace Topol.UseApi.Forms
{
    public partial class WordTablesForm : Form
    {
        private readonly List<DataTable> _datatables = new List<DataTable>();
        private readonly BindingSource _datatableBindingSource = new BindingSource();

        public WordTablesForm()
        {
            InitializeComponent();
        }


        public bool Prepare(string filename, string url)
        {
            Text = filename;
            var uri = new Uri(url);
            var arguments = uri.Query
                .Substring(1) // Remove '?'
                .Split('&')
                .Select(q => q.Split('='))
                .ToDictionary(q => q.FirstOrDefault(), q => q.Skip(1).FirstOrDefault());
            var uid = arguments["uid"];
            filename = Path.Combine(KnownFolders.GetPath(KnownFolder.Downloads), $"{uid}{Path.GetExtension(filename)}");

            if (!File.Exists(filename))
            {
                DownloadFile(filename, url);
            }

            TextFromWord(filename);

            if (!_datatables.Any()) return false;
            _datatableBindingSource.DataSource = _datatables[0];
            dgvTable.DataSource = _datatableBindingSource;
            return true;
        }

        private static void DownloadFile(string filename, string url)
        {

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            request.UserAgent =
                "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.109 Safari/537.36";


            using (var response = (HttpWebResponse)request.GetResponseAsync().Result)
            {
                var responseStream = response.GetResponseStream();
                using (var fileStream = File.Create(filename))
                {
                    responseStream?.CopyTo(fileStream);
                }
            }
        }

        private void TextFromWord(string filename)
        {
            var datatable = new DataTable();
            using (var wDoc = WordprocessingDocument.Open(filename, false))
            {
                var parts = wDoc.MainDocumentPart.Document.Descendants().FirstOrDefault();
                if (parts == null) return;
                _datatables.Clear();
                foreach (var node in parts.ChildElements)
                {
                    switch (node)
                    {
                        case Paragraph _:
                            continue;
                        case Table _:
                            ProcessTable((Table)node, datatable);
                            break;
                    }
                }
                _datatables.Add(datatable);
            }
        }

        private static void ProcessTable(OpenXmlElement node, DataTable datatable)
        {
            foreach (var row in node.Descendants<TableRow>())
            {
                var addColumnCount = (row.Descendants<TableCell>().Count() - datatable.Columns.Count);
                if (addColumnCount > 0)
                {
                    for (var i = 0; i < addColumnCount; i++)
                    {
                        datatable.Columns.Add($"Column{datatable.Columns.Count + 1}", typeof(string));
                    }

                }
                var datatableRow = datatable.NewRow();
                var idxColumn = 0;
                foreach (var cell in row.Descendants<TableCell>())
                {
                    var sbRow = new StringBuilder();
                    foreach (var para in cell.Descendants<Paragraph>())
                    {
                        var text = ProcessParagraph(para);
                        sbRow.AppendLine(text);
                    }
                    datatableRow[idxColumn] = sbRow.ToString();
                    idxColumn++;
                }
                datatable.Rows.Add(datatableRow);
            }
        }

        private static string ProcessParagraph(OpenXmlElement node)
        {
            var sbParagraph = new StringBuilder();
            foreach (var text in node.Descendants<Text>())
            {
                sbParagraph.Append(text.InnerText);
            }
            return sbParagraph.ToString();
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog { FileName = $"Topol.Api_{DateTime.Now:yyyyMMdd_HHmm}.xlsx" };
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            var sourceDataTable = (DataTable)_datatableBindingSource.DataSource;
            var view = new DataView(sourceDataTable, dgvTable.FilterString, dgvTable.SortString, DataViewRowState.CurrentRows);
            var dataTable = view.ToTable();
            CreateExcelFile.CreateExcelDocument(dataTable, saveFileDialog.FileName);
            if (File.Exists(saveFileDialog.FileName)) Process.Start(saveFileDialog.FileName);
        }
    }
}
