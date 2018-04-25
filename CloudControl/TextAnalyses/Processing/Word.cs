using System.Collections.Generic;

namespace Gma.CodeCloud.Controls.TextAnalyses.Processing
{
    public struct Word : IWord
    {
        public string Text { get; }
        public int Occurrences { get; }

        public Word(KeyValuePair<string, int> textOccurrencesPair)
            : this(textOccurrencesPair.Key, textOccurrencesPair.Value)
        {
        }

        public Word(string text, int occurrences)
            : this()
        {
            Text = text;
            Occurrences = occurrences;
        }

        public int CompareTo(IWord other)
        {
            return Occurrences - other.Occurrences;
        }

        public string GetCaption()
        {
            return string.Format("{0} - occurrences", Occurrences);
        }
    }
}