using System.Web.Http;
using Price.WebApi.Models;
using PricePipeCore;

namespace Price.WebApi.Controllers
{
    /// <summary>
    /// Использование fetchee API для определения цены
    /// </summary>
    [RoutePrefix("api/fetchee")]
    public class FetcheeController : ApiController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("task", Name = nameof(PostFetcheeTask) + "Route")]
        public FetcheeTaskReturnDto PostFetcheeTask([FromBody]string url)
        {
            Logger.Log.Info($"{nameof(PostFetcheeTask)}: url={url}");
            //if (string.IsNullOrEmpty(source)) source = AppSettings.DefaultIndex;
            //return Mapper.Map<IEnumerable<ContentDto>>(new SimpleSearcher(source).SimpleSearch(text));
            var fetcheeTaskDto = new FetsheeTaskDto()
            {
                url = url,
                api_key = AppSettings.FetcheeApiKey,
                callback_url = "https://requestb.in/14j4ty31"
            };

            return new FetcheeTaskReturnDto();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fetcheeDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("result", Name = nameof(PostFetcheeResult) + "Route")]
        public FetcheeDto PostFetcheeResult(FetcheeDto fetcheeDto)
        {
            Logger.Log.Info($"{nameof(PostFetcheeResult)}: url={fetcheeDto.url} title={fetcheeDto.title} price={fetcheeDto.price}");
            return fetcheeDto;
        }
    }
}
