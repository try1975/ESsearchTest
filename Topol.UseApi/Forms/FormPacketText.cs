using System;
using System.Windows.Forms;

namespace Topol.UseApi.Forms
{
    public partial class FormPacketText : Form
    {
        public FormPacketText()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Modal) DialogResult = DialogResult.OK;
        }
    }
}
