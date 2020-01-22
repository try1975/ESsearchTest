using System;
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
        private BindingSource _bindingSource { get; }
        
        public MonitoringControl(IDataMаnager dataManager)
        {
            InitializeComponent();

            _dataManager = dataManager;
            _bindingSource = new BindingSource();
            dgvShedules.DataSource = _bindingSource;

            btnGetSchedules.Click += BtnGetSchedules_Click;
            btnAddSchedule.Click += BtnAddSchedule_Click;
            btnChangeSchedule.Click += BtnChangeSchedule_Click;
            btnRemoveSchedule.Click += BtnRemoveSchedule_Click;
        }


        private async void BtnGetSchedules_Click(object sender, EventArgs e)
        {
            var schedules = await _dataManager.GetSchedules();
            if (schedules.Count == 0) MessageBox.Show(@"Не найдено.");
            _bindingSource.DataSource = schedules;
            _bindingSource.ResetBindings(true);
        }

        private async void BtnAddSchedule_Click(object sender, EventArgs e)
        {
            var dto = new ScheduleDto
            {
                NextDate = DateTime.UtcNow.Date.AddDays(1),
                Frequency = Frequency.Monthly,
                IsActive = true
            };
            var frm = new ScheduleForm(dto, ScheduleFormMode.ScheduleFormAdd);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                await _dataManager.PostSchedule(frm._scheduleDto);
            }
        }

        private async void BtnChangeSchedule_Click(object sender, EventArgs e)
        {
            var dto = (ScheduleDto)_bindingSource.Current;
            if (dto == null) return;
            var frm = new ScheduleForm(dto, ScheduleFormMode.ScheduleFormEdit);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                await _dataManager.PostSchedule(frm._scheduleDto);
            }
        }

        private async void BtnRemoveSchedule_Click(object sender, EventArgs e)
        {
            var dto = (ScheduleDto)_bindingSource.Current;
            if (dto == null) return;
            if (MessageBox.Show($"Удалить #{dto.Id} {dto.Name}", "Внимание", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            if (await _dataManager.DeleteSchedule(dto.Id))
            {
                _bindingSource.RemoveCurrent();
            }
            else
            {
                MessageBox.Show(@"Расписание не может быть удалено");
            }
        }
    }
}
