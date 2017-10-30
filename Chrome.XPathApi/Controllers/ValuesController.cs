using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;

namespace Chrome.XPathApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        public string Get(string link)
        {
            Debug.WriteLine(link);
            return link;
            
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post(xPathDto dto)
        {
            if (Request.Headers.Contains("Origin"))
            {
                var values = Request.Headers.GetValues("Origin");
                if (values != null)
                {
                    Debug.WriteLine(values.FirstOrDefault());
                }
                // Do stuff with the values... probably .FirstOrDefault()
            }
            Debug.WriteLine(dto.xPathName);
            Debug.WriteLine(dto.xPathPrice);
            Debug.WriteLine(dto.xPathUrl);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    public class xPathDto
    {
        public string xPathName { get; set; }
        public string xPathPrice { get; set; }

        public string xPathUrl { get; set; }
    }
}
