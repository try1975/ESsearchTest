using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using Price.WebApi.Logic.Xpath;
using PricePipeCore;
using Common.Dto.Logic;
using Common.Dto.Model.XPath;
using Nest;

namespace Price.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    //[RoutePrefix("api/xpath")]
    public class XpathController : ApiController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/xpath", Name = nameof(XpathGet) + "Route")]
        public XPathDto XpathGet(string link)
        {
            var dto = XPathStore.Get(link);
            if (dto != null) return dto;
            var elasticClient = ElasticClientFactory.GetElasticClient("md_xpath");
            var response = elasticClient.Search<XPathDto>(z => z
                .Type(nameof(PriceCommon.Model.Content).ToLower())
                .Size(1)
                .Query(q => q.Match(m => m.Field(f => f.Uri).Query(link)))
            );
            dto = response.Hits.Select(s => s.Source).FirstOrDefault();
            if (dto != null) return dto;
            response = elasticClient.Search<XPathDto>(z => z
                    .Type(nameof(PriceCommon.Model.Content).ToLower())
                    .Size(1)
                    .Query(q => q.Term(t => t.Field(f => f.Domain).Value(new Uri(link).Host)))
                );
            dto = response.Hits.Select(s => s.Source).FirstOrDefault();
            return dto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        [HttpPost]
        [Route("api/xpath", Name = nameof(XpathPost) + "Route")]
        public HttpStatusCode XpathPost(XPathDto dto)
        {
            try
            {
                dto.Normalize();
                dto.CollectedAt = Utils.GetUtcNow();
                XPathStore.Post(dto);

                var elasticClient = ElasticClientFactory.GetElasticClient("md_xpath");
                var response = elasticClient.Index(dto, z => z
                    .Type(nameof(PriceCommon.Model.Content).ToLower())
                    .Id(dto.Id)
                );
                return response.Result == Result.Created || response.Result == Result.Updated
                    ? HttpStatusCode.Created
                    : HttpStatusCode.BadRequest;
            }
            catch (Exception exception)
            {
                Logger.Log.Error($"{nameof(XpathPost)} {dto} {exception}");
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
