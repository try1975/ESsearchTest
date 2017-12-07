using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using Price.WebApi.Logic;
using Price.WebApi.Logic.Xpath;
using Price.WebApi.Models.Xpath;
using PricePipeCore;

namespace Price.WebApi.Controllers
{
    [RoutePrefix("api/xpath")]
    public class XpathController : ApiController
    {
        public XPathDto Get(string link)
        {
            Debug.WriteLine(link);
            var dto = XPathStore.Get(link);
            return dto;
        }

        public void Post(XPathDto dto)
        {
            if (Request.Headers.Contains("Origin"))
            {
                var values = Request.Headers.GetValues("Origin");
                if (values != null)
                {
                    Debug.WriteLine(values.FirstOrDefault());
                }
                // Do stuff with the values... probably .FirstOrDefault()
            }
            Debug.WriteLine($"{nameof(dto.XPathName)}={dto.XPathName}");
            Debug.WriteLine($"{nameof(dto.XPathPrice)}={dto.XPathPrice}");
            Debug.WriteLine($"{nameof(dto.Uri)}={dto.Uri}");

            XPathStore.Post(dto);
            //TODO:записать в базу эластика напрямую md_xpath
            // обновление по триггеру и с использованием HTML Agility Pack
            dto.CollectedAt= (long?)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var elangPath = PathService.GetXpathPath();
            var json = JsonConvert.SerializeObject(XPathStore.Dictionary);
            File.WriteAllText(elangPath, json);
            var elasticClient = ElasticClientFactory.GetElasticClient("md_xpath");
            var response = elasticClient.Index(dto, z => z
                .Type(nameof(PriceCommon.Model.Content).ToLower())
                .Id(dto.Id)
            );
            Debug.WriteLine($"{response}");
        }
    }
}
