using System.Windows.Forms;
using Gma.CodeCloud.Controls.TextAnalyses.Extractors;

namespace ControlDemoApp
{
    internal class ProgressBarWrapper : IProgressIndicator
    {
        private readonly ProgressBar m_ProgressBar;

        public ProgressBarWrapper(ProgressBar toolStripProgressBar)
        {
            m_ProgressBar = toolStripProgressBar;
        }

        public int Value
        {
            get { return m_ProgressBar.Value; }
            set { m_ProgressBar.Value = value; }
        }

        public virtual int Maximum
        {
            get { return m_ProgressBar.Maximum; }
            set { m_ProgressBar.Maximum = value; }
        }

        public virtual void Increment(int value)
        {
            m_ProgressBar.Increment(value);
            Application.DoEvents();
        }
    }
}