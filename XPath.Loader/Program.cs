using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common.Dto.Logic;
using Common.Dto.Model.XPath;
using HtmlAgilityPack;
using PricePipeCore;

namespace XPath.Loader
{
    internal static class Program
    {
        private static void Main()
        {
            //Logger.InitLogger();
            var elasticClient = ElasticClientFactory.GetElasticClient(AppSettings.DefaultIndex);
            int updatePacketSize;
            int.TryParse(ConfigurationManager.AppSettings[nameof(updatePacketSize)], out updatePacketSize);
            if (updatePacketSize == 0) updatePacketSize = 100;
            int daysBeforeUpdate;
            int.TryParse(ConfigurationManager.AppSettings[nameof(daysBeforeUpdate)], out daysBeforeUpdate);
            if (daysBeforeUpdate == 0) daysBeforeUpdate = 3;
            var lowerBound = (long)DateTime.UtcNow.AddDays(-1 * daysBeforeUpdate).Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var isContinue = true;

            while (isContinue)
            {
                #region elastic

                var searchResponse = elasticClient.Search<XPathDto>(s => s
                    .Type("content")
                    .Query(q =>
                            q.Range(r => r
                                        .Field(p => p.CollectedAt)
                                        .LessThan(lowerBound)
                                        )
                    //&& q.QueryString(qs=>qs.DefaultField(nameof(XPathDto.Name)).Query("приправа").DefaultOperator(Operator.And))
                    //q.MatchAll()
                    )
                    .Size(updatePacketSize)
                    .Sort(sort => sort
                        .Ascending(f => f.CollectedAt))
                );
                var xPathDtos = searchResponse.Hits.Select(s => s.Source);
                isContinue = searchResponse.Hits.Any();
                //searchResponse = elasticClient.Search<XPathDto>(s => s
                //    .Type("content")
                //    .Query(q => q
                //        .Term(t => t
                //            .Field(nameof(XPathDto.Id).ToLower())
                //            .Value("30ab8e11a5e0cd3449d1535783abd6b1")))
                //);
                //xPathDtos = searchResponse.Hits.Select(s => s.Source);
                //isContinue = false;



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

                foreach (var dto in xPathDtos)
                {
                    #region HAP

                    var url = dto.Uri;
                    Console.WriteLine(url);

                    var downloader = new HttpDownloader(url, null, null);

                    //var web = new HtmlWeb();

                    //HtmlDocument htmlDoc;
                    var htmlDoc = new HtmlDocument();
                    try
                    {
                        //htmlDoc = web.Load(url);
                        htmlDoc.LoadHtml(downloader.GetPage());
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine(exception);
                        //Logger.Log.Error($"{exception}");
                        continue;
                    }

                    // trim spaces in class name
                    if (dto.XPathName.Contains("class") || dto.XPathPrice.Contains("class"))
                    {
                        var classNodes = htmlDoc.DocumentNode.SelectNodes("//*[@class]");
                        foreach (var classNode in classNodes)
                        {
                            var classValue = classNode.GetAttributeValue("class", null);
                            if (!classValue.Equals(classValue.Trim()))
                            {
                                classNode.SetAttributeValue("class", classValue.Trim());
                            }
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

                    var success = true;
                    var node = htmlDoc.DocumentNode.SelectSingleNode(dto.XPathName);
                    var dtoName = dto.Name;
                    if (node != null)
                    {
                        dtoName = node.InnerText;
                        Console.WriteLine($"XPathName found: {dtoName}");
                    }
                    else
                    {
                        var message = $"id: {dto.Id} url: {url}  XPathName not found: {dto.XPathName}";
                        Console.WriteLine(message);
                        //Logger.Log.Error(message);
                        success = false;
                    }
                    var dtoPrice = dto.Price;
                    node = htmlDoc.DocumentNode.SelectSingleNode(dto.XPathPrice);
                    if (node != null)
                    {
                        //TODO: рассмотреть варианты когда инфа не в тексте
                        var tempo = node.InnerText;
                        var digits = new Regex(@"^\D*?((-?(\d+(\.,\d+)?))|(-?\.,\d+)).*");
                        var mx = digits.Match(tempo);
                        //Console.WriteLine("Input {0} - Digits {1} {2}", str, mx.Success, mx.Groups);

                        var price = mx.Success ? Convert.ToDecimal(mx.Groups[1].Value.Replace(',', '.')) : 0;
                        dtoPrice = $"{price}";

                        Console.WriteLine($"XPathPrice found: {dtoPrice}");


                        //const RegexOptions options = RegexOptions.None;
                        //var regex = new Regex("[ ]{2,}", options);
                        //tempo = regex.Replace(tempo, " ");
                        //tempo = tempo.Replace("\r\n", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty);
                        //Console.WriteLine(tempo);
                        //Console.WriteLine(double.Parse(tempo));
                    }
                    else
                    {
                        var message = $"id: {dto.Id} url: {url}  XPathPrice not found: {dto.XPathPrice}";
                        Console.WriteLine(message);
                        //Logger.Log.Error(message);
                        success = false;
                    }

                    #endregion

                    if (success)
                    {
                        dto.Name = dtoName;
                        dto.Price = dtoPrice;
                        dto.Normalize();
                    }
                    dto.CollectedAt = Utils.GetUtcNow();

                    //write in elastic
                    var response = elasticClient.Index(dto, z => z.Type("content").Id(dto.Id));
                    Console.WriteLine($"response.Result = {response.Result}");
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
