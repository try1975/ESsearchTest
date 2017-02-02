using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ESsearchTest.Model;
using FastMember;
using Nest;

namespace ESsearchTest
{
    public partial class Form1 : Form
    {
        private bool _canUpdateAnalyze = true;
        private ConnectionSettings _connectionSettings;
        private ElasticClient _elasticClient;
        private List<Content> _founded;
        private long _hitsTotal;
        private bool _needUpdateAnalyze;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var defaultIndex = ConfigurationManager.AppSettings["ElasticSearchDefaultIndex"];
            if (string.IsNullOrEmpty(defaultIndex)) defaultIndex = @"md5";
            var address = ConfigurationManager.AppSettings["ElasticSearchAddress"];
            if (string.IsNullOrEmpty(address)) address = @"http://localhost:9200/";
            var userName = ConfigurationManager.AppSettings["ElasticSearchUserName"];
            if (string.IsNullOrEmpty(userName)) userName = @"elastic";
            var password = ConfigurationManager.AppSettings["ElasticSearchPassword"];
            if (string.IsNullOrEmpty(password)) password = @"QJiIN6X3FOEnRWCZEnrRtFQd";

            _connectionSettings = new ConnectionSettings(new Uri(address))
                .OnRequestCompleted(details => Debug.WriteLine(Encoding.UTF8.GetString(details.RequestBodyInBytes)))
                .DisableDirectStreaming()
                .DefaultIndex(defaultIndex)
                .BasicAuthentication(userName, password)
                //.PrettyJson()
                ;

            _elasticClient = new ElasticClient(_connectionSettings);
            lblIndexDocCount.Text = _elasticClient.Count<Content>().Count.ToString();
            //tabControl1.TabPages.Remove(tpExpert);

            var uniqSellerResponce = _elasticClient.Search<Content>(s => s
                .Aggregations(a => a
                    .Terms("unique", te => te
                        .Field("seller")
                        .MinimumDocumentCount(10)
                        .Size(1000)
                    )
                )
                );
            var sellers = uniqSellerResponce.Aggs.Terms("unique").Buckets.Select(b => b.Key).ToList();
            sellers.Sort();
            foreach (var seller in sellers)
            {
                checkedListBox1.Items.Add(seller);
            }
        }

        private void SearchExpert()
        {
            var queryContainer = new List<QueryContainer>();
            var exactRows = tbExactExpert.Text.ToLower().Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            foreach (var row in exactRows)
            {
                if (string.IsNullOrEmpty(row)) continue;
                queryContainer.Add(Query<Content>
                    .Match(m => m
                        .Field(p => p.Name)
                        .Query(row.Trim())));
            }

            var queryRows = tbQueryExpert.Text.ToLower().Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            foreach (var row in queryRows)
            {
                if (string.IsNullOrEmpty(row)) continue;
                var modifiedRow = string.Join(" ",
                    row.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Select(r => /*'*' +*/ r + '*'));
                queryContainer.Add(Query<Content>
                    .QueryString(q => q.Query(modifiedRow))
                    );
            }

            var excludeRows = tbExcludeExpert.Text.ToLower().Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            foreach (var row in excludeRows)
            {
                if (string.IsNullOrEmpty(row)) continue;
                var modifiedRow = string.Join(" ",
                    row.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Select(r => "-*" + r + '*'));
                queryContainer.Add(Query<Content>
                    .QueryString(q => q.Query(modifiedRow))
                    );
            }


            if (queryContainer.Count <= 0) return;

            var responseExpert = _elasticClient.Search<Expert>(s => s
                .Take((int) numericUpDown1.Value)
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
            SetTablesColumns(dgvExpert);
        }

        private void Search()
        {
            //Search query to retrieve info
            var queryContainer = new List<QueryContainer>();
            var exactRows = tbExact.Text.ToLower().Split(new[] {Environment.NewLine}, StringSplitOptions.None);
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

            if (checkedListBox1.CheckedItems.Count > 0)
            {
                var sellers = string.Join(" ", checkedListBox1.CheckedItems.OfType<string>());
                queryContainer.Add(Query<Content>
                    .Match(m => m
                        .Field(p => p.Seller)
                        .Query(sellers.Trim())));
            }

            var queryRows = tbQuery.Text.ToLower().Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            foreach (var row in queryRows)
            {
                if (string.IsNullOrEmpty(row)) continue;
                var modifiedRow = string.Join(" ",
                    row.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Select(r => /*'*' +*/ r + '*'));
                queryContainer.Add(Query<Content>
                    .QueryString(q => q.Query(modifiedRow))
                    );
            }

            var excludeRows = tbExclude.Text.ToLower().Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            foreach (var row in excludeRows)
            {
                if (string.IsNullOrEmpty(row)) continue;
                var modifiedRow = string.Join(" ",
                    row.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Select(r => "-*" + r + '*'));
                queryContainer.Add(Query<Content>
                    .QueryString(q => q.Query(modifiedRow))
                    );
            }


            if (queryContainer.Count <= 0) return;

            var response = _elasticClient.Search<Content>(s => s
                .Take((int) numericUpDown1.Value)
                .Query(q => q
                    .Bool(b => b
                        .Must(queryContainer.ToArray())))
                );

            _founded = response.Hits.Select(s => s.Source).ToList();

            _hitsTotal = response.Total;

            var table = new DataTable();
            using (var reader = ObjectReader.Create(_founded))
            {
                table.Load(reader);
            }
            bsQuery.DataSource = table;

            // bsQuery.DataSource = _founded;
            dgvSearchResult.DataSource = bsQuery;

            SetTablesColumns(dgvSearchResult);
            SetCalculation(dgvSearchResult);
            lblIndexDocCount.Text = _elasticClient.Count<Content>().Count.ToString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cbExpert.Checked) SearchExpert();
            if (cbSearch.Checked) Search();
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
                if ((bool) row.Cells["Selected"].Value)
                {
                    selectedCount++;
                    selectedSeller.Add(row.Cells["Seller"].Value.ToString());
                }
            }
            lblSelectedSellerCount.Text = selectedSeller.Distinct().Count().ToString();
            lblSelectedNameCount.Text = selectedCount.ToString();
            string calculationText;
            var prices = new List<double>();
            foreach (DataGridViewRow row in dgvSearchResult.Rows)
            {
                if ((bool) row.Cells["Selected"].Value) prices.Add(Convert.ToDouble(row.Cells["Price"].Value));
            }
            var calculatedPrice = GetPriceCalculation(prices, out calculationText);
            lblSelectedAveragePrice.Text = calculatedPrice.ToString(CultureInfo.InvariantCulture);
            btnCalc.Visible = selectedCount > 0;
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView) sender;
            var dataGridViewColumn = dgv.Columns["URI"];
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

        private static void SetTablesColumns(DataGridView dgv)
        {
            dgv.RowHeadersWidth = 10;
            var dataGridViewColumn = dgv.Columns["CollectedAt"];
            if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
            dataGridViewColumn = dgv.Columns["Id"];
            if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
            dataGridViewColumn = dgv.Columns["Seller"];
            if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
            dataGridViewColumn = dgv.Columns["Selected"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.SortMode = DataGridViewColumnSortMode.Automatic;
                dataGridViewColumn.HeaderText = @"Отмечен";
                dataGridViewColumn.Width = 60;
                dataGridViewColumn.DisplayIndex = 1;
            }
            dataGridViewColumn = dgv.Columns["Name"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.Width = 500;
                dataGridViewColumn.HeaderText = @"Наименование ТРУ";
                dataGridViewColumn.ReadOnly = true;
                if (dgv.Name.Equals("dgvSearchResult")) dataGridViewColumn.DisplayIndex = 2;
            }
            dataGridViewColumn = dgv.Columns["Price"];
            if (dataGridViewColumn != null)
            {
                //dataGridViewColumn.HeaderText = @"Цена";
                //dataGridViewColumn.ReadOnly = true;
                //dataGridViewColumn.DisplayIndex = 3;
                dataGridViewColumn.Visible = false;
            }
            dataGridViewColumn = dgv.Columns["Nprice"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Цена";
                dataGridViewColumn.ReadOnly = true;
                dataGridViewColumn.DisplayIndex = 3;
            }
            dataGridViewColumn = dgv.Columns["URI"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Ссылка на ТРУ";
                dataGridViewColumn.ReadOnly = true;
                dataGridViewColumn.DisplayIndex = 4;
            }
            dataGridViewColumn = dgv.Columns["Collected"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Дата";
                dataGridViewColumn.DisplayIndex = 5;
            }
            dataGridViewColumn = dgv.Columns["price_2016"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Цена(2016)";
            }
            dataGridViewColumn = dgv.Columns["price_2017"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Цена(2017)";
            }
            dataGridViewColumn = dgv.Columns["price_2018"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Цена(2018)";
            }
            dataGridViewColumn = dgv.Columns["tru_prim"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Уточнение";
            }
            dataGridViewColumn = dgv.Columns["edizm"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Единица измерения";
            }
            dataGridViewColumn = dgv.Columns["expert_date"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Дата";
            }
            dataGridViewColumn = dgv.Columns["expert_name"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Эксперт";
            }
            dataGridViewColumn = dgv.Columns["expert_number"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"№ экспертизы";
            }
            dataGridViewColumn = dgv.Columns["npp"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"№ п/п";
            }
            dataGridViewColumn = dgv.Columns["kpgz_name"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Наименование КПГЗ (ЕАИСТ 2.0)";
            }
            dataGridViewColumn = dgv.Columns["kpgz_code"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"№ КПГЗ (ЕАИСТ 2.0)";
            }
            dataGridViewColumn = dgv.Columns["spgz_name"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"СПГЗ";
            }
            dataGridViewColumn = dgv.Columns["okpd2"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"ОКПД2";
            }
            dataGridViewColumn = dgv.Columns["comment"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Комментарий";
            }
            dataGridViewColumn = dgv.Columns["comment2"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Примечание";
            }
            dataGridViewColumn = dgv.Columns["zakupki_link"];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Ссылка на закупки";
            }
        }

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

            var request = new AnalyzeRequest
            {
                Analyzer = "russian",
                Text = new[] {text}
            };

            var result = _elasticClient.Analyze(request);

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
            btnSearch_Click(null, null);
        }

        private void RestartTimerAnalyze()
        {
            timerAnalyze.Stop();
            _canUpdateAnalyze = false;
            timerAnalyze.Start();
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
            return;
            btnSearch_Click(null, null);
        }

        private void dgvSearchResult_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            SetCalculation((DataGridView) sender);
        }

        private void dgvSearchResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ((DataGridView) sender).CommitEdit(DataGridViewDataErrorContexts.Commit);
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
                if ((bool) row.Cells["Selected"].Value) prices.Add(Convert.ToDouble(row.Cells["Price"].Value));
            }
            GetPriceCalculation(prices, out calculationText);
            MessageBox.Show(calculationText, @"Расчет НМЦК");
        }

        private static double GetPriceCalculation(List<double> prices, out string calculationText)
        {
            prices.Sort();
            var count = prices.Count;

            var sbText = new StringBuilder();
            sbText.Append(
                @"1.Выборка ценовых показателей упорядочивается по возрастанию. Каждому значению цены, начиная с минимального, присваивается порядковый номер:");
            var icount = 0;
            foreach (var price in prices)
            {
                icount++;
                sbText.Append($" {icount}) {price};");
            }
            sbText.Append(Environment.NewLine);
            sbText.AppendLine($"2. Количество ценовых показателей K= {prices.Count()}");
            if (prices.Count() < 10)
            {
                sbText.Append(@"3. В выборке должно быть не менее чем 10 цен");
                calculationText = sbText.ToString();
                return -2;
            }

            var min = count/4;
            var max = count*3/4;
            var del = true;
            if (count%4 > 0)
            {
                min++;
                max++;
                del = false;
            }
            var curCount = 1;
            var minPrice = Convert.ToDouble(0);
            var maxPrice = Convert.ToDouble(0);
            var nextMinPrice = Convert.ToDouble(0);
            var nextMaxPrice = Convert.ToDouble(0);
            foreach (var price in prices)
            {
                if (curCount == min)
                {
                    minPrice = price;
                }
                if (curCount == min + 1)
                {
                    nextMinPrice = price;
                }
                if (curCount == max)
                {
                    maxPrice = price;
                }
                if (curCount == max + 1)
                {
                    nextMaxPrice = price;
                }
                curCount++;
            }
            var min_ind = "Ц[" + min + "] ";
            var max_ind = "Ц[" + max + "] ";
            if (del)
            {
                minPrice = (minPrice + nextMinPrice)/2;
                min_ind = "(Ц[" + min + "]+Ц[" + (min + 1) + "])/2 ";
                maxPrice = (maxPrice + nextMaxPrice)/2;
                max_ind = "(Ц[" + max + "]+Ц[" + (max + 1) + "])/2 ";
            }
            sbText.AppendLine($"3. Минимальное значение цены Цмин={min_ind} ={minPrice}");
            sbText.AppendLine($"4. Максимальное значение цены Цмакс={max_ind} ={maxPrice}");
            if (maxPrice >= minPrice*5/4)
            {
                maxPrice = minPrice*5/4;
                sbText.AppendLine($"5. Верхняя граница диапазона Limmax={maxPrice}");
            }
            var sum = 0.0;
            var countPriceInCalc = 0;
            sbText.AppendLine($"6. Выборка цен, входящая в диапазон от  {minPrice} до  {maxPrice}:");
            foreach (var price in prices)
            {
                if (!(price >= minPrice) || !(price <= maxPrice)) continue;
                sbText.Append($" {price}; ");
                sum = sum + price;
                countPriceInCalc++;
            }
            sbText.Append(Environment.NewLine);

            var calculatedPrice = 0.0;
            if (countPriceInCalc < 3)
            {
                calculatedPrice = -1;
                sbText.AppendLine(
                    $"7. В получившейся выборке цен недостаточно. Необходима экспертная оценка выборки, и определение параметров ценовых групп.");
            }
            else if (countPriceInCalc > 0)
            {
                calculatedPrice = Math.Round(sum/countPriceInCalc, 2, MidpointRounding.AwayFromZero);
            }
            sbText.Append($"7. Итого цена предмета государственного заказа: {calculatedPrice}");
            calculationText = sbText.ToString();
            return calculatedPrice;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvSearchResult.Rows)
            {
                if (!(bool) row.Cells["Selected"].Value) row.Cells["Selected"].Value = true;
            }
        }

        private void btnInvertSelection_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvSearchResult.Rows)
            {
                row.Cells["Selected"].Value = !(bool) row.Cells["Selected"].Value;
            }
        }

        private void cbExpert_CheckedChanged(object sender, EventArgs e)
        {
            var visible = ((CheckBox) sender).Checked;
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
            var visible = ((CheckBox) sender).Checked;
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
            var dgv = (DataGridView) sender;
            var dataGridViewColumn = dgv.Columns["zakupki_link"];
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
    }
}