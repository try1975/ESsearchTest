using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common.Dto.Model.XPath;
using HtmlAgilityPack;
using Nest;

namespace XPath.Loader
{
    internal static class Program
    {
        private static void Main()
        {
            #region elastic
            // использовать api

            //POST md_xpath/ content / _search
            //{
            //    "from": 0,
            //    "size": 10,
            //    "query": {
            //        "range": {
            //            "collectedAt": {
            //                "lt": "now-20d/d"
            //            }
            //        }
            //    }
            //}

            //"hits": [
            //{
            //    "_index": "md_xpath_v01",
            //    "_type": "content",
            //    "_id": "ee7ba7b3bab38583091b924538cf9c7f",
            //    "_score": 1,
            //    "_source": {
            //        "xPathName": "//h1",
            //        "xPathPrice": "//p[@class='product__price    ']",
            //        "uri": "https://leroymerlin.ru/product/emal-pf-115-vybor-mastera-cvet-goluboy-0-9-kg-14713474/",
            //        "name": "Эмаль ПФ-115 Выбор Мастера цвет голубой 0.9 кг",
            //        "price": "127,00",
            //        "collectedAt": 1513770839,
            //        "id": "ee7ba7b3bab38583091b924538cf9c7f"
            //    }
            //},
            //{
            //    "_index": "md_xpath_v01",
            //    "_type": "content",
            //    "_id": "825a2e93bd041bc3e7531fce0bb6b8ff",
            //    "_score": 1,
            //    "_source": {
            //        "xPathName": "//h1",
            //        "xPathPrice": "/html/body/div[2]/footer/p",
            //        "uri": "https://144.76.54.166:44300/",
            //        "name": "ASP.NET",
            //        "price": "2017,",
            //        "collectedAt": 1513163982,
            //        "id": "825a2e93bd041bc3e7531fce0bb6b8ff"
            //    }
            //},

            var connectionSettings = new ConnectionSettings(new Uri(@"http://d69e484ce622e0f735b3af79e820d1ae.us-east-1.aws.found.io:9200/"))
                .OnRequestCompleted(details =>
                {
                    var s = details.RequestBodyInBytes != null
                        ? Encoding.UTF8.GetString(details.RequestBodyInBytes)
                        : null;
                    Debug.WriteLine($"{s}");
                })
                .DisableDirectStreaming()
                .DefaultIndex("md_xpath")
                .BasicAuthentication("md5_reader", "Fynbkjgf777");
            var elasticClient = new ElasticClient(connectionSettings);

            var lowerBound = DateTime.Today.AddDays(-20);
            var lastUpdatedResponse = elasticClient.Search<XPathDto>(s => s
            .Type("content")
            .Query(q => q.DateRange(r => r
                            .Field(p => p.CollectedAt)
                            .LessThan(lowerBound)
                            )
                   //&& q.QueryString(qs=>qs.DefaultField(nameof(XPathDto.name)).Query("мука пшеничная").DefaultOperator(Operator.And))
                   )
                .Size(100)
                .Sort(sort => sort
                    .Descending(f => f.CollectedAt))
            );
            var lastUpdated = lastUpdatedResponse.Hits.Select(s => s.Source);

            #endregion

            #region json

            //https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_Linq_JObject.htm
            //JArray array = new JArray();
            //array.Add("Manual text");
            //array.Add(new DateTime(2000, 5, 23));

            // var o = new JObject();
            ////o.SelectToken()
            ////foreach (JProperty property in o.Properties()){ Console.WriteLine(property.Name + " - " + property.Value);}
            //o["MyArray"] = array;

            //string json = o.ToString();
            // {
            //   "MyArray": [
            //     "Manual text",
            //     "2000-05-23T00:00:00"
            //   ]
            // }

            #endregion

            //http://html-agility-pack.net/from-web
            foreach (var dto in lastUpdated)
            {
                //var url = @"https://leroymerlin.ru/product/emal-pf-115-vybor-mastera-cvet-goluboy-0-9-kg-14713474/";
                var url = dto.Uri;
                Console.WriteLine(url);
                var web = new HtmlWeb();

                HtmlDocument htmlDoc;
                try
                {
                    htmlDoc = web.Load(url);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                    continue;
                }

                var classNodes = htmlDoc.DocumentNode.SelectNodes("//*[@class]");
                foreach (var classNode in classNodes)
                {
                    var classValue = classNode.GetAttributeValue("class", null);
                    if (!classValue.Equals(classValue.Trim()))
                    {
                        classNode.SetAttributeValue("class", classValue.Trim());
                    }
                }

                //var location = Assembly.GetEntryAssembly().GetName().CodeBase;
                //var directoryName = Path.GetDirectoryName(location);
                //if (directoryName != null)
                //{
                //    var filePath = Path.Combine(directoryName, $"{url.GetHashCode()}.html");
                //    File.WriteAllText(new Uri(filePath).LocalPath, htmlDoc.DocumentNode.WriteContentTo());
                //    //File.WriteAllText(new Uri(filePath).LocalPath, cleanHtml, Encoding.UTF8);
                //}

                var node = htmlDoc.DocumentNode.SelectSingleNode(dto.XPathName);
                if (node != null)
                {
                    //Console.WriteLine("Node Name: " + node.Name + "\n" + node.OuterHtml);
                    Console.WriteLine(node.InnerText);
                }
                node = htmlDoc.DocumentNode.SelectSingleNode(dto.XPathPrice);
                if (node != null)
                {
                    var tempo = node.InnerText;
                    Console.WriteLine(tempo);

                    var digits = new Regex(@"^\D*?((-?(\d+(\.,\d+)?))|(-?\.,\d+)).*");
                    var mx = digits.Match(tempo);
                    //Console.WriteLine("Input {0} - Digits {1} {2}", str, mx.Success, mx.Groups);

                    Console.WriteLine(mx.Success ? Convert.ToDecimal(mx.Groups[1].Value.Replace(',', '.')) : 0);


                    //const RegexOptions options = RegexOptions.None;
                    //var regex = new Regex("[ ]{2,}", options);
                    //tempo = regex.Replace(tempo, " ");
                    //tempo = tempo.Replace("\r\n", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty);
                    //Console.WriteLine(tempo);
                    //Console.WriteLine(double.Parse(tempo));
                }
            }
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
    public static class DecimalConverter
    {
        public static decimal ExtractDecimalFromString(string str)
        {
            var sb = new StringBuilder();
            foreach (var c in str.Where(c => c == '.' || c == ',' || char.IsDigit(c)))
            {
                sb.Append(c);
            }
            return Convert.ToDecimal(sb.ToString().Replace(',', '.'));
        }
    }
}
