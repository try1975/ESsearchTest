using System.Net;
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
        [Route("api/contentitem/{id:int}", Name = nameof(DeleteContentItem) + "Route")]
        public IHttpActionResult DeleteContentItem(int id, [FromUri] string elasticId)
        {
            var result = string.IsNullOrEmpty(elasticId) ? _internetContentApi.RemoveItem(id) : _contentApi.RemoveItem(id);
            return StatusCode(result ? HttpStatusCode.NoContent : HttpStatusCode.Conflict);
        }

        [HttpPost]
        [Route("api/contentitem/checked/{id:int}", Name = nameof(PostContentItemChecked) + "Route")]
        public IHttpActionResult PostContentItemChecked(int id, [FromUri] string elasticId)
        {
            var result = string.IsNullOrEmpty(elasticId) ? _internetContentApi.InternetContentChecked(id) : _contentApi.ContentChecked(id);
            return StatusCode(result ? HttpStatusCode.NoContent : HttpStatusCode.Conflict);
        }

    }
}
