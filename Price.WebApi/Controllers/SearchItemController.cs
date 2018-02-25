﻿using System.Collections.Generic;
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
        [Route("api/SearchItem/ByCondition", Name = nameof(GetSearchItemsByConditions) + "Route")]
        public IHttpActionResult GetSearchItemsByConditions(SearchItemCondition searchItemCondition)
        {
            return Ok(((ISearchItemApi)_api).GetItemsByCondition(searchItemCondition));
        }
    }
}

