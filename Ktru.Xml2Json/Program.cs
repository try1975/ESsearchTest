using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Ktru.Xml2Json
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(
                File.ReadAllText(@"C:\Projects\GitHub\ESsearchTest\Ktru.Xml2Json\nsiKTRU_all_20181222010001_001.xml"));

            XmlElement xRoot = doc.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("oos", @"http://zakupki.gov.ru/oos/types/1");

            // выбор всех дочерних узлов
            XmlNodeList childnodes = xRoot.SelectNodes("//oos:position", nsmgr);
            foreach (XmlNode n in childnodes)
            {
                //Console.WriteLine(n.OuterXml);
                Console.WriteLine(JsonConvert.SerializeXmlNode(n, Formatting.Indented));
            }

            //string json = JsonConvert.SerializeXmlNode(doc);
        }
    }
}
