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

        public IEnumerable<T> Get()
        {
            return _api.GetItems();
        }

        public virtual IHttpActionResult Get(TK id)
        {
            var dto = _api.GetItem(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

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

        public IHttpActionResult Delete(TK id)
        {
            return StatusCode(_api.RemoveItem(id) ? HttpStatusCode.NoContent : HttpStatusCode.Conflict);
        }
    }
}