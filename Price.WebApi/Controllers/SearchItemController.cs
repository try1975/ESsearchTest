using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Common.Dto.Model.NewApi;
using Price.WebApi.Maintenance.Interfaces;

namespace Price.WebApi.Controllers
{
    //[RoutePrefix("api/test")]
    public class SearchItemController : TypedController<SearchItemExtDto, string>
    {
        public SearchItemController(ISearchItemApi api) : base(api)
        {
        }

        [HttpPost]
        [ResponseType(typeof(List<SearchItemExtDto>))]
        [Route("api/SearchItem/ByCondition", Name = nameof(GetByConditions) + "Route")]
        public IHttpActionResult GetByConditions(SearchItemCondition searchItemCondition)
        {
            return Ok(((ISearchItemApi)_api).GetItemsByCondition(searchItemCondition));
        }
    }
}

