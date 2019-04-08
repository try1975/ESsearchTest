using ComLog.WinForms.Utils;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Topol.UseApi.Utils;
using DataTable = System.Data.DataTable;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using Table = DocumentFormat.OpenXml.Wordprocessing.Table;

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

        public void Show(string key)
        {
            if (_datatableBindingSource.SupportsSearching)
            {
                key = key.ToLower();
                var tbl = (DataTable)_datatableBindingSource.DataSource;
                foreach (DataColumn column in tbl.Columns)
                {
                    var rowIndex = 0;
                    foreach(DataRow dataRow in tbl.Rows)
                    {
                        
                        if (dataRow[column.ColumnName].ToString().ToLower().Contains(key))
                        {
                            //_datatableBindingSource.Position = rowIndex;
                            dgvTable.CurrentCell = dgvTable.Rows[rowIndex].Cells[column.ColumnName];
                            break;
                        }
                        rowIndex++;
                    }
                }
            }
            Show();
        }

        public bool Prepare(string filename, string url)
        {
            Text = filename;
            var ext = Convert.ToString(Path.GetExtension(filename)).ToLower();

            filename = Downloader.GetFile(filename, url);

            if (ext.EndsWith(".doc"))
            {
                var newFileName = filename.Replace(".doc", ".docx");
                if (File.Exists(filename) && !File.Exists(newFileName)) ConvertDocToDocx(filename, newFileName);
                filename = newFileName;
            }

            TextFromWord(filename);

            if (!_datatables.Any()) return false;
            _datatableBindingSource.DataSource = _datatables[0];
            dgvTable.DataSource = _datatableBindingSource;
            return true;
        }

        public bool Prepare2(string filename, string url)
        {
            Text = filename;
            var ext = Convert.ToString(Path.GetExtension(filename)).ToLower();

            filename = Downloader.GetFile2(filename, url);

            if (ext.EndsWith(".doc"))
            {
                var newFileName = filename.Replace(".doc", ".docx");
                if (File.Exists(filename) && !File.Exists(newFileName)) ConvertDocToDocx(filename, newFileName);
                filename = newFileName;
            }

            TextFromWord(filename);

            if (!_datatables.Any()) return false;
            _datatableBindingSource.DataSource = _datatables[0];
            dgvTable.DataSource = _datatableBindingSource;
            return true;
        }

        private void ConvertDocToDocx(string path, string newFileName)
        {
            if (!path.ToLower().EndsWith(".doc")) return;
            var word = new Microsoft.Office.Interop.Word.Application();
            var document = word.Documents.Open(path);
            document.SaveAs2(newFileName, WdSaveFormat.wdFormatXMLDocument, CompatibilityMode: WdCompatibilityMode.wdWord2010);
            word.ActiveDocument.Close();
            word.Quit();
            //File.Delete(path);
        }

        private void TextFromWord(string filename)
        {
            var datatable = new DataTable();
            //копировать файл если он занят
            try
            {
                using (Stream _ = new FileStream(filename, FileMode.Open))
                {
                    // File/Stream manipulating code here
                }
            }
            catch
            {
                var count = 1;

                var fileNameOnly = Path.GetFileNameWithoutExtension(filename);
                var extension = Path.GetExtension(filename);
                var path = Path.GetDirectoryName(filename);
                var newFullPath = filename;

                while (File.Exists(newFullPath))
                {
                    var tempFileName = $"{fileNameOnly}({count++})";
                    newFullPath = Path.Combine(path ?? throw new InvalidOperationException(), tempFileName + extension);
                }
                File.Copy(filename, newFullPath, true);
                filename = newFullPath;
            }

            using (var wDoc = WordprocessingDocument.Open(filename, false))
            {
                var part = wDoc.MainDocumentPart.Document
                    .Descendants()
                    .FirstOrDefault(z => z.LocalName == "body")
                    ;
                if (part == null) return;
                _datatables.Clear();
                foreach (var node in part.ChildElements)
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
