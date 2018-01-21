using System.Collections.Concurrent;
using System.IO;
using Common.Dto.Model.XPath;
using Newtonsoft.Json;

namespace Price.WebApi.Logic.Xpath
{
    public static class XPathStore
    {
        public static readonly ConcurrentDictionary<int, XPathDto> Dictionary = new ConcurrentDictionary<int, XPathDto>();

        static XPathStore()
        {
            var storePath = PathService.GetXpathPath();
            if (File.Exists(storePath)) Dictionary = JsonConvert.DeserializeObject<ConcurrentDictionary<int, XPathDto>>(File.ReadAllText(storePath));
        }

        public static XPathDto Get(string uri)
        {
            var key = uri.GetHashCode();
            if (Dictionary.ContainsKey(key)) return Dictionary[key];
            var dto = new XPathDto { Uri = uri };
            Dictionary[key] = dto;
            return dto;
        }

        public static void Post(XPathDto dto)
        {
            if (dto == null) return;
            var key = dto.Uri.GetHashCode();
            Dictionary[key] = dto;
        }
    }
}