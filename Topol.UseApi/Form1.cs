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
using Topol.UseApi.Logic;

namespace Topol.UseApi
{
    public partial class Form1 : Form
    {
        private readonly DataMаnager _dataManager;
        private readonly List<SearchItemDto> _listSearchItem = new List<SearchItemDto>();
        private BindingSource PacketItemsBindingSource { get; }
        private BindingSource ContentItemsBindingSource { get; }
        private BackgroundWorker bw = new BackgroundWorker();


        public Form1()
        {
            InitializeComponent();
            cbElasticIndexName.SelectedIndex = 0;
            _dataManager = new DataMаnager();
            PacketItemsBindingSource = new BindingSource();
            ContentItemsBindingSource = new BindingSource();
            PacketItemsBindingSource.CurrentChanged += BindingSourceOnCurrentChanged;
            dgvPacketItems.FilterStringChanged += dgvPacketItems_FilterStringChanged;
            dgvPacketItems.SortStringChanged += dgvPacketItems_SortStringChanged;
            dgvContentItems.FilterStringChanged += dgvContentItems_FilterStringChanged;
            dgvContentItems.SortStringChanged += dgvContentItems_SortStringChanged;
            dgvContentItems.CellContentDoubleClick += dgvContentItems_CellContentDoubleClick;

            bw.WorkerSupportsCancellation = false;
            bw.WorkerReportsProgress = true;
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;


            var baseApi = ConfigurationManager.AppSettings["BaseApi"];
            linkLabel1.Text = $@"Описание API - {baseApi}help";
        }

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
                    var searchPacketTaskDto = _dataManager.GetPacketStatus(item.Id, item.Source).Result;
                    if (searchPacketTaskDto == null) continue;
                    ((BackgroundWorker)sender).ReportProgress(0, searchPacketTaskDto);
                    //else
                    //{
                    //    MessageBox.Show(@"Ошибка запроса.");
                    //}
                }
                System.Threading.Thread.Sleep(1000);
            }
        }


        #region Grid Settings

        private void PacketGridColumnSettings()
        {
            // установить видимость полей
            var column = dgvPacketItems.Columns[nameof(SearchItemDto.Key)];
            if (column != null) column.Visible = false;
            column = dgvPacketItems.Columns[nameof(SearchItemDto.SearchItem)];
            if (column != null) column.Visible = false;
            column = dgvPacketItems.Columns[nameof(SearchItemDto.Content)];
            if (column != null) column.Visible = false;
        }

        private void ContentGridColumnSettings()
        {
            var dgv = dgvContentItems;
            // установить видимость полей
            var column = dgv.Columns[nameof(ContentDto.Price)];
            if (column != null) column.Visible = false;
            column = dgv.Columns[nameof(ContentDto.CollectedAt)];
            if (column != null) column.Visible = false;
            column = dgv.Columns[nameof(ContentDto.Selected)];
            if (column != null)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
                column.HeaderText = @"Отмечен";
                column.Width = 60;
                column.DisplayIndex = 1;
            }

            column = dgv.Columns[nameof(ContentDto.Name)];
            if (column != null)
            {
                column.Width = 500;
                column.HeaderText = @"Наименование ТРУ";
                column.ReadOnly = true;
                column.DisplayIndex = 2;
            }
            column = dgv.Columns[nameof(ContentDto.Nprice)];
            if (column != null)
            {
                column.HeaderText = @"Цена";
                column.ReadOnly = true;
                column.DisplayIndex = 3;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            column = dgv.Columns[nameof(ContentDto.Uri)];
            if (column != null)
            {
                column.Width = 500;
                column.HeaderText = @"Ссылка на ТРУ";
                column.ReadOnly = true;
                column.DisplayIndex = 4;
            }
            column = dgv.Columns[nameof(ContentDto.Collected)];
            if (column != null)
            {
                column.HeaderText = @"Дата";
                column.DisplayIndex = 5;
            }
        }

        #endregion //Grid Settings

        #region Event handlers

        private void dgvContentItems_SortStringChanged(object sender, EventArgs e)
        {
            ContentItemsBindingSource.Sort = dgvContentItems.SortString;
            ContentGridColumnSettings();
        }

        private void dgvContentItems_FilterStringChanged(object sender, EventArgs e)
        {

            ContentItemsBindingSource.Filter = dgvContentItems.FilterString;
            ContentGridColumnSettings();
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

        #endregion //Event handlers




        private void dgvContentItems_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
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
            var openFileDialog = new OpenFileDialog(); if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            tbFileName.Text = openFileDialog.FileName;
            tbTruItems.Lines = File.ReadAllLines(openFileDialog.FileName);
            SearchPacket(tbTruItems.Text);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SearchPacket(tbTruItems.Text);
        }

        private async void SearchPacket(string json)
        {
            var dto = JsonConvert.DeserializeObject<List<SearchItemParam>>(json);
            var searchPacketTaskDto = await _dataManager.PostPacket2(dto, cbElasticIndexName.SelectedItem as string);
            if (searchPacketTaskDto != null)
            {
                SearchPacketTaskStore.Post(searchPacketTaskDto);
                AddPacketItemsToList(searchPacketTaskDto);
                if (bw.IsBusy != true)
                {
                    bw.RunWorkerAsync();
                }
            }
            else
            {
                MessageBox.Show(@"Ошибка запроса.");
            }
        }
        private async void SearchPacket(List<SearchItemParam> dto)
        {
            var searchPacketTaskDto = await _dataManager.PostPacket2(dto, cbElasticIndexName.SelectedItem as string);
            if (searchPacketTaskDto != null)
            {
                SearchPacketTaskStore.Post(searchPacketTaskDto);
                AddPacketItemsToList(searchPacketTaskDto);
                if (bw.IsBusy != true)
                {
                    bw.RunWorkerAsync();
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
                    if (item.Status == searchItemDto.Status) continue;
                    var idx = _listSearchItem.IndexOf(item);
                    _listSearchItem.Remove(item);
                    AddSeacrhItemToList(searchPacketTaskDto, searchItemDto, idx);
                }
            }
            List2Grid();
        }

        private void AddSeacrhItemToList(SearchPacketTaskDto searchPacketTaskDto, SearchItemDto searchItemDto, int index = 0)
        {
            searchItemDto.Source = searchPacketTaskDto.Source;
            _listSearchItem.Add(searchItemDto);
            //if (index < 0 || index > _listSearchItem.Count - 1) index = 0;
            //_listSearchItem.Insert(index, searchItemDto);
        }

        private void List2Grid()
        {
            var dt = ConvertToDataTable(_listSearchItem);
            PacketItemsBindingSource.DataSource = dt;
            dgvPacketItems.DataSource = PacketItemsBindingSource;
            PacketGridColumnSettings();
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
                var contentItems = _listSearchItem.FirstOrDefault(z => z.Key.Equals(key))?.Content ?? new List<ContentDto>();
                var dt = ConvertToDataTable(contentItems);
                ContentItemsBindingSource.DataSource = dt;
                dgvContentItems.DataSource = ContentItemsBindingSource;
                ContentGridColumnSettings();
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dto = new List<SearchItemParam>
            {
                new SearchItemParam
                {
                    Id = Md5Logstah.GetDefaultId("",textBox2.Text),
                    Name = textBox2.Text,
                    Norm = comboBox1.SelectedItem as string
                }
            };
            SearchPacket(dto);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var baseApi = ConfigurationManager.AppSettings["BaseApi"];
            Process.Start($"{baseApi}help");
        }
    }
}
