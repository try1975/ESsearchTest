using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;
using Common.Dto.Model;
using PricePipeCore;

namespace Price.WebApi.Controllers
{
    [RoutePrefix("api/internet")]
    public class InternetController : ApiController
    {
        public IHttpActionResult Post(IEnumerable<BasicContentDto> dtos)
        {
            var success = true;
            var elasticClient = ElasticClientFactory.GetElasticClient("md_internet");
            foreach (var dto in dtos)
            {
                dto.CollectedAt = (long?)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                var response = elasticClient.Index(dto, z => z
                    .Type(nameof(PriceCommon.Model.Content).ToLower())
                    .Id(dto.Id)
                );
                Debug.WriteLine($"{response}");
                success = success && response.ApiCall.Success;
            }
            //elasticClient.LowLevel.Bulk<ContentDto>(nameof(PriceCommon.Model.Content).ToLower(), dtos);
            return success ? (IHttpActionResult) Ok() : NotFound();
        }
    }
}
