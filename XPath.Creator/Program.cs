using System;
using System.Diagnostics;
using HtmlAgilityPack;
using XPath.Downloader;

namespace XPath.Creator
{
    class Program
    {
        static void Main(string[] args)
        {
            //    //h3[contains(.,'8 64')]

            var url = @"https://www.worten.pt/smartphones-e-comunicacoes/telemoveis-e-smartphones/iphone/smartphone-apple-iphone-8-64gb-cinzento-sideral-6304206";
            Console.WriteLine(url);


            var downloader = new HttpDownloader(url, null, null);

            var htmlDoc = new HtmlDocument();
            try
            {
                htmlDoc.LoadHtml(downloader.GetPage());

                var success = true;
                var dtoName = "Smartphone APPLE iPhone 8 64GB";
                //var node = htmlDoc.DocumentNode.SelectSingleNode($"//*[self::h1 or self::h2 or self::h3][contains(., '{dtoName}')]");
                var node = htmlDoc.DocumentNode.SelectSingleNode($"//*[contains(text(), '{dtoName}')]");


                if (node != null)
                {
                    dtoName = node.InnerText;
                    Console.WriteLine($"XPathName found: {dtoName}");
                }
                else
                {
                    var message = $"url: {url}  XPathName not found.";
                    Console.WriteLine(message);
                    //Logger.Log.Error(message);
                    success = false;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            Console.ReadLine();
        }
    }
}
