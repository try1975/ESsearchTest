using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using Gma.CodeCloud.Controls.Geometry;
using Gma.CodeCloud.Controls.TextAnalyses.Blacklist;
using Gma.CodeCloud.Controls.TextAnalyses.Blacklist.En;
using Gma.CodeCloud.Controls.TextAnalyses.Extractors;
using Gma.CodeCloud.Controls.TextAnalyses.Processing;
using Gma.CodeCloud.Controls.TextAnalyses.Stemmers;

namespace ControlDemoApp
{
    public partial class MainForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private const string s_BlacklistTxtFileName = "blacklist.txt";

        private void ButtonGoClick(object sender, EventArgs e)
        {
            IsRunning = true;

            //ProcessTextSimple();
            ProcessText();

            IsRunning = false;
        }

        private void ProcessTextSimple()
        {
            IsRunning = true;

            IBlacklist blacklist = new CommonWords();
            IProgressIndicator progress = new ProgressBarWrapper(progressBar);
            IEnumerable<string> terms = new StringExtractor(textBox.Text, progress);

            cloudControl.WeightedWords =
                terms
                    .Filter(blacklist)
                    .CountOccurences()
                    .SortByOccurences();

            IsRunning = false;
        }

        private void ProcessText()
        {
            IBlacklist blacklist = ComponentFactory.CreateBlacklist(checkBoxExcludeEnglishCommonWords.Checked);
            IBlacklist customBlacklist = CommonBlacklist.CreateFromTextFile(s_BlacklistTxtFileName);

            InputType inputType = ComponentFactory.DetectInputType(textBox.Text);
            IProgressIndicator progress = ComponentFactory.CreateProgressBar(inputType, progressBar);
            IEnumerable<string> terms = ComponentFactory.CreateExtractor(inputType, textBox.Text, progress);
            IWordStemmer stemmer = ComponentFactory.CreateWordStemmer(checkBoxGroupSameStemWords.Checked);

            IEnumerable<IWord> words = terms
                .Filter(blacklist)
                .Filter(customBlacklist)
                .CountOccurences();

            cloudControl.WeightedWords =
                words
                    .GroupByStem(stemmer)
                    .SortByOccurences()
                    .Where(z => z.Occurrences > 3)
                    .Cast<IWord>();
        }

        private bool IsRunning
        {
            set
            {
                buttonCancel.Enabled = value;
                buttonGo.Enabled = !value;
                progressBar.Value = 0;
            }
        }

        private void CancelClick(object sender, EventArgs e)
        {
            IsRunning = false;
        }

        private void CloudControlClick(object sender, EventArgs e)
        {
            LayoutItem itemUderMouse;
            Point mousePositionRelativeToControl = cloudControl.PointToClient(new Point(MousePosition.X, MousePosition.Y));
            if (!cloudControl.TryGetItemAtLocation(mousePositionRelativeToControl, out itemUderMouse))
            {
                return;
            }

            MessageBox.Show(
                itemUderMouse.Word.GetCaption(),
                string.Format("Statistics for word [{0}]", itemUderMouse.Word.Text));
        }

        private void LinkLabelEditCustomBlacklistLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!File.Exists(s_BlacklistTxtFileName))
            {
                using (StreamWriter writer = File.CreateText(s_BlacklistTxtFileName))
                {
                    writer.WriteLine("IgnoreMeOne");
                    writer.WriteLine("IgnoreMeTwo");
                }
            }

            string absoluteFileName = Path.GetFullPath(s_BlacklistTxtFileName);
            Process.Start(absoluteFileName);
        }
    }
}