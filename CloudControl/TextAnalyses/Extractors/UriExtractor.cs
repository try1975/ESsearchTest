using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Gma.CodeCloud.Controls.TextAnalyses.Extractors
{
    public class UriExtractor : FileExtractor
    {
        private readonly Uri _mUri;
        private bool _mIsScriptMode;

        public UriExtractor(Uri uri, IProgressIndicator progressIndicator)
            : base(null, progressIndicator)
        {
            _mUri = uri;
        }

        public override IEnumerable<string> GetWords()
        {
            var request = (HttpWebRequest) WebRequest.Create(_mUri);
            request.Method = "GET";
            using (var response = request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream == null)
                        yield break;

                    using (var reader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        //var text = Regex.Replace(reader.ReadToEnd(), "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                        foreach (var word in GetWords(reader))
                            yield return word;
                    }
                }
            }
        }

        protected override IEnumerable<string> GetWords(string text)
        {
            text = RemoveXmlTags(text);
            text = RemoveScript(text);
            return base.GetWords(text);
        }

        private string RemoveScript(string text)
        {
            var rRemScript = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            return rRemScript.Replace(text, "");
            return Regex.Replace(text, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            if (text.Length == 0)
                return text;

            var indexOfStart = 0;
            var indexOfEnd = text.Length;
            if (!_mIsScriptMode)
            {
                indexOfStart = text.IndexOf("{");
                if (indexOfStart < 0)
                    return text;
                _mIsScriptMode = true;
            }

            if (_mIsScriptMode)
            {
                indexOfEnd = text.IndexOf("}");
                if (indexOfEnd < 0)
                    return text.Remove(indexOfStart);
                _mIsScriptMode = false;
            }

            var count = indexOfEnd - indexOfStart + 2;
            if (indexOfStart + count < text.Length)
                return text.Remove(indexOfStart, indexOfEnd - indexOfStart + 2);
            return text.Remove(indexOfStart);
        }

        private static string RemoveXmlTags(string text)
        {
            var result = text;
            var indexOfStart = result.IndexOf("<");
            while (!(indexOfStart < 0) && indexOfStart + 1 < text.Length)
            {
                var indexOfEnd = result.IndexOf(">", indexOfStart + 1);
                if (indexOfEnd < 0)
                    break;
                result = result.Remove(indexOfStart, indexOfEnd - indexOfStart + 1);
                indexOfStart = result.IndexOf("<");
            }
            return result;
        }
    }
}