using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Topol.UseApi.Interfaces.Common;
using log4net;
using Topol.UseApi.Forms;
using Common.Dto.Model;
using PriceCommon.Enums;

namespace Topol.UseApi.Controls
{
    public partial class MonitoringControl : UserControl
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(MonitoringControl));
        private readonly IDataMаnager _dataManager;
        //private readonly DataTable _dataTable = new DataTable();
        private BindingSource _bindingSource { get; }
        public MonitoringControl(IDataMаnager dataManager)
        {
            InitializeComponent();

            _dataManager = dataManager;
            //_bindingSource = new BindingSource { DataSource = _dataTable };
            _bindingSource = new BindingSource();
            dgvShedules.DataSource = _bindingSource;


            btnGetSchedules.Click += BtnGetSchedules_Click;
            btnAddSchedule.Click += BtnAddSchedule_Click;
        }

        private async void BtnAddSchedule_Click(object sender, EventArgs e)
        {
            var dto = new MonitoringScheduleDto
            {
                FirstDate = DateTime.Today,
                Frequency = Frequency.Monthly,
                IsActive = true
            };
            var frm = new ScheduleForm(dto);
            //frm.ButtonGoClick(null, null);
            if (frm.ShowDialog() == DialogResult.OK) {
                await _dataManager.PostSchedule(frm._monitoringScheduleDto);
            }
        }

        private async void BtnGetSchedules_Click(object sender, EventArgs e)
        {
            //_dataTable.Rows.Clear();
            var schedules = await _dataManager.GetSchedules();
            if (schedules.Count == 0) MessageBox.Show(@"Не найдено.");
            _bindingSource.DataSource = schedules;
            _bindingSource.ResetBindings(true);
        }
    }
}
