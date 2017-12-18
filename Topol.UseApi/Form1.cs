using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Common.Dto;
using Common.Dto.Model;
using Common.Dto.Model.Packet;
using Newtonsoft.Json;
using Topol.UseApi.Data.Common;

namespace Topol.UseApi
{
    public partial class Form1 : Form
    {
        private readonly DataMаnager _dataManager;
        private readonly List<SearchItemDto> _listSearchItem = new List<SearchItemDto>();
        private DataTable _datatableSearchItem;


        private bool EnableResultButtons
        {
            set
            {
                btnSaveInternetResults.Enabled = value;
                btnInvertSelected.Enabled = value;
                btnDeleteSelected.Enabled = value;
            }
        }

        private BindingSource PacketItemsBindingSource { get; }
        private BindingSource ContentItemsBindingSource { get; }
        private BindingSource MaybeItemsBindingSource { get; }
        private BindingSource Okpd2ItemsBindingSource { get; }
        private readonly BackgroundWorker _bw = new BackgroundWorker();


        public Form1()
        {
            InitializeComponent();
            cmbElasticIndexName.SelectedIndex = 0;
            cmbNorm.SelectedIndex = 0;
            _dataManager = new DataMаnager();

            PacketItemsBindingSource = new BindingSource();
            ContentItemsBindingSource = new BindingSource();
            MaybeItemsBindingSource = new BindingSource();
            Okpd2ItemsBindingSource = new BindingSource();

            PacketItemsBindingSource.CurrentChanged += BindingSourceOnCurrentChanged;
            dgvPacketItems.FilterStringChanged += dgvPacketItems_FilterStringChanged;
            dgvPacketItems.SortStringChanged += dgvPacketItems_SortStringChanged;

            dgvContentItems.FilterStringChanged += dgvContentItems_FilterStringChanged;
            dgvContentItems.SortStringChanged += dgvContentItems_SortStringChanged;

            dgvMaybe.FilterStringChanged += dgvMaybe_FilterStringChanged;
            dgvMaybe.SortStringChanged += dgvMaybe_SortStringChanged;

            dgvOkpd2.FilterStringChanged += dgvOkpd2_FilterStringChanged;
            dgvOkpd2.SortStringChanged += dgvOkpd2_SortStringChanged;

            dgvContentItems.CellContentDoubleClick += dgv_CellContentDoubleClick;
            dgvContentItems.CellContentClick += dgvContentItems_CellContentClick;
            dgvContentItems.DataError += dgvContentItems_DataError;
            

            dgvMaybe.CellContentDoubleClick += dgv_CellContentDoubleClick;

            _bw.WorkerSupportsCancellation = false;
            _bw.WorkerReportsProgress = true;
            _bw.DoWork += bw_DoWork;
            _bw.ProgressChanged += bw_ProgressChanged;


            var baseApi = ConfigurationManager.AppSettings["BaseApi"];
            linkLabel1.Text = $@"Описание API - {baseApi}help";

            btnCallMaybe.Click += btnCallMaybe_Click;
            btnOkpd2.Click += btnOkpd2_Click;
            btnLoad.Click += btnLoad_Click;
            btnSearchPacket.Click += btnSearchPacket_Click;
            btnSaveInternetResults.Click += btnSaveInternetResults_Click;
            btnInvertSelected.Click += btnInvertSelected_Click;
            btnDeleteSelected.Click += btnDeleteSelected_Click;
        }

        private void dgvContentItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }


        #region BackgroundWorker

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var searchPacketTaskDto = (SearchPacketTaskDto)e.UserState;
            AddPacketItemsToList(searchPacketTaskDto);
            SearchPacketTaskStore.Post(searchPacketTaskDto);
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var cnt = 1;
            while (cnt > 0)
            {
                var tasks = SearchPacketTaskStore.Dictionary.Values.Where(z => z.ProcessedAt == null).ToList();
                cnt = tasks.Count;
                foreach (var item in tasks)
                {
                    try
                    {
                        var searchPacketTaskDto = _dataManager.GetPacketStatus(item.Id, item.Source).Result;
                        if (searchPacketTaskDto == null) continue;
                        ((BackgroundWorker)sender).ReportProgress(0, searchPacketTaskDto);
                        //else
                        //{
                        //    MessageBox.Show(@"Ошибка запроса.");
                        //}
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine(exception);
                    }
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        #endregion //BackgroundWorker

        #region Grid Settings

        private void PacketGridColumnSettings()
        {
            var dgv = dgvPacketItems;

            // установить видимость полей
            var column = dgvPacketItems.Columns[nameof(SearchItemDto.Key)];
            if (column != null) column.Visible = false;
            column = dgvPacketItems.Columns[nameof(SearchItemDto.SearchItem)];
            if (column != null) column.Visible = false;
            column = dgvPacketItems.Columns[nameof(SearchItemDto.Content)];
            if (column != null) column.Visible = false;
            column = dgvPacketItems.Columns[nameof(SearchItemDto.StartProcessed)];
            if (column != null) column.Visible = false;
            column = dgvPacketItems.Columns[nameof(SearchItemDto.LastUpdate)];
            if (column != null) column.Visible = false;
            column = dgvPacketItems.Columns[nameof(SearchItemDto.ProcessedAt)];
            if (column != null) column.Visible = false;
            column = dgvPacketItems.Columns[nameof(SearchItemDto.Status)];
            if (column != null) column.Visible = false;

            column = dgv.Columns[nameof(SearchItemDto.Source)];
            if (column != null)
            {
                column.HeaderText = @"Источник";
            }
            column = dgv.Columns[nameof(SearchItemDto.Id)];
            if (column != null)
            {
                column.HeaderText = @"Идентификатор";
            }
            column = dgv.Columns[nameof(SearchItemDto.Name)];
            if (column != null)
            {
                column.HeaderText = @"Наименование ТРУ";
            }
            column = dgv.Columns[nameof(SearchItemDto.StartProcessedDateTime)];
            if (column != null)
            {
                column.HeaderText = @"Время старта";
                column.DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss";
            }
            column = dgv.Columns[nameof(SearchItemDto.LastUpdateDateTime)];
            if (column != null)
            {
                column.HeaderText = @"Время обновления";
                column.DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss";
            }
            column = dgv.Columns[nameof(SearchItemDto.ProcessedAtDateTime)];
            if (column != null)
            {
                column.HeaderText = @"Время финиша";
                column.DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss";
            }
            column = dgv.Columns[nameof(SearchItemDto.StatusString)];
            if (column != null)
            {
                column.HeaderText = @"Состояние";
            }
            column = dgv.Columns[nameof(SearchItemDto.ContentCount)];
            if (column != null)
            {
                column.HeaderText = @"Найдено цен";
            }
        }

        private void ContentGridPriceVariants(DataGridView dgv)
        {
            const string cmbName = nameof(ContentDto.PriceVariant);
            var column = dgv.Columns[cmbName];
            if (column == null) return;
            var cmbName2 = $"{cmbName}2";
            column = dgv.Columns[cmbName2];
            if (column == null)
            {
                var cmb = new DataGridViewComboBoxColumn
                {
                    HeaderText = @"Select Data",
                    Name = cmbName2,
                    //DataSource = new string[] { "a", "b", "c" },
                    MaxDropDownItems = 7,
                    //DisplayMember = cmbName,
                    DataPropertyName = "PriceVariant",
                    //ValueMember =  cmbName
                };
                dgv.Columns.Add(cmb);
            }
            foreach (DataGridViewRow row in dgv.Rows)
            {
                var value = row.Cells[nameof(ContentDto.PriceVariants)].Value.ToString();
                if (string.IsNullOrEmpty(value)) continue;
                var comboBoxCell = row.Cells[cmbName2] as DataGridViewComboBoxCell;
                if (comboBoxCell == null) continue;
                var data = value.Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);


                //comboBoxCell.Items.AddRange(data);
                //comboBoxCell.Value = row.Cells[nameof(ContentDto.PriceVariant)].Value.ToString();
                comboBoxCell.DataSource = data;
                // comboBoxCell.ValueMember = cmbName;
                // comboBoxCell.DisplayMember = cmbName;

                comboBoxCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            }
        }

        private void ContentGridColumnSettings(DataGridView dgv)
        {
            ContentGridPriceVariants(dgv);

            // hide all columns
            foreach (DataGridViewColumn dgvColumn in dgv.Columns) dgvColumn.Visible = false;

            var column = dgv.Columns[nameof(ContentDto.Selected)];
            if (column != null)
            {
                column.Visible = true;
                column.SortMode = DataGridViewColumnSortMode.Automatic;
                column.HeaderText = @"Отмечен";
                column.Width = 60;
                column.DisplayIndex = 1;
            }
            column = dgv.Columns[nameof(ContentDto.PriceTypeString)];
            if (column != null)
            {
                column.Visible = true;
                column.SortMode = DataGridViewColumnSortMode.Automatic;
                column.HeaderText = @"Тип цены";
                column.Width = 60;
                column.ReadOnly = true;
                column.DisplayIndex = 2;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            column = dgv.Columns[nameof(ContentDto.Name)];
            if (column != null)
            {
                column.Visible = true;
                column.Width = 500;
                column.HeaderText = @"Наименование ТРУ";
                column.ReadOnly = true;
                column.DisplayIndex = 3;
            }
            column = dgv.Columns[nameof(ContentDto.Nprice)];
            if (column != null)
            {
                column.Visible = true;
                column.HeaderText = @"Цена";
                column.ReadOnly = true;
                column.DisplayIndex = 4;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            column = dgv.Columns[$"{nameof(ContentDto.PriceVariant)}2"];
            if (column != null)
            {
                column.Visible = true;
                column.HeaderText = @"Варианты цены";
                //column.ReadOnly = true;
                column.DisplayIndex = 5;
            }
            column = dgv.Columns[nameof(ContentDto.Uri)];
            if (column != null)
            {
                column.Visible = true;
                column.Width = 500;
                column.HeaderText = @"Ссылка на ТРУ";
                column.ReadOnly = true;
                column.DisplayIndex = 6;
            }
            column = dgv.Columns[nameof(ContentDto.Collected)];
            if (column != null)
            {
                column.Visible = true;
                column.HeaderText = @"Время сбора";
                column.DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                column.DisplayIndex = 7;
            }
            column = dgv.Columns[nameof(ContentDto.Id)];
            if (column != null)
            {
                column.Visible = true;
                column.HeaderText = @"Идентификатор";
            }
        }

        #endregion //Grid Settings

        #region Event handlers

        private void btnSaveInternetResults_Click(object sender, EventArgs e)
        {
            SaveInternetResults(dgvContentItems);
        }

        private async void SaveInternetResults(DataGridView dgv)
        {
            var list = new List<BasicContentDto>();
            var selectedCount = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!(bool)row.Cells[nameof(ContentDto.Selected)].Value) continue;
                var value = (int)row.Cells[nameof(ContentDto.PriceType)].Value;
                if (value != (int)PriceType.Check) continue;
                selectedCount++;
                list.Add(new BasicContentDto
                {
                    Uri = row.Cells[nameof(ContentDto.Uri)].Value.ToString(),
                    Name = row.Cells[nameof(ContentDto.Name)].Value.ToString(),
                    Price = row.Cells[nameof(ContentDto.Price)].Value.ToString()
                });
            }
            if (selectedCount > 0)
            {
                await _dataManager.Post2InternetIndex(list);
            }
        }


        private void btnInvertSelected_Click(object sender, EventArgs e)
        {
            InvertSelected(dgvContentItems);
        }

        private void InvertSelected(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.Cells[nameof(ContentDto.Selected)].Value = !(bool)row.Cells[nameof(ContentDto.Selected)].Value;
            }
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            DeleteSelected(dgvContentItems);
        }

        private void DeleteSelected(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if ((bool)row.Cells[nameof(ContentDto.Selected)].Value)
                {
                    dgv.Rows.Remove(row);
                }
            }
        }

        private void dgvContentItems_SortStringChanged(object sender, EventArgs e)
        {
            ContentItemsBindingSource.Sort = dgvContentItems.SortString;
            ContentGridColumnSettings(dgvContentItems);
        }

        private void dgvContentItems_FilterStringChanged(object sender, EventArgs e)
        {

            ContentItemsBindingSource.Filter = dgvContentItems.FilterString;
            ContentGridColumnSettings(dgvContentItems);
        }

        private void dgvPacketItems_SortStringChanged(object sender, EventArgs e)
        {
            PacketItemsBindingSource.Sort = dgvPacketItems.SortString;
            PacketGridColumnSettings();
        }

        private void dgvPacketItems_FilterStringChanged(object sender, EventArgs e)
        {
            PacketItemsBindingSource.Filter = dgvPacketItems.FilterString;
            PacketGridColumnSettings();
        }

        private void dgvMaybe_SortStringChanged(object sender, EventArgs e)
        {
            MaybeItemsBindingSource.Sort = dgvMaybe.SortString;
            ContentGridColumnSettings(dgvMaybe);
        }

        private void dgvMaybe_FilterStringChanged(object sender, EventArgs e)
        {
            MaybeItemsBindingSource.Filter = dgvMaybe.FilterString;
            ContentGridColumnSettings(dgvMaybe);
        }

        private void dgvOkpd2_SortStringChanged(object sender, EventArgs e)
        {
            Okpd2ItemsBindingSource.Sort = dgvOkpd2.SortString;
        }

        private void dgvOkpd2_FilterStringChanged(object sender, EventArgs e)
        {
            Okpd2ItemsBindingSource.Filter = dgvOkpd2.FilterString;
        }

        private void btnCallMaybe_Click(object sender, EventArgs e)
        {
            var mustRows = tbExact.Text.ToLower()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            //TODO: change ";" to common constant
            var must = string.Join(";", mustRows);
            var shouldRows = tbQuery.Text.ToLower()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var should = string.Join(";", shouldRows);
            var mustNotdRows = tbExclude.Text.ToLower()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var mustNot = string.Join(";", mustNotdRows);
            var source = cmbElasticIndexName.SelectedItem as string;
            SearchMaybe(must, should, mustNot, source);
        }

        private void btnOkpd2_Click(object sender, EventArgs e)
        {
            SearchOkpd2(tbOkpd2.Text.ToLower());
        }

        #endregion //Event handlers

        #region Api calls
        private async void SearchMaybe(string must, string should, string mustNot, string source)
        {
            var contentItems = await _dataManager.GetMaybe(must, should, mustNot, source);
            if (contentItems != null)
            {
                var dt = ConvertToDataTable(contentItems);
                MaybeItemsBindingSource.DataSource = dt;
                dgvMaybe.DataSource = MaybeItemsBindingSource;
                ContentGridColumnSettings(dgvMaybe);
            }
            else
            {
                MessageBox.Show(@"Ошибка запроса.");
            }
        }

        private async void SearchOkpd2(string text)
        {
            var contentItems = await _dataManager.GetOkpd2Reverse(text);
            if (contentItems != null)
            {
                var dt = ConvertToDataTable(contentItems);
                Okpd2ItemsBindingSource.DataSource = dt;
                dgvOkpd2.DataSource = Okpd2ItemsBindingSource;
            }
            else
            {
                MessageBox.Show(@"Ошибка запроса.");
            }
        }

        private async void SearchPacket(string json)
        {
            var dto = JsonConvert.DeserializeObject<List<SearchItemParam>>(json);
            var searchPacketTaskDto = await _dataManager.PostPacketAsync(dto, cmbElasticIndexName.SelectedItem as string);
            if (searchPacketTaskDto != null)
            {
                SearchPacketTaskStore.Post(searchPacketTaskDto);
                AddPacketItemsToList(searchPacketTaskDto);
                if (_bw.IsBusy != true)
                {
                    _bw.RunWorkerAsync();
                }
            }
            else
            {
                MessageBox.Show(@"Ошибка запроса.");
            }
        }
        #endregion //Api calls

        private void dgv_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            var dataGridViewColumn = dgv.Columns[nameof(ContentDto.Uri)];
            if (dataGridViewColumn == null || e.RowIndex < 0) return;
            var webAddressString = dgv.Rows[e.RowIndex].Cells[dataGridViewColumn.Index].Value.ToString();
            if (string.IsNullOrEmpty(webAddressString)) return;
            Process.Start(webAddressString);
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadPacketFromFile();
        }

        private void LoadPacketFromFile()
        {
            var openFileDialog = new OpenFileDialog(); if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            tbFileName.Text = openFileDialog.FileName;
            tbTruItems.Lines = File.ReadAllLines(openFileDialog.FileName);
        }

        private void btnSearchPacket_Click(object sender, EventArgs e)
        {
            SearchPacket(tbTruItems.Text);
        }


        private async void SearchPacket(List<SearchItemParam> dto)
        {
            var searchPacketTaskDto = await _dataManager.PostPacketAsync(dto, cmbElasticIndexName.SelectedItem as string);
            if (searchPacketTaskDto != null)
            {
                SearchPacketTaskStore.Post(searchPacketTaskDto);
                AddPacketItemsToList(searchPacketTaskDto);
                if (_bw.IsBusy != true)
                {
                    _bw.RunWorkerAsync();
                }
            }
            else
            {
                MessageBox.Show(@"Ошибка запроса.");
            }
        }

        private void AddPacketItemsToList(SearchPacketTaskDto searchPacketTaskDto)
        {
            if (searchPacketTaskDto.SearchItems == null) return;
            foreach (var searchItemDto in searchPacketTaskDto.SearchItems)
            {
                var item = _listSearchItem.FirstOrDefault(z => z.Id.Equals(searchItemDto.Key) && z.Source.Equals(searchPacketTaskDto.Source));
                if (item == null)
                {
                    AddSeacrhItemToList(searchPacketTaskDto, searchItemDto);
                }
                else
                {
                    //if (item.Status == searchItemDto.Status) continue;
                    var idx = _listSearchItem.IndexOf(item);
                    _listSearchItem.Remove(item);
                    AddSeacrhItemToList(searchPacketTaskDto, searchItemDto, idx);
                }
            }
            List2Grid(ref _datatableSearchItem);
        }

        private void AddSeacrhItemToList(SearchPacketTaskDto searchPacketTaskDto, SearchItemDto searchItemDto, int index = 0)
        {
            searchItemDto.Source = searchPacketTaskDto.Source;
            _listSearchItem.Add(searchItemDto);
            //if (index < 0 || index > _listSearchItem.Count - 1) index = 0;
            //_listSearchItem.Insert(index, searchItemDto);
        }

        private void List2Grid(ref DataTable dataTable)
        {
            if (dataTable == null)
            {
                dataTable = ConvertToDataTable(_listSearchItem);
                //foreach (DataRow row in dataTable.Rows)
                //{
                //    var text = row[nameof(SearchItemDto.Name)].ToString();
                //    var okpd2List = await _dataManager.GetOkpd2Reverse(text.Replace(";",""));
                //    if (okpd2List!=null && okpd2List.Any()) row[nameof(SearchItemDto.Okpd2)] = okpd2List.FirstOrDefault().Okpd2;
                //}
                PacketItemsBindingSource.DataSource = dataTable;
                dgvPacketItems.DataSource = PacketItemsBindingSource;
                PacketGridColumnSettings();
            }
            else
            {
                var properties = TypeDescriptor.GetProperties(typeof(SearchItemDto));
                foreach (var searchItemDto in _listSearchItem)
                {
                    var foundRows = dataTable.Select($"{nameof(searchItemDto.Key)}='{searchItemDto.Key}'");
                    if (foundRows.Any())
                    {
                        foreach (var dataRow in foundRows)
                        {
                            dataRow.SetField(nameof(searchItemDto.StartProcessed), searchItemDto.StartProcessed);
                            dataRow.SetField(nameof(searchItemDto.LastUpdate), searchItemDto.LastUpdate);
                            dataRow.SetField(nameof(searchItemDto.ProcessedAt), searchItemDto.ProcessedAt);
                            dataRow.SetField(nameof(searchItemDto.StartProcessedDateTime), searchItemDto.StartProcessedDateTime);
                            dataRow.SetField(nameof(searchItemDto.LastUpdateDateTime), searchItemDto.LastUpdateDateTime);
                            dataRow.SetField(nameof(searchItemDto.ProcessedAtDateTime), searchItemDto.ProcessedAtDateTime);
                            dataRow.SetField(nameof(searchItemDto.Status), searchItemDto.Status);
                            dataRow.SetField(nameof(searchItemDto.StatusString), searchItemDto.StatusString);
                            dataRow.SetField(nameof(searchItemDto.ContentCount), searchItemDto.ContentCount);
                        }
                    }
                    else
                    {
                        var row = dataTable.NewRow();
                        foreach (PropertyDescriptor prop in properties)
                            row[prop.Name] = prop.GetValue(searchItemDto) ?? DBNull.Value;

                        //var text = searchItemDto.Name;
                        //var okpd2List = await _dataManager.GetOkpd2Reverse(text.Replace(";", ""));
                        //if (okpd2List != null && okpd2List.Any()) row[nameof(SearchItemDto.Okpd2)] = okpd2List.FirstOrDefault().Okpd2;
                        dataTable.Rows.Add(row);
                    }
                }
                //    var dt = ConvertToDataTable(_listSearchItem);
                //PacketItemsBindingSource.DataSource = dt;
                //dgvPacketItems.DataSource = PacketItemsBindingSource;
                //PacketItemsBindingSource.ResetBindings(metadataChanged: false);
            }
        }

        private static DataTable ConvertToDataTable<T>(IEnumerable<T> data)
        {
            var properties =
                TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (var item in data)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        private void BindingSourceOnCurrentChanged(object sender, EventArgs e)
        {
            try
            {
                if (PacketItemsBindingSource.Current == null) return;
                var current = (DataRowView)PacketItemsBindingSource.Current;
                var key = current.Row[nameof(SearchItemDto.Key)] as string;
                EnableResultButtons = (int)current.Row[nameof(SearchItemDto.Status)] == (int)TaskStatus.Ok && ((string)current.Row[nameof(SearchItemDto.Source)]).Contains("internet");
                var contentItems = _listSearchItem.FirstOrDefault(z => z.Key.Equals(key))?.Content ?? new List<ContentDto>();
                var dt = ConvertToDataTable(contentItems);
                ContentItemsBindingSource.DataSource = dt;
                dgvContentItems.DataSource = ContentItemsBindingSource;
                ContentGridColumnSettings(dgvContentItems);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                //throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dto = new List<SearchItemParam>
            {
                new SearchItemParam
                {
                    Id = Md5Logstah.GetDefaultId("", textBox2.Text),
                    Name = textBox2.Text,
                    Norm = cmbNorm.SelectedItem as string
                }
            };
            SearchPacket(dto);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var baseApi = ConfigurationManager.AppSettings["BaseApi"];
            Process.Start($"{baseApi}help");
        }

        private void dgvContentItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            return;
            ((DataGridView)sender).CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }
}
