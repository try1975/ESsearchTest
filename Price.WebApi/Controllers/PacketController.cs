using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Common.Dto;
using Common.Dto.Logic;
using Common.Dto.Model;
using Common.Dto.Model.NewApi;
using Common.Dto.Model.Packet;
using Newtonsoft.Json;
using Price.WebApi.Logic;
using Price.WebApi.Logic.Packet;
using Price.WebApi.Maintenance.Interfaces;

namespace Price.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/packet")]
    public class PacketController : ApiController
    {
        private readonly ISearchItemApi _searchItemApi;

        private delegate void ElasticDelegate(SearchItemParam searchItem, string allSources, string searchItemId);

        private readonly Delegate _elasticDelegate = new ElasticDelegate(ElasticSeacherAndDbWriter.Execute);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchItemApi"></param>
        public PacketController(ISearchItemApi searchItemApi)
        {
            _searchItemApi = searchItemApi;
        }

        /// <summary>
        /// Описание
        /// </summary>
        /// <param name="searchItemsParam">Состав пакета</param>
        /// <param name="source">Наименование источника для поиска</param>
        /// <param name="keywords">Дополнительные слова для поиска через пробел</param>
        /// <returns></returns>
        [ResponseType(typeof(List<SearchItemHeaderDto>))]
        public HttpResponseMessage Post(List<SearchItemParam> searchItemsParam, [FromUri] string source = "", [FromUri] string keywords = "")
        {
            #region check input parameter

            if (searchItemsParam == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ErrorDto { Message = $"not found {nameof(searchItemsParam)} in parameters" });
            if (!searchItemsParam.Any())
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ErrorDto { Message = $"not found {nameof(searchItemsParam)} in parameters" });
            source = source.ToLower();

            #endregion

           
            string internet;
            internet = $"{nameof(internet)}";

            var resultList = new List<SearchItemHeaderDto>();
            var searchInInternet = source.Contains(internet);
            foreach (var searchItem in searchItemsParam)
            {
                if (!string.IsNullOrEmpty(keywords)) searchItem.Name = $"{searchItem.Name} {keywords}";
                searchItem.Name = searchItem.Name.ToLower();
                var json = JsonConvert.SerializeObject(searchItem);
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
                        Normalizer = searchItem.Norm
                    };
                    dto.BeginProcess(Utils.GetUtcNow());
                    if (searchInInternet) dto.InternetSessionId = GetInternetSessionId(json);
                    _searchItemApi.AddItem(dto);
                    ThreadUtil.FireAndForget(_elasticDelegate, new object[] { searchItem, source, id });
                    resultList.Add(Mapper.Map<SearchItemHeaderDto>(dto));
                }
                else resultList.Add(dtoHeader);
            }
            return Request.CreateResponse(HttpStatusCode.OK, resultList);
        }

        private static string GetInternetSessionId(string json)
        {
            using (var client = new WebClient())
            {
                var en = Encoding.UTF8;
                var data = en.GetBytes($"{{\"items\":[{json}]}}");
                var uri = new Uri(AppGlobal.InternetSearchHost);
                try
                {
                    var result = client.UploadData(uri, "PUT", data);
                    var text = en.GetString(result);
                    var o = JsonConvert.DeserializeObject<AnalystNewSearchResult>(text);
                    return o.result[0].sessions[0];
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
            public AnalystNewSearchSessions[] result { get; set; }
        }

        private class AnalystNewSearchSessions
        {
            public string[] sessions { get; set; }
        }
    }




}
