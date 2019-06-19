using GzDocs.Services;
using System;
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
            var regions = new List<string>();
            var directories = GetDirectoriesName(AppGlobal.GzAttachmentsPathPrefix);

            foreach (var directory in directories)
            {
                string region = null;
                try
                {
                    region = Regex.Match(directory, "([0-9]{2})_([0-9]{4})").Groups[1].Value;
                    if (!string.IsNullOrEmpty(region)) regions.Add(region);
                }
                catch (ArgumentException)
                {
                    // Syntax error in the regular expression
                }
                
            }
            return regions.Distinct().OrderBy(z => z).ToList();
        }

        [HttpGet, Route("months")]
        public List<string> GetMonths()
        {
            var regions = new List<string>();
            var directories = GetDirectoriesName(AppGlobal.GzAttachmentsPathPrefix);

            foreach (var directory in directories)
            {
                string month = null;
                try
                {
                    month = Regex.Match(directory, "([0-9]{2})_([0-9]{6})").Groups[2].Value;
                    if (!string.IsNullOrEmpty(month)) regions.Add(month);
                }
                catch (ArgumentException)
                {
                    // Syntax error in the regular expression
                }

            }
            return regions.Distinct().OrderBy(z => z).ToList();
        }

        public static List<string> GetDirectoriesName(string path)
        {
            return Directory.GetDirectories(path)
                            .Select(Path.GetFileName)
                            .ToList();
        }
    }
}
