using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Http;
using System.Web.Http.Description;
using Price.WebApi.Logic;
using Price.WebApi.Maintenance.Interfaces;
using PriceCommon.Model;
using PricePipeCore;

namespace Price.WebApi.Controllers
{
    /// <summary>
    /// Функции общего назначения
    /// </summary>
    [RoutePrefix("api/common")]
    //[Authorize]
    public class CommonController : ApiController
    {
        private readonly IFindCompanyApi _findCompanyApi;
        //private IUpdatePriceWatcher _updatePriceWatcher;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public CommonController(IFindCompanyApi findCompanyApi)
        {
            _findCompanyApi = findCompanyApi;
            //_updatePriceWatcher = updatePriceWatcher;
        }

        /// <summary>
        /// Обратный поиск по ОКПД2
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Наименование ТРУ для определения ОКПД2</returns>
        [HttpGet]
        [Route("Okpd2Reverse", Name = nameof(GetOkpd2Reverse) + "Route")]
        public IEnumerable<Okpd2Reverse> GetOkpd2Reverse(string text)
        {
            //поиск ОКПД2 пока не появится результат путем удаления слов текста с конца
            text = text.Replace("/", "//");
            var words = text.ToLower().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var list = new Okpd2ReverseSeacher().Search(text);
            if (list == null) return new List<Okpd2Reverse>();
            if (list.Any() && words.Length == 1) return list;
            var i = 1;
            var notEnd = true;
            do
            {
                var newWords = words.Take(words.Length - i);
                var enumerable = newWords as string[] ?? newWords.ToArray();
                list = new Okpd2ReverseSeacher().Search(string.Join(" ", enumerable));
                if (list.Any() || enumerable.Length == 1)
                {
                    notEnd = false;
                }
                i = i + 1;
            } while (notEnd);
            return list;
        }

        /// <summary>
        /// Количество записей в разных источниках данных
        /// </summary>Gz
        /// <returns>
        /// Md5 - ценовые роботы
        /// Gz - госзакупкм
        /// Company - фирмы
        /// </returns>
        [HttpGet]
        [ResponseType(typeof(Dictionary<string, string>))]
        [Route("counts", Name = nameof(GetCounts) + "Route")]
        public IDictionary<string, string> GetCounts()
        {
            const string companyKey = "Company";
            const string countSufix = "Count";
            string companyCount;
            if (!MemoryCache.Default.Contains($"{companyKey}{countSufix}"))
            {
                companyCount = _findCompanyApi.GetSellerCount().ToString();
                MemoryCache.Default.Add($"{companyKey}{countSufix}", companyCount, DateTimeOffset.UtcNow.AddSeconds(AppGlobal.CashSeconds));
            }
            else
            {
                companyCount = MemoryCache.Default.Get($"{companyKey}{countSufix}")?.ToString();
            }
            string mdCount;
            if (!MemoryCache.Default.Contains($"{nameof(ElacticIndexName.Md5)}{countSufix}"))
            {
                var elasticClient = ElasticClientFactory.GetElasticClient(source: nameof(ElacticIndexName.Md5));
                mdCount = elasticClient.Count<Content>().Count.ToString();
                MemoryCache.Default.Add($"{nameof(ElacticIndexName.Md5)}{countSufix}", mdCount, DateTimeOffset.UtcNow.AddSeconds(AppGlobal.CashSeconds));
            }
            else
            {
                mdCount = MemoryCache.Default.Get($"{nameof(ElacticIndexName.Md5)}{countSufix}")?.ToString();
            }
            string gzCount;
            if (!MemoryCache.Default.Contains($"{nameof(ElacticIndexName.Gz)}{countSufix}"))
            {
                var elasticClient = ElasticClientFactory.GetElasticClient(source: nameof(ElacticIndexName.Gz));
                gzCount = elasticClient.Count<Content>().Count.ToString();
                MemoryCache.Default.Add($"{nameof(ElacticIndexName.Gz)}{countSufix}", gzCount, DateTimeOffset.UtcNow.AddSeconds(AppGlobal.CashSeconds));
            }
            else
            {
                gzCount = MemoryCache.Default.Get($"{nameof(ElacticIndexName.Gz)}{countSufix}")?.ToString();
            }
            return new Dictionary<string, string>
            {
                {nameof(ElacticIndexName.Md5), mdCount},
                {nameof(ElacticIndexName.Gz), gzCount },
                {companyKey, companyCount}
            };
        }

        /// <summary>
        /// Настройки для парсера
        /// </summary>
        /// <param name="host">Хост</param>
        /// <returns>Объект настроек для парсинга</returns>
        //[HttpGet]
        //[Route("sourceByHost", Name = nameof(GetSourceByHost) + "Route")]
        //public SourceDto GetSourceByHost(string host)
        //{
        //    return SourceNames.GetSourceDtoByHost(host);
        //}


        /// <summary>
        /// Задача обновления цен
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <returns>Объект задачи обновления цен</returns>
        //[HttpGet]
        //[Route("updatePriceTask/{id}", Name = nameof(GetUpdatePriceTask) + "Route")]
        //[ResponseType(typeof(UpdatePriceTaskDto))]
        //public IHttpActionResult GetUpdatePriceTask(string id)
        //{
        //    return Ok(UpdatePriceTaskStore.Get(id)); //StatusCode(HttpStatusCode.Accepted);
        //}

        /// <summary>
        /// Обновление цен по списку ТРУ
        /// </summary>
        /// <param name="uriList">Список URL ТРУ</param>
        /// /// <param name="createScreenshots">true - делать скриншот</param>
        /// <returns>Задача обновления цен</returns>
        //[HttpPost]
        //[Route("updatePrices", Name = nameof(PostUpdatePrices) + "Route")]
        //[ResponseType(typeof(UpdatePriceTaskDto))]
        //public HttpResponseMessage PostUpdatePrices(List<Uri> uriList, [FromUri] bool createScreenshots = false)
        //{
        //    #region check input parameter

        //    if (uriList == null)
        //        return Request.CreateResponse(HttpStatusCode.BadRequest,
        //            new ErrorDto { Message = "not found urls in parameters" });
        //    if (!uriList.Any())
        //        return Request.CreateResponse(HttpStatusCode.BadRequest,
        //            new ErrorDto { Message = "not found urls in parameters" });

        //    #endregion

        //    var processedAt = Utils.GetUtcNow();
        //    var hosts = uriList.Select(h => h.Host).Distinct().ToList();
        //    var baseUri = $"{Request.RequestUri.Scheme}://{Request.RequestUri.Host}:{Request.RequestUri.Port}";
        //    var taskId = IdService.GenerateId();
        //    var taskDto = new UpdatePriceTaskDto(uriList.Count) { Id = taskId, CreateScreenshots = createScreenshots, BaseUri = baseUri };
        //    UpdatePriceTaskStore.Post(taskDto);

        //    foreach (var host in hosts)
        //    {
        //        var elangPath = PathService.GetElangPath(host);
        //        var sourceName = SourceNames.GetSourceName(host);

        //        #region not found elang

        //        if (string.IsNullOrEmpty(elangPath) || string.IsNullOrEmpty(sourceName))
        //        {
        //            var uriListErrors = uriList.Where(h => h.Host == host).Distinct();
        //            foreach (var uriError in uriListErrors)
        //            {
        //                var updatePriceDto = new UpdatePriceDto
        //                {
        //                    Uri = uriError,
        //                    ProcessedAt = processedAt,
        //                    Status = UpdatePriceStatus.ElangError
        //                };
        //                taskDto.UpdatePrices.Add(updatePriceDto);
        //                UpdatePriceStore.Post(updatePriceDto);
        //            }
        //            taskDto.UpdateStatistics();
        //            continue;
        //        }

        //        #endregion

        //        var uriListByHost = uriList.Where(h => h.Host == host).Distinct();
        //        foreach (var uri in uriListByHost)
        //        {
        //            var updatePriceDto = UpdatePriceStore.Get(uri);
        //            // if not found elang earlier
        //            if (updatePriceDto.Status == UpdatePriceStatus.ElangError)
        //            {
        //                updatePriceDto.ProcessedAt = null;
        //            }
        //            // if one day age
        //            if (updatePriceDto.ProcessedAt != null && processedAt - updatePriceDto.ProcessedAt > 86400)
        //            {
        //                updatePriceDto.ProcessedAt = null;
        //            }
        //            taskDto.UpdatePrices.Add(updatePriceDto);
        //        }
        //        taskDto.UpdateStatistics();

        //        var notProcessedByHost =
        //            taskDto.UpdatePrices
        //                .Where(h => h.Uri.Host == host && h.ProcessedAt == null)
        //                .Select(h => h.Uri.AbsoluteUri)
        //                .ToList();
        //        if (!notProcessedByHost.Any()) continue;
        //        ProductParser.PrepareAndRun(host, taskId, notProcessedByHost, elangPath, sourceName);
        //    }

        //    //return Ok(dto);
        //    return Request.CreateResponse(HttpStatusCode.OK, taskDto);
        //    //return StatusCode(HttpStatusCode.Accepted);
        //}
    }
}