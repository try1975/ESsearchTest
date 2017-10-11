using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Newtonsoft.Json;
using Norm.MedPrep;
using Price.WebApi.Jobs;
using Price.WebApi.Logic;
using Price.WebApi.Logic.Packet;
using Price.WebApi.Models;
using Price.WebApi.Models.Packet;
using PricePipeCore;

namespace Price.WebApi.Controllers
{/// <summary>
 ///     Простой поиск цен
 ///  </summary>
    [RoutePrefix("api/simpleprice")]
    //[Authorize]
    public class SimplePriceController : ApiController
    {
        /// <summary>
        ///     Получение списка результатов поиска без нормализации
        /// </summary>
        /// <param name="text">Текст для поиска</param>
        /// <param name="source">Источник, в котором осуществляется поиск (пусто - источник по умолчанию, gz - госзакупки)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("contents", Name = nameof(GetSimplePriceContents) + "Route")]
        public IEnumerable<ContentDto> GetSimplePriceContents(string text, string source = "")
        {
            Logger.Log.Info($"{nameof(GetSimplePriceContents)}: text={text}&source={source}");
            if (string.IsNullOrEmpty(source)) source = AppSettings.DefaultIndex;
            return Mapper.Map<IEnumerable<ContentDto>>(new SimpleSearcher(source).SimpleSearch(text));
        }

        /// <summary>
        ///  GET - Аналог поиска maybe без нормализации
        /// </summary>
        /// <param name="must">Массив строк (через точку с запятой) - обязательное вхождение</param>
        /// <param name="should">Массив строк (через точку с запятой) - возможное вхождение</param>
        /// <param name="mustNot">Массив строк (через точку с запятой) - обязательное отсутствие</param>
        /// <param name="source">Источник, в котором осуществляется поиск (пусто - источник по умолчанию, gz - госзакупки)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("maybe", Name = nameof(GetMaybeSearch) + "Route")]
        public IEnumerable<ContentDto> GetMaybeSearch(string must = "", string should = "", string mustNot = "", string source = "")
        {
            Logger.Log.Info($"{nameof(GetMaybeSearch)}: {nameof(must)}={must}&{nameof(should)}={should}&{nameof(mustNot)}={mustNot}&{nameof(source)}={source}");
            if (string.IsNullOrEmpty(source)) source = AppSettings.DefaultIndex;
            return Mapper.Map<IEnumerable<ContentDto>>(new SimpleSearcher(source).MaybeSearch(must, should, mustNot));
        }

        /// <summary>
        ///  POST - Аналог поиска maybe без нормализации
        /// </summary>
        /// <param name="maybeDto">Структура для задания параметров поиска типа Maybe</param>
        /// <param name="source">Источник, в котором осуществляется поиск (пусто - источник по умолчанию, gz - госзакупки)</param>
        /// <returns></returns>
        [HttpPost]
        [Route("maybe", Name = nameof(PostMaybeSearch) + "Route")]
        public IEnumerable<ContentDto> PostMaybeSearch(MaybeDto maybeDto, [FromUri]string source = "")
        {
            Logger.Log.Info($"{nameof(PostMaybeSearch)}: {JsonConvert.SerializeObject(maybeDto)}");
            if (string.IsNullOrEmpty(source)) source = AppSettings.DefaultIndex;
            var delimiter = SimpleSearcher.ListDelimiter.FirstOrDefault();
            var must = maybeDto.Musts == null ? "" : string.Join(delimiter, maybeDto.Musts);
            var should = maybeDto.Shoulds == null ? "" : string.Join(delimiter, maybeDto.Shoulds);
            var mustNot = maybeDto.MustNots == null ? "" : string.Join(delimiter, maybeDto.MustNots);
            return Mapper.Map<IEnumerable<ContentDto>>(new SimpleSearcher(source).MaybeSearch(must, should, mustNot));
        }

        /// <summary>
        /// Синхронный поиск пакетом
        /// </summary>
        /// <param name="searchItemsParam">Список искомых позиций</param>
        /// <param name="source">Источник, в котором осуществляется поиск (пусто - источник по умолчанию, gz - госзакупки)</param>
        /// <returns></returns>
        [HttpPost]
        [Route("packet_sync", Name = nameof(PostPacket) + "Route")]
        public SearchPacketTaskDto PostPacket(List<SearchItemParam> searchItemsParam, [FromUri]string source = "")
        {
            #region check input parameter

            if (searchItemsParam == null) return new SearchPacketTaskDto();
            if (!searchItemsParam.Any()) return new SearchPacketTaskDto();

            #endregion
            if (string.IsNullOrEmpty(source)) source = AppSettings.DefaultIndex;
            Logger.Log.Info($"{nameof(PostPacket)}: {JsonConvert.SerializeObject(searchItemsParam)}");
            
            var processedAt = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var baseUri = $"{Request.RequestUri.Scheme}://{Request.RequestUri.Host}:{Request.RequestUri.Port}";
            var taskId = IdService.GenerateId();
            var searchPacketTaskDto = new SearchPacketTaskDto(searchItemsParam.Count) { Id = taskId, Source = source, BaseUri = baseUri };
            SearchPacketTaskStore.Post(searchPacketTaskDto);

            var delimiter = SimpleSearcher.ListDelimiter.FirstOrDefault();

            foreach (var searchItem in searchItemsParam)
            {
                var searchItemDto = SearchItemStore.Get($"{searchItem.Id}{source}");
                searchItemDto.Id = searchItem.Id;
                searchItemDto.Name = searchItem.Name;
                searchItemDto.Source = source;
                searchItemDto.SearchItem = searchItem;
                searchPacketTaskDto.SearchItems.Add(searchItemDto);
                // if one day age
                if (searchItemDto.ProcessedAt != null && processedAt - searchItemDto.ProcessedAt > 86400)
                {
                    searchItemDto.ProcessedAt = null;
                    searchItemDto.Status = TaskStatus.NotProcessed;
                }
                else
                {
                    continue;
                }
                

                try
                {
                    var splitResult = searchItem.Name.Split(SimpleSearcher.ListDelimiter, StringSplitOptions.RemoveEmptyEntries);
                    if (searchItem.Norm != null && searchItem.Norm.Equals("лек_средства:основной"))
                    {
                        var name = string.Join(" ", splitResult.Select(p => p.Trim()));
                        var firstWords = string.Empty;
                        var lekForm = string.Empty;
                        var upak = string.Empty;
                        var dozValue = string.Empty;
                        var dozKey = string.Empty;
                        foreach (var property in searchItem.SearchItemProperties)
                        {
                            if (property.Key.Equals("МНН")) firstWords = property.Value.Trim();
                            if (property.Key.Equals("Форма выпуска"))
                            {
                                var lekFormNorm = new LekFormNorm { InitialName = property.Value.Trim() };
                                lekForm = lekFormNorm.NormResult;
                            }
                            if (property.Key.Equals("Дозировка"))
                            {
                                var dozNorm = new DozNorm() { InitialName = property.Value.Trim() };
                                dozValue = dozNorm.DozValue;
                                dozKey = dozNorm.DozKey;
                            }
                            if (property.Key.Equals("Фасовка"))
                            {
                                var upakNorm = new UpakNorm(name) { InitialName = property.Value.Trim() };
                                upak = upakNorm.NormResult;
                            }
                        }
                        var syn = string.Empty;
                        if (searchItem.Syn != null && searchItem.Syn.Length > 0)
                        {
                            syn = firstWords + "," + string.Join(",", searchItem.Syn.Select(p => p.Trim()));
                        }

                        searchItemDto.Content =
                            Mapper.Map<IEnumerable<ContentDto>>(new PharmacySearcher(source).Search(name, firstWords, lekForm, upak, dozValue, dozKey, syn: syn));
                    }
                    else
                    {
                        if (splitResult.Length > 0)
                        {
                            var must = splitResult[0].Trim();
                            var should = string.Empty;
                            if (splitResult.Length > 1)
                            {
                                should = string.Join(delimiter, splitResult.Skip(1).Select(p => p.Trim()));
                            }
                            searchItemDto.Content =
                                Mapper.Map<IEnumerable<ContentDto>>(
                                    new SimpleSearcher(source).MaybeSearch(must, should, string.Empty));
                        }
                    }
                    searchItemDto.ProcessedAt = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    searchItemDto.Status = TaskStatus.Ok;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    Logger.Log.Error($"{e}");
                    searchItemDto.ProcessedAt = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    searchItemDto.Status = TaskStatus.Error;
                }

            }
            searchPacketTaskDto.UpdateStatistics();
            if (searchPacketTaskDto.TotalCount == searchPacketTaskDto.ProcessedCount)
                searchPacketTaskDto.ProcessedAt = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return searchPacketTaskDto;
        }


        /// <summary>
        /// Результат поиска пакетом
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("packet/{id}", Name = nameof(GetPacket2) + "Route")]
        [ResponseType(typeof(SearchPacketTaskDto))]
        public IHttpActionResult GetPacket2(string id)
        {
            return Ok(SearchPacketTaskStore.Get(id));
        }

        /// <summary>
        /// Асинхронный поиск пакетом
        /// </summary>
        /// <param name="searchItemsParam">Список искомых позиций</param>
        /// <param name="source">Источник, в котором осуществляется поиск (пусто - источник по умолчанию, gz - госзакупки)</param>
        /// <returns></returns>
        [HttpPost]
        [Route("packet", Name = nameof(PostPacket2) + "Route")]
        [ResponseType(typeof(SearchPacketTaskDto))]
        public HttpResponseMessage PostPacket2(List<SearchItemParam> searchItemsParam, [FromUri] string source = "")
        {
            #region check input parameter

            if (searchItemsParam == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ErrorDto { Message = $"not found {nameof(searchItemsParam)} in parameters" });
            if (!searchItemsParam.Any())
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ErrorDto { Message = $"not found {nameof(searchItemsParam)} in parameters" });

            #endregion
            if (string.IsNullOrEmpty(source)) source = AppSettings.DefaultIndex;
            Logger.Log.Info($"{nameof(PostPacket2)}: {JsonConvert.SerializeObject(searchItemsParam)}");

            var processedAt = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var baseUri = $"{Request.RequestUri.Scheme}://{Request.RequestUri.Host}:{Request.RequestUri.Port}";
            var taskId = IdService.GenerateId();
            var searchPacketTaskDto = new SearchPacketTaskDto(searchItemsParam.Count) { Id = taskId, Source = source, BaseUri = baseUri };
            SearchPacketTaskStore.Post(searchPacketTaskDto);

            foreach (var searchItem in searchItemsParam)
            {
                var searchItemDto = SearchItemStore.Get($"{searchItem.Id}{source}");
                searchItemDto.Id = searchItem.Id;
                searchItemDto.Name = searchItem.Name;
                searchItemDto.Source = source;
                searchItemDto.SearchItem = searchItem;
                // if one day age
                if (searchItemDto.ProcessedAt != null && processedAt - searchItemDto.ProcessedAt > 86400)
                {
                    searchItemDto.ProcessedAt = null;
                    searchItemDto.Status = TaskStatus.NotProcessed;
                }
                searchPacketTaskDto.SearchItems.Add(searchItemDto);
            }
            searchPacketTaskDto.UpdateStatistics();
            if (searchPacketTaskDto.TotalCount == searchPacketTaskDto.ProcessedCount)
                searchPacketTaskDto.ProcessedAt = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            return Request.CreateResponse(HttpStatusCode.OK, searchPacketTaskDto);
        }
    }
}
