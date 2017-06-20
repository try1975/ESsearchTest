using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Price.WebApi.Logic;
using Price.WebApi.Logic.Interfaces;
using Price.WebApi.Models;

namespace Price.WebApi.Controllers
{
    /// <summary>
    /// Функции общего назначения
    /// </summary>
    [RoutePrefix("api/common")]
    //[Authorize]
    public class CommonController : ApiController
    {
        private IUpdatePriceWatcher _updatePriceWatcher;

        public CommonController(IUpdatePriceWatcher updatePriceWatcher)
        {
            _updatePriceWatcher = updatePriceWatcher;
        }

        /// <summary>
        /// Настройки для парсера
        /// </summary>
        /// <param name="host">Хост</param>
        /// <returns>Объект настроек для парсинга</returns>
        [HttpGet]
        [Route("sourceByHost", Name = nameof(GetSourceByHost) + "Route")]
        public SourceDto GetSourceByHost(string host)
        {
            return SourceNames.GetSourceDtoByHost(host);
        }


        /// <summary>
        /// Задача обновления цен
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <returns>Объект задачи обновления цен</returns>
        [HttpGet]
        [Route("updatePriceTask/{id}", Name = nameof(GetUpdatePriceTask) + "Route")]
        [ResponseType(typeof(TaskDto))]
        public IHttpActionResult GetUpdatePriceTask(string id)
        {
            return Ok(UpdatePriceTasks.Get(id)); //StatusCode(HttpStatusCode.Accepted);
        }

        ///// <summary>
        ///// Обновление цен по одному ТРУ
        ///// </summary>
        ///// <param name="uri">URL ТРУ</param>
        ///// <returns>Если обновление успешно - код 200</returns>
        //[HttpGet]
        //[Route("updatePrice", Name = nameof(GetUpdatePrice) + "Route")]
        //public IHttpActionResult GetUpdatePrice(Uri uri)
        //{
        //    var host = uri.Host;
        //    var elangPath = PathService.GetElangPath(host);
        //    var sourceName = SourceNames.GetSourceName(host);
        //    if (string.IsNullOrEmpty(elangPath) || string.IsNullOrEmpty(sourceName))
        //        return BadRequest("not found in source database");
        //    var taskId = IdService.GenerateId();

        //    //var partialFileName = PathService.GetFileNameFromHostName(host);
        //    //var csvPath = PathService.GetCsvPath(partialFileName, taskId);
        //    //var urlPath = PathService.GetUrlPath(partialFileName, taskId);
        //    //File.WriteAllText(urlPath, $"{uri}");
        //    //ProductParser.Run(elangPath, sourceName, urlPath, "", csvPath);
        //    ProductParser.PrepareAndRun(host, taskId, new[] {$"{uri}"}, elangPath, sourceName);
        //    //var screenshotFilename = Path.GetFullPath("screenshot_" + DateTime.Now.Ticks + ".png");

        //    return Ok(); //StatusCode(HttpStatusCode.Accepted);
        //}

        /// <summary>
        /// Обновление цен по списку ТРУ
        /// </summary>
        /// <param name="uriList">Список URL ТРУ</param>
        /// /// <param name="createScreenshots">true - делать скриншот</param>
        /// <returns>Задача обновления цен</returns>
        [HttpPost]
        [Route("updatePrices", Name = nameof(PostUpdatePrices) + "Route")]
        [ResponseType(typeof(TaskDto))]
        public HttpResponseMessage PostUpdatePrices(List<Uri> uriList, [FromUri] bool createScreenshots = false)
        {
            #region check input parameter

            if (uriList == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ErrorDto { Message = "not found urls in parameters" });
            if (!uriList.Any())
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ErrorDto { Message = "not found urls in parameters" });

            #endregion

            var processedAt = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var hosts = uriList.Select(h => h.Host).Distinct().ToList();
            var baseUri = $"{Request.RequestUri.Scheme}://{Request.RequestUri.Host}:{Request.RequestUri.Port}";
            var taskId = IdService.GenerateId();
            var taskDto = new TaskDto(uriList.Count) { Id = taskId, CreateScreenshots = createScreenshots, BaseUri = baseUri };
            UpdatePriceTasks.Post(taskDto);

            foreach (var host in hosts)
            {
                var elangPath = PathService.GetElangPath(host);
                var sourceName = SourceNames.GetSourceName(host);

                #region not found elang

                if (string.IsNullOrEmpty(elangPath) || string.IsNullOrEmpty(sourceName))
                {
                    var uriListErrors = uriList.Where(h => h.Host == host).Distinct();
                    foreach (var uriError in uriListErrors)
                    {
                        var updatePriceDto = new UpdatePriceDto
                        {
                            Uri = uriError,
                            ProcessedAt = processedAt,
                            Status = UpdatePriceStatus.ElangError
                        };
                        taskDto.UpdatePrices.Add(updatePriceDto);
                        UpdatePrices.Post(updatePriceDto);
                    }
                    taskDto.UpdateStatistics();
                    continue;
                }

                #endregion

                var uriListByHost = uriList.Where(h => h.Host == host).Distinct();
                foreach (var uri in uriListByHost)
                {
                    var updatePriceDto = UpdatePrices.Get(uri);
                    // if not found elang earlier
                    if (updatePriceDto.Status == UpdatePriceStatus.ElangError)
                    {
                        updatePriceDto.ProcessedAt = null;
                    }
                    // if one day age
                    if (updatePriceDto.ProcessedAt != null && processedAt - updatePriceDto.ProcessedAt > 86400)
                    {
                        updatePriceDto.ProcessedAt = null;
                    }
                    taskDto.UpdatePrices.Add(updatePriceDto);
                }
                taskDto.UpdateStatistics();

                var notProcessedByHost =
                    taskDto.UpdatePrices
                        .Where(h => h.Uri.Host == host && h.ProcessedAt == null)
                        .Select(h => h.Uri.AbsoluteUri)
                        .ToList();
                if (!notProcessedByHost.Any()) continue;
                ProductParser.PrepareAndRun(host, taskId, notProcessedByHost, elangPath, sourceName);
            }

            //return Ok(dto);
            return Request.CreateResponse(HttpStatusCode.OK, taskDto);
            //return StatusCode(HttpStatusCode.Accepted);
        }
    }
}