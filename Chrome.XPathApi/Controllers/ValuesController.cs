using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Http;
using Chrome.XPathApi.Logic;
using Newtonsoft.Json;

namespace Chrome.XPathApi.Controllers
{
    public class ValuesController : ApiController
    {
        //// GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new[] { "value1", "value2" };
        //}

        public XPathDto Get(string link)
        {
            Debug.WriteLine(link);
            var dto = XPathStore.Get(link);
            return dto;
        }

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        public void Post(XPathDto dto)
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
            Debug.WriteLine($"{nameof(dto.XPathName)}={dto.XPathName}");
            Debug.WriteLine($"{nameof(dto.XPathPrice)}={dto.XPathPrice}");
            Debug.WriteLine($"{nameof(dto.XPathUrl)}={dto.XPathUrl}");

            XPathStore.Post(dto);
            
            var elangPath = PathService.GetElangPath();
            var json = JsonConvert.SerializeObject(XPathStore.Dictionary);
            File.WriteAllText(elangPath, json);

        }

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
