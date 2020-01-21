using Common.Dto.Model;
using PriceCommon.Enums;
using System.Windows.Forms;

namespace Topol.UseApi.Forms
{
    public partial class ScheduleForm : Form
    {
        public MonitoringScheduleDto _monitoringScheduleDto;

        public ScheduleForm(MonitoringScheduleDto monitoringScheduleDto)
        {
            InitializeComponent();
            _monitoringScheduleDto = monitoringScheduleDto;

            switch (_monitoringScheduleDto.Frequency)
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
            cbIsActive.Checked = _monitoringScheduleDto.IsActive;
            tbUri.Text = _monitoringScheduleDto.Uri;

            this.FormClosed += ScheduleForm_FormClosed;
        }

        private void ScheduleForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (rbDaily.Checked) _monitoringScheduleDto.Frequency = Frequency.Daily;
            if (rbWeekly.Checked) _monitoringScheduleDto.Frequency = Frequency.Weekly;
            if (rbMonthly.Checked) _monitoringScheduleDto.Frequency = Frequency.Monthly;
            if (rbEvery2Month.Checked) _monitoringScheduleDto.Frequency = Frequency.Every2Month;
            if (rbQuarterly.Checked) _monitoringScheduleDto.Frequency = Frequency.Quarterly;
            if (rbHalfYearly.Checked) _monitoringScheduleDto.Frequency = Frequency.HalfYearly;
            if (rbAnnually.Checked) _monitoringScheduleDto.Frequency = Frequency.Annually;

            _monitoringScheduleDto.IsActive = cbIsActive.Checked;
            _monitoringScheduleDto.Uri = tbUri.Text;
            _monitoringScheduleDto.Name = tbName.Text;
        }
    }
}
