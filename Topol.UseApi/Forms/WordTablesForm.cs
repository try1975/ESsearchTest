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
using ADGV;
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

            btnGzDocSearch.Click += BtnGzDocSearchOnClick;
            button1.Click += Button1OnClick;
            dgvTable.FilterStringChanged += DgvTableOnFilterStringChanged;
            dgvTable.SortStringChanged += DgvTableOnSortStringChanged;
        }

        private static void DgvTableOnSortStringChanged(object sender, EventArgs e)
        {
            if (!(sender is AdvancedDataGridView dataGridView)) return;
            if (!(dataGridView.DataSource is BindingSource bindingSource)) return;
            bindingSource.Sort = dataGridView.SortString;
        }

        private static void DgvTableOnFilterStringChanged(object sender, EventArgs e)
        {
            if (!(sender is AdvancedDataGridView dataGridView)) return;
            if (!(dataGridView.DataSource is BindingSource bindingSource)) return;
            bindingSource.Filter = dataGridView.FilterString;
        }

        private void Button1OnClick(object sender, EventArgs eventArgs)
        {
            SearchInTable(tbGzDocSearchKey.Text, dgvTable.CurrentCell.RowIndex, dgvTable.CurrentCell.ColumnIndex);
        }

        private void BtnGzDocSearchOnClick(object sender, EventArgs eventArgs)
        {
            SearchInTable(tbGzDocSearchKey.Text);
        }

        public void Show(string key)
        {
            Show();
            tbGzDocSearchKey.Text = key;
            SearchInTable(key);
        }

        private void SearchInTable(string key, int startRow = 0, int startColumn = 0)
        {
            if (!_datatableBindingSource.SupportsSearching) return;
            key = key.ToLower();
            var tbl = (DataTable)_datatableBindingSource.DataSource;
            var found = false;

            if (startColumn > 0) startColumn++;
            if (startColumn >= tbl.Columns.Count - 1)
            {
                startColumn = 0;
                startRow++;
            }
            if (startRow >= tbl.Rows.Count - 1) startRow = 0;

            for (var rowIndex = startRow; rowIndex < tbl.Rows.Count; rowIndex++)
            {
                var dataRow = tbl.Rows[rowIndex];
                for (var columnIndex = startColumn; columnIndex < tbl.Columns.Count; columnIndex++)
                {
                    if (!dataRow[columnIndex].ToString().ToLower().Contains(key)) continue;
                    dgvTable.CurrentCell = dgvTable.Rows[rowIndex].Cells[columnIndex];
                    found = true;
                    break;
                }
                startColumn = 0;
                if (found) break;
            }
            if (!found) MessageBox.Show($@"{key} - не найдено.");
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
                    var sbRow = new StringBuilder(cell.Descendants<Paragraph>().Count());
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
            var sbParagraph = new StringBuilder(node.Descendants<Text>().Count());
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
