using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Gma.CodeCloud.Controls.TextAnalyses.Processing
{
    public struct WordGroup : IWord, IEnumerable<IWord>
    {
        private readonly IEnumerable<IWord> _mAssociatedWords;

        public WordGroup(string stem, IEnumerable<IWord> associatedWords)
            : this()
        {
            Stem = stem;
            _mAssociatedWords = associatedWords;
            Occurrences = _mAssociatedWords.Sum(word => word.Occurrences);
            Text = _mAssociatedWords.Max().Text;
        }

        public string Stem { get; set; }

        public string Text { get; }

        public int Occurrences { get; }

        public int CompareTo(IWord other)
        {
            return Occurrences - other.Occurrences;
        }

        public IEnumerator<IWord> GetEnumerator()
        {
            return _mAssociatedWords.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string GetCaption()
        {
            var caption = string.Empty;
            return
                this
                    .OrderByDescending(
                        word => word.Occurrences)
                    .Aggregate(
                        caption,
                        (s, word) => string.Format("{0}\r\n{1}\t{2}", s, word.Text, word.Occurrences));
        }
    }
}