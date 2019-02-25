using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ESsearchTest.Controls;
using FastMember;
using Nest;
using PriceCommon.Model;
using PriceCommon.Norm;
using PriceCommon.Utils;
using PricePipeCore;

namespace ESsearchTest
{
    public partial class Form1 : Form
    {
        private readonly List<KeyValuePair<string, INorm>> _norms;
        private bool _canUpdateAnalyze = true;
        private ElasticClient _elasticClient;
        private List<Content> _founded;
        private long _hitsTotal;
        private bool _needUpdateAnalyze;
        private INorm _norm;

        #region Initial

        public Form1()
        {
            InitializeComponent();

            _norms = new List<KeyValuePair<string, INorm>>();
        }

        private void AddNormControlToForm(Control control)
        {
            control.Dock = DockStyle.Fill;
            pnlNorm.Controls.Clear();
            pnlNorm.Controls.Add(control);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (AppSettings.MaxResultCount > udResultCount.Maximum) udResultCount.Maximum = AppSettings.MaxResultCount;

            var address = AppSettings.Md5Host;
            if (string.IsNullOrEmpty(address)) address = @"http://localhost:9200/";

            try
            {
                var connectionSettings = new ConnectionSettings(new Uri(address))
                    //.OnRequestCompleted(details => Debug.WriteLine($"{details.HttpMethod} {details.Uri} {Encoding.UTF8.GetString(details.RequestBodyInBytes)}"))
                    .OnRequestCompleted(details =>
                    {
                        var s = details.RequestBodyInBytes != null
                            ? Encoding.UTF8.GetString(details.RequestBodyInBytes)
                            : null;
                        Debug.WriteLine($"{s}");
                    })
                    .DisableDirectStreaming()
                    .DefaultIndex(AppSettings.DefaultIndex)
                    .BasicAuthentication(AppSettings.Md5UserName, AppSettings.Md5Password)
                    //.PrettyJson()
                    ;
                _elasticClient = new ElasticClient(connectionSettings);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                MessageBox.Show($@"Ошибка создания клиента ES. {exception}");
                throw;
            }

            // get document count of type Content in index
            try
            {
                lblIndexDocCount.Text = $"В базе: {_elasticClient.Count<Content>().Count}";
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                MessageBox.Show($"Ошибка получения количества документов. {exception}");
                throw;
            }
            tabControl1.TabPages.Remove(tpExpert);

            // get seller count
            try
            {
                var searchResponse = _elasticClient.Search<Content>(s => s
                    .Aggregations(a => a
                        .Terms("unique", te => te
                            .Field(nameof(Content.Seller).ToLower())
                            .MinimumDocumentCount(10)
                            .Size(1000)
                        )
                    )
                    );
                var sellers = searchResponse.Aggs.Terms("unique").Buckets.Select(b => b.Key).ToList();
                sellers.Sort();
                foreach (var seller in sellers)
                {
                    clbSellers.Items.Add(seller);
                }
                lblSellerCount.Text = $"Поставщиков: {clbSellers.Items.Count}";
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                MessageBox.Show($"Ошибка получения списка поставщиков. {exception}");
                //throw;
            }

            // Last update date
            try
            {
                var lastUpdatedResponse = _elasticClient.Search<Content>(s => s
                    .Query(q => q.MatchAll())
                    .Size(1)
                    .Sort(sort => sort
                        .Descending(f => f.CollectedAt))
                    );
                var lastUpdated = lastUpdatedResponse.Hits.Select(s => s.Source).FirstOrDefault();
                var updatedDateText = "";
                if (lastUpdated != null) updatedDateText = $"{lastUpdated.Collected}";
                lblLastUpdated.Text = $"Обновлено: {updatedDateText}";
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                MessageBox.Show($"Ошибка получения даты последнего обновления. {exception}");
                throw;
            }

            // Add norm control to form
            _norms.Add(new KeyValuePair<string, INorm>("Медицинские препараты", new MedPrepControl(_elasticClient)));
            _norms.Add(new KeyValuePair<string, INorm>("Пустой нормализатор", new NormControl()));

            cmbNorm.DataSource = _norms;
            cmbNorm.ValueMember = "Value";
            cmbNorm.DisplayMember = "Key";
            cmbNorm.SelectedIndexChanged += cmbNorm_SelectedIndexChanged;
            cmbNorm.SelectedIndex = -1;
            cmbNorm.SelectedIndex = 0;
        }

        #endregion //Initial

        #region Search

        private void SearchExpert(int maxTake = 200)
        {
            var queryContainer = new List<QueryContainer>();
            var exactRows = tbExactExpert.Text.ToLower().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var row in exactRows)
            {
                if (string.IsNullOrEmpty(row)) continue;
                queryContainer.Add(Query<Content>
                    .Match(m => m
                        .Field(p => p.Name)
                        .Query(row.Trim())));
            }

            var queryRows = tbQueryExpert.Text.ToLower().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var row in queryRows)
            {
                if (string.IsNullOrEmpty(row)) continue;
                var modifiedRow = string.Join(" ",
                    row.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(r => /*'*' +*/ r + '*'));
                queryContainer.Add(Query<Content>
                    .QueryString(q => q.Query(modifiedRow))
                    );
            }

            var excludeRows = tbExcludeExpert.Text.ToLower().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var row in excludeRows)
            {
                if (string.IsNullOrEmpty(row)) continue;
                var modifiedRow = string.Join(" ",
                    row.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(r => "-*" + r + '*'));
                queryContainer.Add(Query<Content>
                    .QueryString(q => q.Query(modifiedRow))
                    );
            }


            if (queryContainer.Count <= 0) return;

            var responseExpert = _elasticClient.Search<Expert>(s => s
                .Take(maxTake)
                .Query(q => q
                    .Bool(b => b
                        .Must(queryContainer.ToArray())))
                );
            //var table = new DataTable();
            //using (var reader = ObjectReader.Create(responseExpert.Hits.Select(s => s.Source).ToList()))
            //{
            //    table.Load(reader);
            //}
            //bsExpert.DataSource = table;
            bsExpert.DataSource = responseExpert.Hits.Select(s => s.Source).ToList();
            dgvExpert.DataSource = bsExpert;
            GridUtils.SetTablesColumns(dgvExpert);
        }

        private void Search(int maxTake = 200)
        {
            var queryContainer = GetQueryContainer();
            if (queryContainer.Count == 0) return;

            var responseContent = _elasticClient.Search<Content>(s => s
                .Take(maxTake)
                .Query(q => q
                    .Bool(b => b
                        .Must(queryContainer.ToArray())))
                );
            _founded = responseContent.Hits.Select(s => s.Source).ToList();
            _hitsTotal = responseContent.Total;
            var table = new DataTable();
            using (var reader = ObjectReader.Create(_founded))
            {
                table.Load(reader);
            }
            bsQuery.DataSource = table;
            dgvSearchResult.DataSource = bsQuery;
            GridUtils.SetTablesColumns(dgvSearchResult);
            SetCalculation(dgvSearchResult);
            //lblIndexDocCount.Text = $"В базе: {_elasticClient.Count<Content>().Count}";
        }

        private List<QueryContainer> GetQueryContainer()
        {
            var queryContainer = new List<QueryContainer>();
            // if use normalizer
            if (cbNorm.Checked && _norm != null)
            {
                _norm.InitialName = tbName.Text;
                queryContainer.AddRange(_norm.QueryContainer);
            }


            var exactRows = tbExact.Text.ToLower()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var row in exactRows)
            {
                if (string.IsNullOrEmpty(row)) continue;
                queryContainer.Add(Query<Content>
                    .Match(m => m
                        .Field(p => p.Name)
                        .Query(row.Trim())));
                //queryContainer.Add(Query<Content>
                //   .Term(m => m
                //       .Field(p => p.Name)
                //       .Value(row.Trim())));
            }

            if (clbSellers.CheckedItems.Count > 0)
            {
                var sellers = "";
                if (clbSellers.CheckedItems.Count == 1)
                {
                    sellers = string.Join("", clbSellers.CheckedItems.OfType<string>()).Trim() + '*';
                }
                else
                {
                    sellers = clbSellers.CheckedItems.OfType<string>()
                        .Aggregate(sellers, (current, seller) => current + seller.Trim() + "* ");
                }
                queryContainer.Add(Query<Content>
                    .QueryString(qs => qs
                        .DefaultField(df => df.Seller)
                        .Query(sellers)))
                    ;
            }

            var queryRows = tbQuery.Text.ToLower()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var row in queryRows)
            {
                if (string.IsNullOrEmpty(row)) continue;
                //var modifiedRow = string.Join("|", row.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(r => "\""+  r.Trim() + "\"*"));
                var modifiedRow = string.Join(" ",
                    row.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(r => r + "*"));
                queryContainer.Add(Query<Content>
                    .QueryString(q => q.Query(modifiedRow))
                    );
            }

            var excludeRows = tbExclude.Text.ToLower()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var row in excludeRows)
            {
                if (string.IsNullOrEmpty(row)) continue;
                var modifiedRow = string.Join(" ",
                    row.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(r => "-*" + r.Trim() + "*"));
                queryContainer.Add(Query<Content>
                    .QueryString(q => q.Query(modifiedRow))
                    );
            }

            return queryContainer;
        }

        #endregion //Search

        #region Utils

        private List<string> GetHistoryIdc(DataGridView dgv)
        {
            var historyIdc = new List<string>();
            var selectedCount = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!(bool)row.Cells[nameof(Content.Selected)].Value) continue;
                selectedCount++;
                historyIdc.Add(row.Cells[nameof(Content.Id)].Value.ToString());
            }
            if (selectedCount == 0 && _founded != null && _founded.Count > 0)
            {
                historyIdc = _founded.Select(e => e.Id).ToList();
            }

            return historyIdc;
        }

        private void SetCalculation(DataGridView dgv)
        {
            var sellerCount = _founded.Select(e => e.Seller).Distinct().Count();
            lblAllSellerCount.Text = sellerCount.ToString();
            lblAllNameCount.Text = _hitsTotal.ToString();

            var selectedSeller = new List<string>();
            var selectedCount = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!(bool)row.Cells[nameof(Content.Selected)].Value) continue;
                selectedCount++;
                selectedSeller.Add(row.Cells[nameof(Content.Seller)].Value.ToString());
            }
            lblSelectedSellerCount.Text = selectedSeller.Distinct().Count().ToString();
            lblSelectedNameCount.Text = selectedCount.ToString();
            string calculationText;
            var prices = new List<double>();
            foreach (DataGridViewRow row in dgvSearchResult.Rows)
            {
                if ((bool)row.Cells[nameof(Content.Selected)].Value)
                    prices.Add(Convert.ToDouble(row.Cells[nameof(Content.Price)].Value));
            }
            var calculatedPrice = Utils.GetPriceCalculation(prices, out calculationText);
            lblSelectedAveragePrice.Text = calculatedPrice.ToString(CultureInfo.InvariantCulture);
            btnCalc.Visible = selectedCount > 0;
            btnUpdatePrices.Enabled = selectedCount > 0;
        }

        //private static void SetTablesColumns(DataGridView dgv)
        //{
        //    dgv.RowHeadersWidth = 10;
        //    var dataGridViewColumn = dgv.Columns[nameof(Content.CollectedAt)];
        //    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
        //    dataGridViewColumn = dgv.Columns[nameof(Content.Id)];
        //    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
        //    dataGridViewColumn = dgv.Columns[nameof(Content.Seller)];
        //    if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
        //    dataGridViewColumn = dgv.Columns[nameof(Content.Selected)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.SortMode = DataGridViewColumnSortMode.Automatic;
        //        dataGridViewColumn.HeaderText = @"Отмечен";
        //        dataGridViewColumn.Width = 60;
        //        dataGridViewColumn.DisplayIndex = 1;
        //    }

        //    dataGridViewColumn = dgv.Columns[nameof(Content.Name)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.Width = 500;
        //        dataGridViewColumn.HeaderText = @"Наименование ТРУ";
        //        dataGridViewColumn.ReadOnly = true;
        //        if (dgv.Name.Equals("dgvSearchResult")) dataGridViewColumn.DisplayIndex = 2;
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Content.Price)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.Visible = false;
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Content.Nprice)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.HeaderText = @"Цена";
        //        dataGridViewColumn.ReadOnly = true;
        //        dataGridViewColumn.DisplayIndex = 3;
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Content.Uri)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.Width = 500;
        //        dataGridViewColumn.HeaderText = @"Ссылка на ТРУ";
        //        dataGridViewColumn.ReadOnly = true;
        //        dataGridViewColumn.DisplayIndex = 4;
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Content.Collected)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.HeaderText = @"Дата";
        //        dataGridViewColumn.DisplayIndex = 5;
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Expert.Price2016)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.HeaderText = @"Цена(2016)";
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Expert.Price2017)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.HeaderText = @"Цена(2017)";
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Expert.Price2018)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.HeaderText = @"Цена(2018)";
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Expert.TruPrim)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.HeaderText = @"Уточнение";
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Expert.Edizm)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.HeaderText = @"Единица измерения";
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Expert.ExpertDate)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.HeaderText = @"Дата";
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Expert.ExpertName)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.HeaderText = @"Эксперт";
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Expert.ExpertNumber)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.HeaderText = @"№ экспертизы";
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Expert.Npp)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.HeaderText = @"№ п/п";
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Expert.KpgzName)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.HeaderText = @"Наименование КПГЗ (ЕАИСТ 2.0)";
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Expert.KpgzCode)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.HeaderText = @"№ КПГЗ (ЕАИСТ 2.0)";
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Expert.SpgzName)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.HeaderText = @"СПГЗ";
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Expert.Okpd2)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.HeaderText = @"ОКПД2";
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Expert.Comment)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.HeaderText = @"Комментарий";
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Expert.Comment2)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.HeaderText = @"Примечание";
        //    }
        //    dataGridViewColumn = dgv.Columns[nameof(Expert.ZakupkiLink)];
        //    if (dataGridViewColumn != null)
        //    {
        //        dataGridViewColumn.HeaderText = @"Ссылка на закупки";
        //    }
        //}

        #endregion //Utils

        #region Analyze text

        private void UpdateDataAnalyze()
        {
            if (tbName.Text.Length <= 2) return;
            HandleTextChangedAnalyze();
        }

        private void HandleTextChangedAnalyze()
        {
            _needUpdateAnalyze = false;
            var text = tbName.Text;

            const string pattern = @"^(\s+|\d+|\w+|[^\d\s\w]+)+$";
            var tmpList = new List<string>();

            var regex = new Regex(pattern);
            if (regex.IsMatch(text))
            {
                var match = regex.Match(text);

                foreach (Capture capture in match.Groups[1].Captures)
                {
                    if (!string.IsNullOrWhiteSpace(capture.Value))
                        tmpList.Add(capture.Value.ToLower());
                }
            }

            var analyzeRequest = new AnalyzeRequest(AppSettings.DefaultIndex)
            {
                /*Analyzer = "russian", /* "standard|russian", https://www.elastic.co/guide/en/elasticsearch/reference/current/indices-analyze.html */
                Text = new[] { text.Replace('.', ' ') /* text */}
            };

            try
            {
                if (!cbNorm.Checked)
                {
                    var result = _elasticClient.Analyze(analyzeRequest);

                    tbExact.Clear();
                    tbQuery.Clear();
                    tbExclude.Clear();
                    tbExactExpert.Clear();
                    tbQueryExpert.Clear();
                    tbExcludeExpert.Clear();

                    foreach (var token in result.Tokens)
                    {
                        if (tmpList.Contains(token.Token))
                        {
                            tbExact.Text += token.Token + Environment.NewLine;
                            tbExactExpert.Text += token.Token + Environment.NewLine;
                            continue;
                        }

                        tbQuery.Text += token.Token + Environment.NewLine;
                        tbQueryExpert.Text += token.Token + Environment.NewLine;
                    }
                }
                btnSearch_Click(null, null);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                MessageBox.Show($"Ошибка анализатора. {e}");
            }
        }

        private void RestartTimerAnalyze()
        {
            timerAnalyze.Stop();
            _canUpdateAnalyze = false;
            timerAnalyze.Start();
        }

        #endregion //Analyze text

        #region Event handlers

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            var dataGridViewColumn = dgv.Columns[nameof(Content.Uri)];
            if (dataGridViewColumn == null || e.RowIndex < 0) return;
            var webAddressString = dgv.Rows[e.RowIndex].Cells[dataGridViewColumn.Index].Value.ToString();
            if (string.IsNullOrEmpty(webAddressString)) return;
            if (cbOpenLink.Checked)
            {
                Process.Start(webAddressString);
            }

            if (cbScreenshotLink.Checked)
            {
                var siteShoterExe = Path.GetFullPath(@"siteshoter\SiteShoter.exe");
                var screenshotFilename = Path.GetFullPath("screenshot_" + DateTime.Now.Ticks + ".png");
                var siteShoterParam =
                    $"/URL {webAddressString} /Filename \"{screenshotFilename}\" /OpenImageAfterSave 1 /MaxBrowserWidth 2000 /MaxBrowserHeight 20000";
                Process.Start(siteShoterExe, siteShoterParam);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var maxTake = (int)udResultCount.Value;
            if (cbExpert.Checked) SearchExpert(maxTake);
            if (cbSearch.Checked) Search(maxTake);
        }

        private void timerAnalyze_Tick(object sender, EventArgs e)
        {
            _canUpdateAnalyze = true;
            timerAnalyze.Stop();
            UpdateDataAnalyze();
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            if (!_needUpdateAnalyze) return;
            if (_canUpdateAnalyze)
            {
                _canUpdateAnalyze = false;
                UpdateDataAnalyze();
            }
            else
            {
                RestartTimerAnalyze();
            }
        }

        private void SearchCondition_TextChanged(object sender, EventArgs e)
        {
            //btnSearch_Click(null, null);
        }

        private void dgvSearchResult_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            SetCalculation((DataGridView)sender);
        }

        private void dgvSearchResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ((DataGridView)sender).CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            _needUpdateAnalyze = true;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            string calculationText;
            var prices = new List<double>();
            foreach (DataGridViewRow row in dgvSearchResult.Rows)
            {
                if ((bool)row.Cells[nameof(Content.Selected)].Value)
                    prices.Add(Convert.ToDouble(row.Cells[nameof(Content.Price)].Value));
            }
            Utils.GetPriceCalculation(prices, out calculationText);
            MessageBox.Show(calculationText, @"Расчет НМЦК");
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvSearchResult.Rows)
            {
                if (!(bool)row.Cells[nameof(Content.Selected)].Value) row.Cells[nameof(Content.Selected)].Value = true;
            }
        }

        private void btnInvertSelection_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvSearchResult.Rows)
            {
                row.Cells[nameof(Content.Selected)].Value = !(bool)row.Cells[nameof(Content.Selected)].Value;
            }
        }

        private void cbExpert_CheckedChanged(object sender, EventArgs e)
        {
            var visible = ((CheckBox)sender).Checked;
            if (visible)
            {
                gbExpert.Visible = true;
                tabControl1.TabPages.Add(tpExpert);
            }
            else
            {
                gbExpert.Visible = false;
                tabControl1.TabPages.Remove(tpExpert);
            }
        }

        private void cbSearch_CheckedChanged(object sender, EventArgs e)
        {
            var visible = ((CheckBox)sender).Checked;
            if (visible)
            {
                pnlSearch.Visible = true;
                tabControl1.TabPages.Add(tpSearch);
            }
            else
            {
                pnlSearch.Visible = false;
                tabControl1.TabPages.Remove(tpSearch);
            }
        }

        private void dgvExpert_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            var dataGridViewColumn = dgv.Columns[nameof(Expert.ZakupkiLink)];
            if (dataGridViewColumn == null || e.RowIndex < 0) return;
            var url = dgv.Rows[e.RowIndex].Cells[dataGridViewColumn.Index].Value.ToString();
            if (string.IsNullOrEmpty(url)) return;

            if (cbOpenLink.Checked) Process.Start(url);

            if (!cbScreenshotLink.Checked) return;
            var shoterExe = Path.GetFullPath(@"siteshoter\SiteShoter.exe");
            var filename = Path.GetFullPath("screenshot_" + DateTime.Now.Ticks + ".png");
            var siteShoterParam =
                $"/URL {url} /Filename \"{filename}\" /OpenImageAfterSave 1 /MaxBrowserWidth 2000 /MaxBrowserHeight 20000";
            Process.Start(shoterExe, siteShoterParam);
        }

        private void cbNorm_CheckedChanged(object sender, EventArgs e)
        {
            gbNorm.Visible = ((CheckBox)sender).Checked;
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog { FileName = "test.xlsx" };
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            var dataTable = (DataTable)bsQuery.DataSource;
            var dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);
            CreateExcelFile.CreateExcelDocument(dataSet, saveFileDialog.FileName);
            if (File.Exists(saveFileDialog.FileName)) Process.Start(saveFileDialog.FileName);
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            var historyIdc = GetHistoryIdc(dgvSearchResult);
            if (!historyIdc.Any()) return;
            var responseHistory = _elasticClient.Search<History>(s => s
                .Size(1000)
                .Query(q => q
                    .Terms(t => t
                        .Field(p => p.Idc)
                        .Terms(historyIdc))))
                ;
            var foundedHistory = responseHistory.Hits.Select(s => s.Source).OrderBy(s => s.Collected).ToList();
            var table = new DataTable();
            using (var reader = ObjectReader.Create(foundedHistory))
            {
                table.Load(reader);
            }
            var priceHistoryForm = new PriceHistoryForm(table);
            priceHistoryForm.Show();
        }

        private void cmbNorm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNorm.SelectedIndex < 0) return;
            _norm = (INorm)cmbNorm.SelectedValue;
            AddNormControlToForm((Control)_norm);
        }

        private void btnUpdatePrices_Click(object sender, EventArgs e)
        {
            var uriList = new List<Uri>();
            foreach (DataGridViewRow row in dgvSearchResult.Rows)
            {
                if ((bool)row.Cells[nameof(Content.Selected)].Value)
                    uriList.Add(new Uri(Convert.ToString(row.Cells[nameof(Content.Uri)].Value)));
            }
            if (string.IsNullOrEmpty(AppSettings.UpdatePriceApi))
            {
                MessageBox.Show(@"Адрес сервиса обновления не указан.");
                return;
            }
            var updatePricesHttpClient = new HttpClient();
            var token = AppSettings.ExternalToken;
            updatePricesHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
            using (var response = updatePricesHttpClient.PostAsJsonAsync($"{AppSettings.UpdatePriceApi}/api/common/updatePrices", uriList).Result)
            {
                if (!response.IsSuccessStatusCode) MessageBox.Show(@"Ошибка обновления цен.");
                MessageBox.Show(@"Обновление цен запущено.");
            }
        }

        #endregion //Event handlers
    }
}