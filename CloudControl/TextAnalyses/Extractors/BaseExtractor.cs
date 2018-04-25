using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Gma.CodeCloud.Controls.TextAnalyses.Extractors
{
    public abstract class BaseExtractor : IEnumerable<string>
    {
        protected BaseExtractor(IProgressIndicator progressIndicator)
        {
            ProgressIndicator = progressIndicator;
        }

        protected IProgressIndicator ProgressIndicator { get; set; }

        public IEnumerator<string> GetEnumerator()
        {
            return GetWords().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract IEnumerable<string> GetWords();

        protected virtual IEnumerable<string> GetWords(string text)
        {
            var word = new StringBuilder();
            foreach (var ch in text)
            {
                if (char.IsLetterOrDigit(ch))
                {
                    word.Append(ch);
                }
                else
                {
                    if (word.Length > 1)
                    {
                        yield return word.ToString();
                        OnWordPorcessed(word);
                    }
                    word.Clear();
                }
                OnCharPorcessed(ch);
            }
        }

        protected virtual void OnCharPorcessed(char ch)
        {
        }

        protected virtual void OnWordPorcessed(StringBuilder word)
        {
        }

        protected virtual void OnLinePorcessed(string line)
        {
        }
    }
}