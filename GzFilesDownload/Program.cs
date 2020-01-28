using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GzFilesDownload
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = File.ReadAllLines(@"D:\Gz\04_debug2.csv", Encoding.GetEncoding(866));
            var downloadRecords = new List<DownloadRecord>(lines.Length);
            foreach (var line in lines)
            {
                if (!line.StartsWith("http")) continue;
                var urlAndFile = line.Split('\t');
                if (!(urlAndFile.Length == 2)) continue;
                var url = urlAndFile[0];
                var path = urlAndFile[1];
                if (File.Exists(path)) continue;
                url = url.Replace("https://lk.zakupki.gov.ru", "http://zakupki.gov.ru");
                downloadRecords.Add(new DownloadRecord { Url = url, Path = path });
            }
            foreach (var downloadRecord in downloadRecords)
            {
                await GetAttachment(downloadRecord.Url, downloadRecord.Path);
            }
            //Parallel.ForEach(downloadRecords, new ParallelOptions { MaxDegreeOfParallelism = 10 }, await GetAttachment);

            Console.WriteLine("Done. Press any key.");
            Console.ReadKey();
        }

        public async static Task GetAttachment(string url, string path)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    HttpContent content = response.Content;
                    var contentStream = await content.ReadAsStreamAsync(); // get the actual content stream
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
                    contentStream.CopyTo(fileStream);
                    fileStream.Dispose();
                    Console.WriteLine($"{path}");
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
        }

    }

    internal class DownloadRecord
    {
        internal string Url { get; set; }
        internal string Path { get; set; }
    }
}
