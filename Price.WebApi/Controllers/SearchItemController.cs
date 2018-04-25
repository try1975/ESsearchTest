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

        [HttpGet]
        [ResponseType(typeof(SearchItemHeaderDto))]
        [Route("api/SearchItem/status/{id}", Name = nameof(GetSearchItemStatus) + "Route")]
        public IHttpActionResult GetSearchItemStatus(string id)
        {
            return Ok(((ISearchItemApi)_api).GetItemHeader(id));
        }

        [HttpGet]
        [ResponseType(typeof(List<ContentExtDto>))]
        [Route("api/SearchItem/content/{id}", Name = nameof(GetSearchItemContents) + "Route")]
        public IHttpActionResult GetSearchItemContents(string id)
        {
            ((ISearchItemApi)_api).BaseUrl = $"{Request.RequestUri.Scheme}://{Request.RequestUri.Host}:{Request.RequestUri.Port}";
            return Ok(((ISearchItemApi)_api).GetItemContents(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchItemCondition"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(List<SearchItemHeaderDto>))]
        [Route("api/SearchItem/ByCondition", Name = nameof(PostSearchItemsByConditions) + "Route")]
        public IHttpActionResult PostSearchItemsByConditions(SearchItemCondition searchItemCondition)
        {
            return Ok(((ISearchItemApi)_api).GetItemsByCondition(searchItemCondition));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/SearchItem/break/{id}", Name = nameof(PostSearchItemsBreak) + "Route")]
        public IHttpActionResult PostSearchItemsBreak(string id)
        {
            var result = ((ISearchItemApi)_api).SearchItemBreak(id);
            return result ? (IHttpActionResult)Ok() : NotFound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/SearchItem/checked/{id}", Name = nameof(PostSearchItemsChecked) + "Route")]
        public IHttpActionResult PostSearchItemsChecked(string id)
        {
            var result = ((ISearchItemApi)_api).SearchItemChecked(id);
            return result ? (IHttpActionResult)Ok() : NotFound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="extId"></param>
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

