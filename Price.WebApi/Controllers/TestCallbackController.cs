using System.Diagnostics;
using System.Web.Http;

namespace Price.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/TestCallback")]
    public class TestCallbackController : ApiController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post()
        {
            var data = Request.Content.ReadAsStringAsync().Result;
            Debug.WriteLine(data);

            return  Ok(data);
        }
    }
}
