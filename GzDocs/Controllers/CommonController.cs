using GzDocs.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using GzCommon;

namespace GzDocs.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/common")]
    public class CommonController : ApiController
    {

        /// <summary>
        /// Список регионов, по которым есть данные
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("regions")]
        public List<RegionItem> GetRegions()
        {
            var regionCodes = GetDirectoriesName(AppGlobal.GzAttachmentsPathPrefix)
                .Select(z => z.Substring(0, 2))
                .Distinct()
                .OrderBy(z => z)
                ;
            return RegionItem.RegionItems
                .Where(z => regionCodes.Contains(z.Code))
                .ToList()
                ;
        }

        /// <summary>
        /// Список ГодМесяц, по которым есть данные
        /// </summary>
        /// <returns></returns>
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

        private static IEnumerable<string> GetDirectoriesName(string path)
        {
            return Directory.GetDirectories(path)
                            .Select(Path.GetFileName)
                            .Where(z => Regex.IsMatch(z, "([0-9]{2})_([0-9]{6})"))
                            .ToList();
        }
    }
}
