using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Price.WebApi.Maintenance.Interfaces;

namespace Price.WebApi.Controllers
{
    public class ContentItemController : ApiController
    {
        private IContentApi _contentApi;
        private IInternetContentApi _internetContentApi;

        public ContentItemController(IContentApi contentApi, IInternetContentApi internetContentApi)
        {
            _contentApi = contentApi;
            _internetContentApi = internetContentApi;
        }

        [HttpDelete]
        [Route("api/contentitem/{id:int}", Name = "Route")]
        public IHttpActionResult Delete(int id, [FromUri] string elasticId)
        {
            var result = string.IsNullOrEmpty(elasticId) ? _internetContentApi.RemoveItem(id) : _contentApi.RemoveItem(id);
            return StatusCode(result ? HttpStatusCode.NoContent : HttpStatusCode.Conflict);
        }

    }
}
