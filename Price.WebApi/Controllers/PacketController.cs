using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
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
        private readonly IContentApi _contentApi;

        delegate void ElasticDelegate(SearchItemParam searchItem, string allSources, string searchItemId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchItemApi"></param>
        /// <param name="contentApi"></param>
        public PacketController(ISearchItemApi searchItemApi, IContentApi contentApi)
        {
            _searchItemApi = searchItemApi;
            _contentApi = contentApi;
        }

        /// <summary>
        /// Описание
        /// </summary>
        /// <param name="searchItemsParam">Состав пакета</param>
        /// <param name="source">Наименование источника для поиска</param>
        /// <param name="keyword">Дополнительные слова для поиска через пробел</param>
        /// <returns></returns>
        [ResponseType(typeof(SearchPacketTaskDto))]
        public HttpResponseMessage Post(List<SearchItemParam> searchItemsParam, [FromUri] string source = "", [FromUri] string keyword = "")
        {
            #region check input parameter

            if (searchItemsParam == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ErrorDto { Message = $"not found {nameof(searchItemsParam)} in parameters" });
            if (!searchItemsParam.Any())
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ErrorDto { Message = $"not found {nameof(searchItemsParam)} in parameters" });
            #endregion

            Delegate dWrite = new ElasticDelegate(ElasticSeacherAndDbWriter.Execute);

            string internet;
            internet = $"{nameof(internet)}";

            var searchPacketTaskDto = new SearchPacketTaskDto();
            var searchInInternet = source.Contains(internet);
            foreach (var searchItem in searchItemsParam)
            {
                if (!string.IsNullOrEmpty(keyword)) searchItem.Name = $"{searchItem.Name} {keyword}";
                searchItem.Name = searchItem.Name.ToLower();
                var json = JsonConvert.SerializeObject(searchItem);
                //TODO: из конфига частоту поиска вычислять
                //var searchRate = $"{DateTime.Today:yyyyMMdd}";
                var searchRate = $"{Utils.GetUtcNow()/AppGlobal.CashSeconds}";
                var id = Md5Logstah.GetDefaultId($"{source}{searchRate}", json);
                var dto = _searchItemApi.GetItem(id);
                var searchItemDto = new SearchItemDto
                {
                    Id = id,
                    Source = source.Replace(internet, ""),
                    Name = searchItem.Name
                };
                if (dto == null)
                {
                    dto = new SearchItemExtDto
                    {
                        Id = id,
                        Source = source,
                        Name = searchItem.Name,
                        ExtId = searchItem.Id,
                        JsonText = json,
                        Normalizer = searchItem.Norm
                    };
                    dto.BeginProcess(Utils.GetUtcNow());
                    
                    if (searchInInternet) CallInternetSearchService(json, dto);
                    _searchItemApi.AddItem(dto);

                    ThreadUtil.FireAndForget(dWrite, new object[] { searchItem, searchItemDto.Source, id });

                    //var listContentDto = ElasticSeacher.Search(searchItem, searchItemDto.Source);
                    //var listContenExtDto = listContentDto.Select(contentDto => new ContentExtDto()
                    //{
                    //    ElasticId = contentDto.Id,
                    //    Name = contentDto.Name,
                    //    Price = contentDto.Price,
                    //    Uri = contentDto.Uri,
                    //    SearchItemId = id,
                    //    CollectedAt = contentDto.CollectedAt,
                    //    Okpd2 = contentDto.Okpd2
                    //})
                    //    .ToList();
                    //_contentApi.AddItems(listContenExtDto);

                }
                searchPacketTaskDto.SearchItems.Add(searchItemDto);
            }


            return Request.CreateResponse(HttpStatusCode.OK, searchPacketTaskDto);
        }

        private static void CallInternetSearchService(string json, SearchItemExtDto dto)
        {
            using (var client = new WebClient())
            {
                var en = Encoding.UTF8;
                var data = en.GetBytes($"{{\"items\":[{json}]}}");
                var uri = new Uri(AppGlobal.InternetSearchHost);
                var result = client.UploadData(uri, "PUT", data);
                var text = en.GetString(result);
                var o = JsonConvert.DeserializeObject<AnalystNewSearchResult>(text);
                dto.InternetSessionId = o.result[0].sessions[0];
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
