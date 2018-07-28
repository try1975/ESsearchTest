using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Common.Dto;
using Price.WebApi.Maintenance;

namespace Price.WebApi.Controllers
{
    public class TypedController<T, TK> : ApiController where T : class, IDto<TK>
    {
        protected readonly ITypedApi<T, TK> _api;

        public TypedController(ITypedApi<T, TK> api)
        {
            _api = api;
        }

        /// <summary>
        /// Получить элементы (служебный метод)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Get()
        {
            return _api.GetItems();
        }

        /// <summary>
        /// Получить по идентификатору (служебный метод)
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        public virtual IHttpActionResult Get(TK id)
        {
            var dto = _api.GetItem(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        /// <summary>
        /// Записать элемент ((служебный метод))
        /// </summary>
        /// <param name="creatingDto"></param>
        /// <returns></returns>
        public IHttpActionResult Post(T creatingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dto = _api.AddItem(creatingDto);
            if (dto == null)
            {
                return BadRequest("POST failed.");
            }
            return CreatedAtRoute("DefaultApi", new { id = dto.Id }, dto);
        }

        /// <summary>
        /// Модифицировать элемент (служебный метод)
        /// </summary>
        /// <param name="changedDto"></param>
        /// <returns></returns>
        public IHttpActionResult Put(T changedDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dto = _api.ChangeItem(changedDto);
            if (dto == null)
            {
                return BadRequest("PUT failed.");
            }
            return CreatedAtRoute("DefaultApi", new { id = dto.Id }, dto);
        }

        /// <summary>
        /// Удалить элемент (служебный метод)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(TK id)
        {
            return StatusCode(_api.RemoveItem(id) ? HttpStatusCode.NoContent : HttpStatusCode.Conflict);
        }
    }
}