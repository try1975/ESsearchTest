using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Common.Dto;
using Common.Dto.Logic;
using Common.Dto.Model.NewApi;
using Common.Dto.Model.Packet;
using Newtonsoft.Json;
using Price.WebApi.Logic;
using Price.WebApi.Logic.Packet;
using Price.WebApi.Maintenance.Interfaces;

namespace Price.WebApi.Controllers
{
    /// <summary>
    /// Поиск пакетом ТРУ(пакет может состоять из одного ТРУ). Поиск возможен в различных источниках.
    /// </summary>
    [Authorize]
    [RoutePrefix("api/packet")]
    public class PacketController : ApiController
    {
        private readonly ISearchItemApi _searchItemApi;

        private delegate void ElasticDelegate(SearchItemParam searchItem, string allSources, string searchItemId);

        private readonly Delegate _elasticDelegate = new ElasticDelegate(ElasticSeacherAndDbWriter.Execute);
        /// <summary>
        /// Поиск пакетом ТРУ(пакет может состоять из одного ТРУ).
        /// </summary>
        /// <param name="searchItemApi"></param>
        public PacketController(ISearchItemApi searchItemApi)
        {
            _searchItemApi = searchItemApi;
        }

        /// <summary>
        /// Поиск пакетом ТРУ(пакет может состоять из одного ТРУ). Поиск возможен в различных источниках.
        /// </summary>
        /// <param name="searchItemsParam">Состав пакета</param>
        /// <param name="source">Наименование источника для поиска</param>
        /// <param name="keywords">Дополнительные слова для поиска через пробел</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<SearchItemHeaderDto>))]
        public IHttpActionResult Post(List<SearchItemParam> searchItemsParam, [FromUri] string source = "", [FromUri] string keywords = "")
        {
            #region check input parameter

            //if (searchItemsParam == null)
            //    return Request.CreateResponse(HttpStatusCode.BadRequest,
            //        new ErrorDto { Message = $"not found {nameof(searchItemsParam)} in parameters" });
            //if (!searchItemsParam.Any())
            //    return Request.CreateResponse(HttpStatusCode.BadRequest,
            //        new ErrorDto { Message = $"not found {nameof(searchItemsParam)} in parameters" });
            source = source.ToLower();

            #endregion

            var userName = "";
            if (RequestContext.Principal.Identity.IsAuthenticated) userName = RequestContext.Principal.Identity.Name;

            string internet;
            internet = $"{nameof(internet)}";

            var resultList = new List<SearchItemHeaderDto>();
            var searchInInternet = source.Contains(internet);
            foreach (var searchItem in searchItemsParam)
            {
                searchItem.AddKeywords = keywords;
                searchItem.Name = searchItem.Name.ToLower();
                var json = JsonConvert.SerializeObject(searchItem);
                Logger.Log.Info($"Internet json: {json}");
                // Calc search rate
                var searchRate = $"{Utils.GetUtcNow()/AppGlobal.CashSeconds}";
                var id = Md5Logstah.GetDefaultId($"{source}{searchRate}", json);
                var dtoHeader = _searchItemApi.GetItemHeader(id);
                if (dtoHeader == null)
                {
                    var dto = new SearchItemExtDto
                    {
                        Id = id,
                        Source = source,
                        Name = searchItem.Name,
                        ExtId = searchItem.Id,
                        JsonText = json,
                        Normalizer = searchItem.Norm,
                        UserName = userName
                    };
                    dto.BeginProcess(Utils.GetUtcNow());
                    if (searchInInternet) dto.InternetSessionId = GetInternetSessionId(json);
                    _searchItemApi.AddItem(dto);
                    //if (!string.IsNullOrEmpty(keywords)) searchItem.Name = $"{searchItem.Name} {keywords}";
                    ThreadUtil.FireAndForget(_elasticDelegate, searchItem, source, id);
                    resultList.Add(Mapper.Map<SearchItemHeaderDto>(dto));
                }
                else resultList.Add(dtoHeader);
            }
            return Ok(resultList);
        }

        private static string GetInternetSessionId(string json)
        {
            using (var client = new WebClient())
            {
                //client.Headers.Add("content-type", "application/json");
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var en = Encoding.UTF8;
                var strData = $"{{\"items\":[{json}]}}";
                var data = en.GetBytes(strData);
                var uri = new Uri($"{AppGlobal.InternetSearchHost}/NewSearch");
                try
                {
                    var result = client.UploadData(uri, "PUT", data);
                    var text = en.GetString(result);
                    var o = JsonConvert.DeserializeObject<AnalystNewSearchResult>(text);
                    return o.Result[0].Sessions[0];
                }
                catch (Exception exception)
                {
                    Logger.Log.Error(exception);
                    return "";
                }
                
            }
        }

        private class AnalystNewSearchResult
        {
            [JsonProperty("result")]
            public AnalystNewSearchSessions[] Result { get; set; }
        }

        private class AnalystNewSearchSessions
        {
            [JsonProperty("sessions")]
            public string[] Sessions { get; set; }
        }
    }
}
