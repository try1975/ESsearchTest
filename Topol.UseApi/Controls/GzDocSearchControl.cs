using ADGV;
using log4net;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using GzCommon;
using Topol.UseApi.Forms;
using Topol.UseApi.Interfaces;
using Topol.UseApi.Interfaces.Common;
using Topol.UseApi.Ninject;
using Topol.UseApi.Utils;

namespace Topol.UseApi.Controls
{
    public partial class GzDocSearchControl : UserControl, IGzDocSearchView
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(GzDocSearchControl));
        private readonly IDataMаnager _dataManager;
        private readonly DataTable _docsDataTable = new DataTable();
        private readonly IGzDocListView _gzDocListView;
        private string _reestrNumber;
        private BindingSource DocsBindingSource { get; }

        public GzDocSearchControl(IDataMаnager dataManager)
        {
            InitializeComponent();
            _dataManager = dataManager;

            var column = new DataColumn("Selected", typeof(bool)) { DefaultValue = false };
            _docsDataTable.Columns.Add(column);
            column = new DataColumn(GzDocUtils.DocName) { ReadOnly = true };
            _docsDataTable.Columns.Add(column);
            column = new DataColumn(GzDocUtils.DocExt) { ReadOnly = true };
            _docsDataTable.Columns.Add(column);
            column = new DataColumn(GzDocUtils.DocRegion) { ReadOnly = true };
            _docsDataTable.Columns.Add(column);
            column = new DataColumn(GzDocUtils.DocMonth) { ReadOnly = true };
            _docsDataTable.Columns.Add(column);
            column = new DataColumn(GzDocUtils.DocReestrNumber) { ReadOnly = true };
            _docsDataTable.Columns.Add(column);
            column = new DataColumn(GzDocUtils.DocUrl) { ReadOnly = true };
            _docsDataTable.Columns.Add(column);


            DocsBindingSource = new BindingSource { DataSource = _docsDataTable };
            dgvDocs.DataSource = DocsBindingSource;
            DocsBindingSource.CurrentChanged += DocsBindingSource_CurrentChanged;
            btnGzDocSearch.Click += BtnGzDocSearch_Click;
            btnWordTable.Click += BtnWordTable_Click;
            dgvDocs.CellContentDoubleClick += DgvDocsCellContentDoubleClick;

            _gzDocListView = CompositionRoot.Resolve<IGzDocListView>();
            var docListControl = (Control)_gzDocListView;
            docListControl.Dock = DockStyle.Fill;
            panel7.Controls.Clear();
            panel7.Controls.Add(docListControl);

            dgvDocs.FilterStringChanged += DgvDocsFilterStringChanged;
            dgvDocs.SortStringChanged += DgvDocsSortStringChanged;
            btnGoGzWebsite.Click += BtnGoGzWebsiteOnClick;

            var gzRegions = _dataManager.GetGzRegions();
            clbRegions.DataSource = gzRegions;
            clbRegions.DisplayMember = "Name";
            clbRegions.SelectedIndex = 0;
        }

        private void BtnGoGzWebsiteOnClick(object sender, EventArgs eventArgs)
        {
            GzDocUtils.GetUrlGzWebsiteCardDocs(_reestrNumber);
        }

        private static void DgvDocsSortStringChanged(object sender, EventArgs e)
        {
            if (!(sender is AdvancedDataGridView dataGridView)) return;
            if (!(dataGridView.DataSource is BindingSource bindingSource)) return;
            bindingSource.Sort = dataGridView.SortString;
        }

        private static void DgvDocsFilterStringChanged(object sender, EventArgs e)
        {
            if (!(sender is AdvancedDataGridView dataGridView)) return;
            if (!(dataGridView.DataSource is BindingSource bindingSource)) return;
            bindingSource.Filter = dataGridView.FilterString;
        }

        private void DocsBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            GetData(out var url, out var filename);
            SetBtnWordTableEnable(filename);
            _gzDocListView.ClearData();
            GzDocUtils.GetUrlData(url, out var _, out var _, out var docReestrNumber, Log);
            if (string.IsNullOrWhiteSpace(docReestrNumber)) return;
            _reestrNumber = docReestrNumber;
            _gzDocListView.FillByReestrNum(_reestrNumber);
        }

        private void GetData(out string url, out string filename)
        {
            var current = (DataRowView)DocsBindingSource.Current;
            if (current == null)
            {
                url = null;
                filename = null;
                return;
            }
            url = current.Row[GzDocUtils.DocUrl] as string;
            filename = current.Row[GzDocUtils.DocName] as string;
        }

        

        private void BtnGzDocSearch_Click(object sender, EventArgs e)
        {
            var regions ="";
            foreach (var item in clbRegions.CheckedItems)
            {
                if (!string.IsNullOrEmpty(regions)) regions += ",";
                regions += ((RegionItem) item).Code;
            }

            _docsDataTable.Rows.Clear();
            var docs = _dataManager.GetGzDocSearch(tbGzDocSearchKey.Text, regions, tbGzDocMonths.Text);
            if (docs.Count == 0) MessageBox.Show(@"Не найдено.");
            foreach (var doc in docs)
            {
                var row = _docsDataTable.NewRow();
                row[GzDocUtils.DocName] = doc.Value;
                row[GzDocUtils.DocUrl] = doc.Key;
                GzDocUtils.GetUrlData(doc.Key, out var docRegion, out var docMonth, out var docReestrNumber, Log);
                row[GzDocUtils.DocRegion] = docRegion;
                row[GzDocUtils.DocMonth] = docMonth;
                row[GzDocUtils.DocReestrNumber] = docReestrNumber;
                GzDocUtils.GetFilenameData(doc.Value, out var ext);
                row[GzDocUtils.DocExt] = ext;
                _docsDataTable.Rows.Add(row);
            }
        }

        private void SetBtnWordTableEnable(string filename)
        {
            btnWordTable.Enabled = false;
            if (string.IsNullOrWhiteSpace(filename)) return;
            var ext = Convert.ToString(Path.GetExtension(filename)).ToLower();
            if (ext.StartsWith(".doc") /*|| ext.StartsWith(".pdf")*/) btnWordTable.Enabled = true;
        }

        private void BtnWordTable_Click(object sender, EventArgs e)
        {
            GetData(out var url, out var filename);
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(filename)) return;
            try
            {
                var ext = Path.GetExtension(filename);
                if (string.IsNullOrWhiteSpace(ext)) return;
                ext = ext.ToLower();
                if (!ext.StartsWith(".doc") /*&& !ext.StartsWith(".pdf")*/) return;
                var frmWordTable = new WordTablesForm();
                if (frmWordTable.Prepare2(filename, url)) frmWordTable.Show(tbGzDocSearchKey.Text);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                MessageBox.Show($@"Not started {filename}");
            }
        }

        private void DgvDocsCellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            GetData(out var url, out var filename);
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(filename)) return;
            try
            {
                GzDocUtils.GetFilenameData(filename, out var ext);
                if (!string.IsNullOrWhiteSpace(ext))
                {
                    if (!ModifierKeys.HasFlag(Keys.Control))
                    {
                        url = Downloader.GetFile2(filename, url);
                    }
                    else
                    {
                        if (ext.StartsWith(".doc") || ext.StartsWith(".xls"))
                        {
                            url = $@"https://view.officeapps.live.com/op/view.aspx?src={Uri.EscapeDataString(url)}";
                        }
                        else if (ext.StartsWith(".pdf"))
                        {
                            url = $@"https://docs.google.com/viewerng/viewer?url={Uri.EscapeDataString(url)}&embedded=true";
                        }
                    }

                }
                Process.Start(url);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                MessageBox.Show($@"Not started {filename}");
            }
        }
    }
}
