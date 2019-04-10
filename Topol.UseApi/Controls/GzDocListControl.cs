using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using log4net;
using Topol.UseApi.Forms;
using Topol.UseApi.Interfaces;
using Topol.UseApi.Interfaces.Common;
using Topol.UseApi.Ninject;
using Topol.UseApi.Utils;

namespace Topol.UseApi.Controls
{
    public partial class GzDocListControl : UserControl, IGzDocListView
    {

        private static readonly ILog Log = LogManager.GetLogger(nameof(GzDocListControl));
        private readonly DataTable _docsDataTable = new DataTable();
        private readonly IDataMаnager _dataManager;

        public GzDocListControl()
        {
            InitializeComponent();


            _dataManager = CompositionRoot.Resolve<IDataMаnager>();
            _docsDataTable.Columns.Add(GzDocUtils.DocName);
            _docsDataTable.Columns.Add(GzDocUtils.DocUrl);
            lbGzDocs.DataSource = _docsDataTable;
            lbGzDocs.DisplayMember = GzDocUtils.DocName;
            lbGzDocs.ValueMember = GzDocUtils.DocUrl;
            lbGzDocs.MouseDoubleClick += ListBox1_MouseDoubleClick;
            lbGzDocs.SelectedIndexChanged += LbGzDocsOnSelectedIndexChanged;
            btnWordTable.Click += BtnWordTableOnClick;
            
        }

        private void LbGzDocsOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            SetBtnWordTableEnable();
        }

        private void SetBtnWordTableEnable()
        {
            btnWordTable.Enabled = false;
            GetData(out var url, out var filename);
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(filename)) return;
            var ext = Convert.ToString(Path.GetExtension(filename)).ToLower();
            if (ext.StartsWith(".doc") /*|| ext.StartsWith(".pdf")*/) btnWordTable.Enabled = true;
        }

        private void BtnWordTableOnClick(object sender, EventArgs eventArgs)
        {
            GetData(out var url, out var filename);
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(filename)) return;
            try
            {
                var ext = Path.GetExtension(filename);
                ext = ext.ToLower();
                if (!ext.StartsWith(".doc") /*&& !ext.StartsWith(".pdf")*/) return;
                var frmWordTable = new WordTablesForm();
                if (frmWordTable.Prepare(filename, url)) frmWordTable.Show();
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                MessageBox.Show($@"Not started {filename}");
            }
        }

        private void ListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GetData(out var url, out var filename);
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(filename)) return;
            try
            {
                var ext = Path.GetExtension(filename);
                ext = ext.ToLower();
                if (!ModifierKeys.HasFlag(Keys.Control))
                {
                    url = Downloader.GetFile(filename, url);
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
                Process.Start(url);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                MessageBox.Show($@"Not started {filename}");
            }
        }


        public void ClearData()
        {
            _docsDataTable.Rows.Clear();
        }

        public void Add(string docName, string docUrl)
        {
            var row = _docsDataTable.NewRow();
            row[GzDocUtils.DocName] = docName;
            row[GzDocUtils.DocUrl] = docUrl;
            _docsDataTable.Rows.Add(row);
        }

        public void FillByReestrNum(string reestrNumber)
        {
            ClearData();
            if(string.IsNullOrWhiteSpace(reestrNumber)) return;
            var docs = _dataManager.GetGzDocs(reestrNumber);
            foreach (var doc in docs)
            {
                Add(doc.Value, doc.Key);
            }
            SetBtnWordTableEnable();
        }

        public void GetData(out string url, out string filename)
        {
            url = null;
            filename = null;
            if (lbGzDocs.SelectedIndex < 0) return;

            filename = lbGzDocs.Text;
            url = lbGzDocs.SelectedValue.ToString();
        }
    }
}
