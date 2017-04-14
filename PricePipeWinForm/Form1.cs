using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using PricePipeCore;

namespace PricePipeWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            tbFileName.Text = openFileDialog.FileName;
            tbTruItems.Lines = File.ReadAllLines(openFileDialog.FileName);
            StartCalc();
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            StartCalc();
        }

        private void StartCalc()
        {
            tbTrueResult.Clear();
            var priceCalculator = new Searcher(
                AppSettings.DefaultIndex,
                AppSettings.Host,
                AppSettings.UserName,
                AppSettings.Password);

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var name in tbTruItems.Lines)
            {
                Application.DoEvents();
                var nmck = priceCalculator.GetNmck(name);
                tbTrueResult.AppendText($"\"{name}\";\"{nmck}\"{Environment.NewLine}");
            }
            stopWatch.Stop();
            var msgResult = "Расчет завершен.";
            if (tbTruItems.Lines.Length > 0) msgResult += $"Расчитано {tbTruItems.Lines.Length} ТРУ за {stopWatch.Elapsed}";
            MessageBox.Show(msgResult);
        }
    }
}
