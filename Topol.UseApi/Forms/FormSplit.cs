using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Gma.CodeCloud.Controls.TagCloud;
using Gma.CodeCloud.Controls.Geometry;
using Gma.CodeCloud.Controls.TextAnalyses.Blacklist;
using Gma.CodeCloud.Controls.TextAnalyses.Processing;

namespace Topol.UseApi.Forms
{
    public partial class FormSplit : Form
    {
        public FormSplit()
        {
            InitializeComponent();
        }
        private const string s_BlacklistTxtFileName = "blacklist.txt";

        public void ButtonGoClick(object sender, EventArgs e)
        {
            IsRunning = true;
            ProcessText();
            IsRunning = false;
        }

        private void ProcessText()
        {
            var blacklist = ComponentFactory.CreateBlacklist(true);
            var customBlacklist = CommonBlacklist.CreateFromTextFile(s_BlacklistTxtFileName);

            var inputType = ComponentFactory.DetectInputType(textBox.Text);
            var progress = ComponentFactory.CreateProgressBar(inputType, progressBar);
            var terms = ComponentFactory.CreateExtractor(inputType, textBox.Text, progress);
            var stemmer = ComponentFactory.CreateWordStemmer(true);

            var words = terms
                .Filter(blacklist)
                .Filter(customBlacklist)
                .CountOccurences()
                .ToList();

            cloudControl.WeightedWords =
                words
                    .GroupByStem(stemmer)
                    .SortByOccurences()
                    //.Where(z => z.Occurrences > 3)
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
            var mousePositionRelativeToControl = cloudControl.PointToClient(new Point(MousePosition.X, MousePosition.Y));
            if (!cloudControl.TryGetItemAtLocation(mousePositionRelativeToControl, out itemUderMouse)) return;
            MessageBox.Show(itemUderMouse.Word.GetCaption(), $@"Statistics for word [{itemUderMouse.Word.Text}]");
        }
    }
}
