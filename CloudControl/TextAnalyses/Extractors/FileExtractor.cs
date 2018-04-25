using System.Collections.Generic;
using System.IO;

namespace Gma.CodeCloud.Controls.TextAnalyses.Extractors
{
    public class FileExtractor : BaseExtractor
    {
        private readonly FileInfo _mFileInfo;

        public FileExtractor(FileInfo fileInfo, IProgressIndicator progressIndicator)
            : base(progressIndicator)
        {
            _mFileInfo = fileInfo;
        }

        public override IEnumerable<string> GetWords()
        {
            using (var reader = _mFileInfo.OpenText())
            {
                foreach (var word in GetWords(reader))
                    yield return word;
            }
        }

        protected IEnumerable<string> GetWords(StreamReader reader)
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var wordsInLine = GetWords(line);
                foreach (var word in wordsInLine)
                    yield return word;
                OnLinePorcessed(line);
            }
        }

        protected override void OnLinePorcessed(string line)
        {
            ProgressIndicator.Increment(1);
        }
    }
}