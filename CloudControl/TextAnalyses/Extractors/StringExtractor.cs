using System.Collections.Generic;

namespace Gma.CodeCloud.Controls.TextAnalyses.Extractors
{
    public class StringExtractor : BaseExtractor
    {
        private readonly string _mText;

        public StringExtractor(string text, IProgressIndicator progressIndicator)
            : base(progressIndicator)
        {
            _mText = text;
            ProgressIndicator = progressIndicator;
            ProgressIndicator.Maximum = _mText.Length;
        }

        public override IEnumerable<string> GetWords()
        {
            return GetWords(_mText);
        }

        protected override void OnCharPorcessed(char ch)
        {
            ProgressIndicator.Increment(1);
        }
    }
}