using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using AutoMapper;
using Common.Dto.Model.NewApi;
using Gma.CodeCloud.Controls.TextAnalyses.Processing;
using Price.WebApi.Maintenance.Interfaces;

namespace Price.WebApi.Controllers
{
    //[RoutePrefix("api/test")]
    /// <summary>
    /// 
    /// </summary>
    public class SearchItemController : TypedController<SearchItemExtDto, string>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="api"></param>
        public SearchItemController(ISearchItemApi api) : base(api)
        {
        }

        /// <summary>
        /// Получить заголовок поискового запроса по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор поискового запроса</param>
        /// <returns>Заголовок поискового запроса</returns>
        [HttpGet]
        [ResponseType(typeof(SearchItemHeaderDto))]
        [Route("api/SearchItem/status/{id}", Name = nameof(GetSearchItemStatus) + "Route")]
        public IHttpActionResult GetSearchItemStatus(string id)
        {
            return Ok(((ISearchItemApi)_api).GetItemHeader(id));
        }

        /// <summary>
        /// Получить элементы результата поискового запроса
        /// </summary>
        /// <param name="id">Идентификатор поискового запроса</param>
        /// <returns>Список элементов результата поискового запроса</returns>
        [HttpGet]
        [ResponseType(typeof(List<ContentExtDto>))]
        [Route("api/SearchItem/content/{id}", Name = nameof(GetSearchItemContents) + "Route")]
        public IHttpActionResult GetSearchItemContents(string id)
        {
            ((ISearchItemApi)_api).BaseUrl = $"{Request.RequestUri.Scheme}://{Request.RequestUri.Host}:{Request.RequestUri.Port}";
            return Ok(((ISearchItemApi)_api).GetItemContents(id));
        }

        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/dd541344.aspx
        /// </summary>
        /// <param name="id"></param>
        /// <param name="queryOptions"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<ContentExtDto>))]
        [Route("api/SearchItem/content/filtered/{id}", Name = nameof(ODataGetSearchItemContents) + "Route")]
        public IHttpActionResult ODataGetSearchItemContents(string id, ODataQueryOptions<ContentExtTxtDto> queryOptions)
        {
            //7fc2154233d9f247f98d218dc7503bdc
            var result = ((ISearchItemApi) _api).GetItemContentsTxt(id);
            var enumerableQuery = result == null ? null : new EnumerableQuery<ContentExtTxtDto>(result);
            //https://d-fens.ch/2017/02/26/modifying-odataqueryoptions-on-the-fly/
            var dtos = queryOptions.ApplyTo(enumerableQuery) as IQueryable<ContentExtTxtDto>;
            return Ok(Mapper.Map<List<ContentExtDto>>(dtos.ToList()));
        }

        /// <summary>
        /// Получить заголовки поисковых запросов по условиям
        /// </summary>
        /// <param name="searchItemCondition"></param>
        /// <returns>Список заголовков поисковых запросов по указанным условиям</returns>
        [HttpPost]
        [ResponseType(typeof(List<SearchItemHeaderDto>))]
        [Route("api/SearchItem/ByCondition", Name = nameof(PostSearchItemsByConditions) + "Route")]
        public IHttpActionResult PostSearchItemsByConditions(SearchItemCondition searchItemCondition)
        {
            return Ok(((ISearchItemApi)_api).GetItemsByCondition(searchItemCondition));
        }

        /// <summary>
        /// Прервать работу поисковго запроса с определенным идентификатором, возвращает 200, если поисковой запрос найден и прерван, иначе 404
        /// </summary>
        /// <param name="id">Идентификатор поискового запроса</param>
        /// <returns>200, если поисковой запрос найден и прерван, иначе 404</returns>
        [HttpPost]
        [Route("api/SearchItem/break/{id}", Name = nameof(PostSearchItemsBreak) + "Route")]
        public IHttpActionResult PostSearchItemsBreak(string id)
        {
            var result = ((ISearchItemApi)_api).SearchItemBreak(id);
            return result ? (IHttpActionResult)Ok() : NotFound();
        }

        /// <summary>
        /// Пометить поисковый запрос как проверенный - его нельзя будет случайно удалить.
        /// Только по проверенным поисковым запросам и проверенным элементам производится автоматическая актуализация цен.
        /// Возвращает 200, если поисковой запрос найден и помечен как проверенный, иначе 404
        /// </summary>
        /// <param name="id">Идентификатор поискового запроса</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/SearchItem/checked/{id}", Name = nameof(PostSearchItemsChecked) + "Route")]
        public IHttpActionResult PostSearchItemsChecked(string id)
        {
            var result = ((ISearchItemApi)_api).SearchItemChecked(id);
            return result ? (IHttpActionResult)Ok() : NotFound();
        }

        /// <summary>
        /// Переместить элементы результатов поискового запроса в другой поисковой запрос (разделить результаты). 
        /// </summary>
        /// <param name="dtos">Список элементов результата поискового запроса для перемещения</param>
        /// <param name="id">Идентификатор поискового запроса откуда будут перемещены элементы результата</param>
        /// <param name="name">Новое наименование ТРУ (если пустое - будет взято из исходного поискового запроса)</param>
        /// <param name="extId">Новая метка (если пустое - будет взято из исходного поискового запроса)</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(SearchItemHeaderDto))]
        [Route("api/SearchItem/Move", Name = nameof(PostContentItemsMove) + "Route")]
        public IHttpActionResult PostContentItemsMove(IEnumerable<ContentMoveDto> dtos, [FromUri]string id, [FromUri]string name, [FromUri]string extId)
        {
            var result = ((ISearchItemApi)_api).MoveContents(dtos, id, name, extId);
            return result!=null ? (IHttpActionResult)Ok(result) : NotFound();
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<IWord>))]
        [Route("api/SearchItem/wordscloud/{id}", Name = nameof(GetWordsCloud) + "Route")]
        public IHttpActionResult GetWordsCloud(string id)
        {
            var result = ((ISearchItemApi)_api).WordsCloud(id);
            return result!=null ? (IHttpActionResult)Ok(result) : NotFound();
        }
    }
    
}

