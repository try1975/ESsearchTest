﻿using Common.Dto.Model;
using PriceCommon.Enums;
using System.Windows.Forms;

namespace Topol.UseApi.Forms
{
    public enum ScheduleFormMode { ScheduleFormAdd, ScheduleFormEdit }

    public partial class ScheduleForm : Form
    {
        public ScheduleDto _scheduleDto;
        private readonly ScheduleFormMode _scheduleFormMode;

        public ScheduleForm(ScheduleDto scheduleDto, ScheduleFormMode scheduleFormMode)
        {
            InitializeComponent();
            _scheduleFormMode = scheduleFormMode;
            _scheduleDto = scheduleDto;

            switch (_scheduleDto.Frequency)
            {
                case Frequency.Daily:
                    rbDaily.Checked = true;
                    break;
                case Frequency.Weekly:
                    rbWeekly.Checked = true;
                    break;
                case Frequency.Monthly:
                    rbMonthly.Checked = true;
                    break;
                case Frequency.Every2Month:
                    rbEvery2Month.Checked = true;
                    break;
                case Frequency.Quarterly:
                    rbQuarterly.Checked = true;
                    break;
                case Frequency.HalfYearly:
                    rbHalfYearly.Checked = true;
                    break;
                case Frequency.Annually:
                    rbAnnually.Checked = true;
                    break;
            }
            cbIsActive.Checked = _scheduleDto.IsActive;
            tbUri.Text = _scheduleDto.Uri;
            tbName.Text = _scheduleDto.Name;

            panel2.Visible = (_scheduleFormMode == ScheduleFormMode.ScheduleFormEdit);
            dateTimePicker1.Value = _scheduleDto.NextDate;

            FormClosed += ScheduleForm_FormClosed;
        }

        private void ScheduleForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (rbDaily.Checked) _scheduleDto.Frequency = Frequency.Daily;
            if (rbWeekly.Checked) _scheduleDto.Frequency = Frequency.Weekly;
            if (rbMonthly.Checked) _scheduleDto.Frequency = Frequency.Monthly;
            if (rbEvery2Month.Checked) _scheduleDto.Frequency = Frequency.Every2Month;
            if (rbQuarterly.Checked) _scheduleDto.Frequency = Frequency.Quarterly;
            if (rbHalfYearly.Checked) _scheduleDto.Frequency = Frequency.HalfYearly;
            if (rbAnnually.Checked) _scheduleDto.Frequency = Frequency.Annually;

            _scheduleDto.IsActive = cbIsActive.Checked;
            _scheduleDto.Uri = tbUri.Text;
            _scheduleDto.Name = tbName.Text;
            _scheduleDto.NextDate = dateTimePicker1.Value;
        }
    }
}
