using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using log4net;

namespace Topol.UseApi.Controls
{
    public static class GzDocUtils
    {
        public const string DocName = nameof(DocName);
        public const string DocUrl = nameof(DocUrl);
        public const string DocRegion = nameof(DocRegion);
        public const string DocMonth = nameof(DocMonth);
        public const string DocReestrNumber = nameof(DocReestrNumber);
        public const string DocExt = nameof(DocExt);
        public const string ReestrNumber = "reestrNumber";

        private const string GzWebsiteCardDocs = @"http://zakupki.gov.ru/epz/contract/contractCard/document-info.html?reestrNumber={0}";

        public static void GetUrlGzWebsiteCardDocs(string reestrNumber)
        {
            if (string.IsNullOrWhiteSpace(reestrNumber)) return;
            Process.Start(string.Format(GzWebsiteCardDocs, reestrNumber));
        }

        public static void GetFilenameData(string filename, out string ext)
        {
            ext = null;
            if (string.IsNullOrWhiteSpace(filename)) return;
            ext = Path.GetExtension(filename);
            if (!string.IsNullOrWhiteSpace(ext)) ext = ext.ToLower();
        }

        public static void GetUrlData(string url, out string docRegion, out string docMonth, out string docReestrNumber, ILog log)
        {
            docRegion = null;
            docMonth = null;
            docReestrNumber = null;
            if (string.IsNullOrWhiteSpace(url)) return;

            var uri = new Uri(url);
            var arguments = uri.Query
                .Substring(1) // Remove '?'
                .Split('&')
                .Select(q => q.Split('='))
                .ToDictionary(q => q.FirstOrDefault(), q => q.Skip(1).FirstOrDefault());
            var docPath = arguments["docPath"];
            if (string.IsNullOrWhiteSpace(docPath)) return;
            try
            {
                var match = Regex.Match(docPath, "([0-9]*)_([0-9]*)%2f([0-9]*)%2f", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                docRegion = match.Groups[1].Value;
                docMonth = match.Groups[2].Value;
                docReestrNumber = match.Groups[3].Value;
            }
            catch (Exception exception)
            {
                log?.Error(exception);
            }
        }
    }
}