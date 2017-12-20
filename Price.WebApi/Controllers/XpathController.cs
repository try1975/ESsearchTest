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

        [HttpGet]
        [Route("", Name = nameof(XpathGet) + "Route")]
        public XPathDto XpathGet(string link)
        {
            //TODO:брать из эластика напрямую md_xpath



            //var elasticClient = ElasticClientFactory.GetElasticClient("md_xpath");
            //var response = elasticClient.Search(dto, z => z
            //    .Type(nameof(PriceCommon.Model.Content).ToLower())
            //);

            Debug.WriteLine(link);
            var dto = XPathStore.Get(link);
            return dto;
        }

        //[HttpGet]
        //[Route("domain", Name = nameof(XpathGetDomain) + "Route")]
        //public XPathDto XpathGetDomain(string link)
        //{
        //    //TODO:брать из эластика напрямую md_xpath

        //    Debug.WriteLine(link);
        //    var dto = XPathStore.Get(link);
        //    return dto;
        //}

        [HttpPost]
        [Route("", Name = nameof(XpathPost) + "Route")]
        public void XpathPost(XPathDto dto)
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
