using GzDocs.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace GzDocs.Controllers
{
    [RoutePrefix("api/common")]
    public class CommonController : ApiController
    {

        [HttpGet, Route("regions")]
        public List<string> GetRegions()
        {
            return GetDirectoriesName(AppGlobal.GzAttachmentsPathPrefix)
                .Select(z => z.Substring(0,2))
                .Distinct()
                .OrderBy(z => z)
                .ToList()
                ;
        }

        [HttpGet, Route("months")]
        public List<string> GetMonths()
        {
            return GetDirectoriesName(AppGlobal.GzAttachmentsPathPrefix)
                .Select(z => z.Substring(3))
                .Distinct()
                .OrderBy(z=>z)
                .ToList()
                ;
        }

        public static List<string> GetDirectoriesName(string path)
        {
            return Directory.GetDirectories(path)
                            .Select(Path.GetFileName)
                            .Where(z => Regex.IsMatch(z, "([0-9]{2})_([0-9]{6})"))
                            .ToList();
        }
    }
}
