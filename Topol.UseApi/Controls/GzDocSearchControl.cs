using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using log4net;
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
        private BindingSource _dgvItemsBindingSource { get; }

        public GzDocSearchControl(IDataMаnager dataManager)
        {
            InitializeComponent();
            _dataManager = dataManager;

            _docsDataTable.Columns.Add(GzDocUtils.DocName);
            _docsDataTable.Columns.Add(GzDocUtils.DocUrl);
            _dgvItemsBindingSource = new BindingSource {DataSource = _docsDataTable};
            dgvItems.DataSource = _dgvItemsBindingSource;
            _dgvItemsBindingSource.CurrentChanged += DgvItemsBindingSource_CurrentChanged;
            btnGzDocSearch.Click += BtnGzDocSearch_Click;
            btnWordTable.Click += BtnWordTable_Click;
            dgvItems.CellContentDoubleClick += DgvItems_CellContentDoubleClick;

            _gzDocListView = CompositionRoot.Resolve<IGzDocListView>();
            var control = (Control)_gzDocListView;
            control.Dock = DockStyle.Fill;
            panel7.Controls.Clear();
            panel7.Controls.Add(control);
        }

        private void DgvItemsBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            GetData(out var url, out var filename);
            SetBtnWordTableEnable(filename);
            _gzDocListView.ClearData();
            if (!string.IsNullOrWhiteSpace(url))
            {
                var uri = new Uri(url);
                var arguments = uri.Query
                    .Substring(1) // Remove '?'
                    .Split('&')
                    .Select(q => q.Split('='))
                    .ToDictionary(q => q.FirstOrDefault(), q => q.Skip(1).FirstOrDefault());
                var docPath = arguments["docPath"];
                if(string.IsNullOrWhiteSpace(docPath)) return;
                try
                {
                    var reestrNumber = Regex.Match(docPath, "%2f([0-9]*)%2f", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace).Groups[1].Value;
                    _gzDocListView.FillByReestrNum(reestrNumber);
                }
                catch (Exception)
                {
                }
            }
        }

        private void GetData(out string url, out string filename)
        {
            var current = (DataRowView)_dgvItemsBindingSource.Current;
            if(current == null)
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
            _docsDataTable.Rows.Clear();
            var docs = _dataManager.GetGzDocSearch(tbGzDocSearchKey.Text);
            foreach (var doc in docs)
            {
                var row = _docsDataTable.NewRow();
                row["DocName"] = doc.Value;
                row["DocUrl"] = doc.Key;
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
            if(string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(filename)) return;
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

        private void DgvItems_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        { 
            GetData(out var url, out var filename);
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(filename)) return;
            try
            {
                var ext = Path.GetExtension(filename);
                if (! string.IsNullOrWhiteSpace(ext))
                {
                    ext = ext.ToLower();
                    if (!ModifierKeys.HasFlag(Keys.Control))
                    {
                        url = Downloader.GetFile2(filename, url);
                    }
                    else
                    {
                        if ((ext.StartsWith(".doc") || ext.StartsWith(".xls")))
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
