using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Common.Dto.Model.Packet;
using Newtonsoft.Json;
using Topol.UseApi.Data.Common;

namespace Topol.UseApi
{
    public partial class Form1 : Form
    {
        private readonly DataMаnager _dataManager;
        private readonly List<SearchItemDto> _listSearchItem = new List<SearchItemDto>();
        private BindingSource PacketItemsBindingSource { get; }
        private BindingSource ContentItemsBindingSource { get; }

        public Form1()
        {
            InitializeComponent();
            _dataManager = new DataMаnager();
            PacketItemsBindingSource = new BindingSource();
            ContentItemsBindingSource = new BindingSource();
            PacketItemsBindingSource.CurrentChanged += BindingSourceOnCurrentChanged;
        }



        private void btnLoad_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog(); if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            tbFileName.Text = openFileDialog.FileName;
            tbTruItems.Lines = File.ReadAllLines(openFileDialog.FileName);
            SearchPacket();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SearchPacket();
        }

        private async void SearchPacket()
        {
            var dto = JsonConvert.DeserializeObject<List<SearchItemParam>>(tbTruItems.Text);
            var searchPacketTaskDto = await _dataManager.PostPacket2(dto,"gz");
            if (searchPacketTaskDto != null)
            {
                AddPaketItemsToGrid(searchPacketTaskDto);
            }
            else
            {
                MessageBox.Show(@"Ошибка запроса.");
            }
        }

        private void AddPaketItemsToGrid(SearchPacketTaskDto searchPacketTaskDto)
        {
            if (searchPacketTaskDto.SearchItems == null) return;
            foreach (var searchItemDto in searchPacketTaskDto.SearchItems)
            {
                var item = _listSearchItem.FirstOrDefault(z => z.Id.Equals(searchItemDto.Id));
                if (item==null)
                {
                    _listSearchItem.Add(searchItemDto);
                }
                else
                {
                    if (item.Status == searchItemDto.Status) continue;
                    _listSearchItem.Remove(item);
                    _listSearchItem.Add(searchItemDto);
                }
            }
            var dt = ConvertToDataTable(_listSearchItem);
            PacketItemsBindingSource.DataSource = dt;
            dgvPacketItems.DataSource = PacketItemsBindingSource;
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
                var id = current.Row[nameof(SearchItemDto.Id)] as string;
                var contentItems = _listSearchItem.FirstOrDefault(z => z.Id.Equals(id))?.Content;
                var dt = ConvertToDataTable(contentItems);
                ContentItemsBindingSource.DataSource = dt;
                dgvContentItems.DataSource = ContentItemsBindingSource;
            }
            catch (Exception exception)
            {
                //throw;
            }
        }
    }
}
