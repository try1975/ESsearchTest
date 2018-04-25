using System;
using System.Collections.Generic;
using System.IO;

namespace Gma.CodeCloud.Controls.TextAnalyses.Blacklist
{
    public class CommonBlacklist : IBlacklist
    {
        private readonly HashSet<string> _mExcludedWordsHashSet;

        public CommonBlacklist() : this(new string[] { })
        {
        }

        public CommonBlacklist(IEnumerable<string> excludedWords)
            : this(excludedWords, StringComparer.InvariantCultureIgnoreCase)
        {
        }

        public CommonBlacklist(IEnumerable<string> excludedWords, StringComparer comparer)
        {
            _mExcludedWordsHashSet = new HashSet<string>(excludedWords, comparer);
        }

        public bool Countains(string word)
        {
            return _mExcludedWordsHashSet.Contains(word);
        }

        public int Count => _mExcludedWordsHashSet.Count;


        public static IBlacklist CreateFromTextFile(string fileName)
        {
            return
                !File.Exists(fileName)
                    ? new NullBlacklist()
                    : CreateFromStremReader(new FileInfo(fileName).OpenText());
        }

        public static IBlacklist CreateFromStremReader(TextReader reader)
        {
            if (reader == null) throw new ArgumentNullException("reader");
            var commonBlacklist = new CommonBlacklist();
            using (reader)
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    commonBlacklist.Add(line.Trim());
                    line = reader.ReadLine();
                }
            }
            return commonBlacklist;
        }

        public void Add(string line)
        {
            _mExcludedWordsHashSet.Add(line);
        }
    }
}