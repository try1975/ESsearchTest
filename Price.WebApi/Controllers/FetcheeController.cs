using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Common.Dto.Model;
using Price.WebApi.Logic.Fetchee;
using PricePipeCore;

namespace Price.WebApi.Controllers
{
    /// <summary>
    /// Использование Fetchee API для определения цены на произвольном сайте
    /// </summary>
    [RoutePrefix("api/fetchee")]
    public class FetcheeController : ApiController
    {
        /// <summary>
        /// Получить результат определения цены, если null - ещё в процессе
        /// </summary>
        /// <param name="id">Идетификатор задачи</param>
        /// <returns></returns>
        [HttpGet]
        [Route("task/{id}", Name = nameof(GetFetcheeTask) + "Route")]
        [ResponseType(typeof(FetcheeDto))]
        public IHttpActionResult GetFetcheeTask(string id)
        {
            Logger.Log.Info($"{nameof(GetFetcheeTask)}: id={id}");
            return Ok(FetcheeTasks.Get(id));
        }

        /// <summary>
        /// Для обратного вызова Fetchee API (не использовать)
        /// </summary>
        /// <param name="fetcheeDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("callback", Name = nameof(PostFetcheeCallback) + "Route")]
        public IHttpActionResult PostFetcheeCallback(FetcheeDto fetcheeDto)
        {
            Logger.Log.Info($"{nameof(PostFetcheeCallback)}: url={fetcheeDto.Url} title={fetcheeDto.Title} price={fetcheeDto.Price}");
            FetcheeTasks.Post(fetcheeDto);
            return Ok();
        }

        /// <summary>
        /// Определить цену через Fetchee API
        /// </summary>
        /// <param name="url">Адрес страницы товара</param>
        /// <returns></returns>
        [HttpPost]
        [Route("task", Name = nameof(PostFetcheeTask) + "Route")]
        public async Task<FetcheeTaskReturnDto> PostFetcheeTask([FromUri] string url)
        {
            var callback = "https://requestb.in/1ag7w4i1";
            callback = $"{Request.RequestUri.Scheme}://{Request.RequestUri.Authority}{Url.Route(nameof(PostFetcheeCallback) + "Route", null)}";
            Logger.Log.Info($"{nameof(PostFetcheeTask)}: {nameof(url)}={url} {nameof(callback)}={callback} {nameof(FetcheeTaskDto.api_key)}={AppSettings.FetcheeApiKey}");

            var fetcheeTaskDto = new FetcheeTaskDto()
            {
                url = url,
                api_key = AppSettings.FetcheeApiKey,
                callback_url = callback
            };


            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://fetch.ee/api/v1/product");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

                using (var response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress, fetcheeTaskDto))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        Logger.Log.Error($"{nameof(PostFetcheeTask)}: {nameof(response.StatusCode)}={response.StatusCode} {response.ReasonPhrase}={response.ReasonPhrase}");
                        return null;
                    }
                    var result = await response.Content.ReadAsAsync<FetcheeTaskReturnDto>();
                    return result;
                }
            }
        }
    }
}
