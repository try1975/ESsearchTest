using System.Net;
using System.Web.Http;
using Price.WebApi.Maintenance.Interfaces;

namespace Price.WebApi.Controllers
{
    /// <inheritdoc />
    [Authorize]
    public class ContentItemController : ApiController
    {
        private readonly IContentApi _contentApi;
        private readonly IInternetContentApi _internetContentApi;

        /// <inheritdoc />
        public ContentItemController(IContentApi contentApi, IInternetContentApi internetContentApi)
        {
            _contentApi = contentApi;
            _internetContentApi = internetContentApi;
        }

        /// <summary>
        /// Удалить элемент результата поиска
        /// </summary>
        /// <param name="id">Идентификатор элемента результата поиска</param>
        /// <param name="elasticId">Идентификатор elasticId элемента результата поиска</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/contentitem/{id:int}", Name = nameof(DeleteContentItem) + "Route")]
        public IHttpActionResult DeleteContentItem(int id, [FromUri] string elasticId)
        {
            var result = string.IsNullOrEmpty(elasticId) ? _internetContentApi.RemoveItem(id) : _contentApi.RemoveItem(id);
            return StatusCode(result ? HttpStatusCode.NoContent : HttpStatusCode.Conflict);
        }

        /// <summary>
        /// Пометить элемент результата поиска как провереннный.
        /// </summary>
        /// <param name="id">Идентификатор элемента результата поиска</param>
        /// <param name="elasticId">Идентификатор elasticId элемента результата поиска</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/contentitem/checked/{id:int}", Name = nameof(PostContentItemChecked) + "Route")]
        public IHttpActionResult PostContentItemChecked(int id, [FromUri] string elasticId)
        {
            var result = string.IsNullOrEmpty(elasticId) ? _internetContentApi.InternetContentChecked(id) : _contentApi.ContentChecked(id);
            return StatusCode(result ? HttpStatusCode.NoContent : HttpStatusCode.Conflict);
        }

        /// <summary>
        /// Пометить элемент результата поиска как непровереннный.
        /// </summary>
        /// <param name="id">Идентификатор элемента результата поиска</param>
        /// <param name="elasticId">Идентификатор elasticId элемента результата поиска</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/contentitem/notchecked/{id:int}", Name = nameof(PostContentItemNotChecked) + "Route")]
        public IHttpActionResult PostContentItemNotChecked(int id, [FromUri] string elasticId)
        {
            var result = string.IsNullOrEmpty(elasticId) ? _internetContentApi.InternetContentNotChecked(id) : _contentApi.ContentNotChecked(id);
            return StatusCode(result ? HttpStatusCode.NoContent : HttpStatusCode.Conflict);
        }

        /// <summary>
        /// Установить цену для элемента результата поиска
        /// </summary>
        /// <param name="id">Идентификатор элемента результата поиска</param>
        /// <param name="elasticId">Идентификатор elasticId элемента результата поиска</param>
        /// <param name="price">Цена</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/contentitem/setprice/{id:int}", Name = nameof(PostContentItemSetPrice) + "Route")]
        public IHttpActionResult PostContentItemSetPrice(int id, [FromUri] string elasticId, [FromUri] string price)
        {
            var result = string.IsNullOrEmpty(elasticId) ? _internetContentApi.InternetContentSetPrice(id, price) : _contentApi.ContentSetPrice(id, price);
            return StatusCode(result ? HttpStatusCode.NoContent : HttpStatusCode.Conflict);
        }

    }
}
