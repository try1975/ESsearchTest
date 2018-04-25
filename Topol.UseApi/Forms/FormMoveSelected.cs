using System;
using System.Windows.Forms;

namespace Topol.UseApi.Forms
{
    public partial class FormMoveSelected : Form
    {
        public FormMoveSelected()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Modal) DialogResult = DialogResult.OK;
        }

        private void rbExist_CheckedChanged(object sender, EventArgs e)
        {
            lblId.Enabled = rbExist.Checked;
            tbId.Enabled = rbExist.Checked;
        }

        private void rbNew_CheckedChanged(object sender, EventArgs e)
        {
            lblName.Enabled = rbNew.Checked;
            tbName.Enabled = rbNew.Checked;
            lblExtId.Enabled = rbNew.Checked;
            tbExtId.Enabled = rbNew.Checked;
        }
    }
}
