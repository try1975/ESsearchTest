using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Common.Dto.Model.NewApi;
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
    }
}

