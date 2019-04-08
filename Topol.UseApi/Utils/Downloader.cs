using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Topol.UseApi.Utils
{
    public static class Downloader
    {
        private static void DownloadFile(string filename, string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.109 Safari/537.36";

            var path = Path.GetDirectoryName(filename);
            Directory.CreateDirectory(path ?? throw new InvalidOperationException());
            using (var response = (HttpWebResponse)request.GetResponseAsync().Result)
            {
                var responseStream = response.GetResponseStream();
                using (var fileStream = File.Create(filename))
                {
                    responseStream?.CopyTo(fileStream);
                }
            }
        }

        internal static string GetFile(string filename, string url)
        {
            var uri = new Uri(url);
            var arguments = uri.Query
                .Substring(1) // Remove '?'
                .Split('&')
                .Select(q => q.Split('='))
                .ToDictionary(q => q.FirstOrDefault(), q => q.Skip(1).FirstOrDefault());
            var uid = arguments["uid"];

            filename = Path.Combine(KnownFolders.GetPath(KnownFolder.Downloads), uid, filename);

            if (!File.Exists(filename)) DownloadFile(filename, url);
            return !File.Exists(filename) ? string.Empty : filename;
        }

        internal static string GetFile2(string filename, string url)
        {
            var uri = new Uri(url);
            var arguments = uri.Query
                .Substring(1) // Remove '?'
                .Split('&')
                .Select(q => q.Split('='))
                .ToDictionary(q => q.FirstOrDefault(), q => q.Skip(1).FirstOrDefault());
            var docPath = arguments["docPath"];
            docPath = HttpUtility.UrlDecode(docPath, System.Text.Encoding.UTF8);
            var uid = Path.GetDirectoryName(docPath);
            filename = Path.Combine(KnownFolders.GetPath(KnownFolder.Downloads), uid, filename);

            if (!File.Exists(filename)) DownloadFile(filename, url);
            return !File.Exists(filename) ? string.Empty : filename;
        }
    }
}
