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

            Logger.Log.Info($"{nameof(PostPacket)}: {JsonConvert.SerializeObject(searchItemsParam)}");

            var processedAt = Utils.GetUtcNow();
            var searchPacketTaskDto = GetSearchPacketTaskDto(source, searchItemsParam.Count);

            foreach (var searchItem in searchItemsParam)
            {
                var searchItemDto = GetSearchItemDto(searchItem, searchPacketTaskDto);
                searchPacketTaskDto.SearchItems.Add(searchItemDto);
                if (!GreaterOneDayAge(searchItemDto, processedAt)) continue;
                PacketItemSeacher.Search(searchItem, searchItemDto);
            }
            searchPacketTaskDto.UpdateStatistics();
            return searchPacketTaskDto;
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

            Logger.Log.Info($"{nameof(PostPacket2)}: {JsonConvert.SerializeObject(searchItemsParam)}");

            var processedAt = Utils.GetUtcNow();
            var searchPacketTaskDto = GetSearchPacketTaskDto(source, searchItemsParam.Count);

            foreach (var searchItem in searchItemsParam)
            {
                var searchItemDto = GetSearchItemDto(searchItem, searchPacketTaskDto);
                searchPacketTaskDto.SearchItems.Add(searchItemDto);
                GreaterOneDayAge(searchItemDto, processedAt);
            }
            searchPacketTaskDto.UpdateStatistics();

            return Request.CreateResponse(HttpStatusCode.OK, searchPacketTaskDto);
        }

        private static bool GreaterOneDayAge(SearchItemDto searchItemDto, long processedAt)
        {
            if (searchItemDto.ProcessedAt == null || !(processedAt - searchItemDto.ProcessedAt > 86400)) return false;
            searchItemDto.ProcessedAt = null;
            searchItemDto.Status = TaskStatus.NotProcessed;
            return true;
        }

        private static SearchItemDto GetSearchItemDto(SearchItemParam searchItem, SearchPacketTaskDto searchPacketTaskDto)
        {
            var searchItemDto = SearchItemStore.Get($"{searchItem.Id}{searchPacketTaskDto.Source}");
            searchItemDto.Id = searchItem.Id;
            searchItemDto.Name = searchItem.Name;
            searchItemDto.Source = searchPacketTaskDto.Source;
            searchItemDto.SearchItem = searchItem;
            return searchItemDto;
        }

        private SearchPacketTaskDto GetSearchPacketTaskDto(string source, int cnt)
        {
            var searchPacketTaskDto = new SearchPacketTaskDto(cnt)
            {
                Id = IdService.GenerateId(),
                Source = string.IsNullOrEmpty(source) ? AppSettings.DefaultIndex : source,
                BaseUri = $"{Request.RequestUri.Scheme}://{Request.RequestUri.Host}:{Request.RequestUri.Port}"
            };
            SearchPacketTaskStore.Post(searchPacketTaskDto);
            return searchPacketTaskDto;
        }
    }
}
