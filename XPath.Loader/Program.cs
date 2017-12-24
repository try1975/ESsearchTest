using System;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace XPath.Loader
{
    class Program
    {
        static void Main(string[] args)
        {
            //https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_Linq_JObject.htm
            JArray array = new JArray();
            array.Add("Manual text");
            array.Add(new DateTime(2000, 5, 23));

            JObject o = new JObject();
            o["MyArray"] = array;

            string json = o.ToString();
            // {
            //   "MyArray": [
            //     "Manual text",
            //     "2000-05-23T00:00:00"
            //   ]
            // }

            //http://html-agility-pack.net/from-web



            var html = @"http://html-agility-pack.net/";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var node = htmlDoc.DocumentNode.SelectSingleNode("//head/title");

            Console.WriteLine("Node Name: " + node.Name + "\n" + node.OuterHtml);
        }
    }
}
