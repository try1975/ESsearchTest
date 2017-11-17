using System.Collections.Concurrent;
using System.IO;
using Newtonsoft.Json;

namespace Chrome.XPathApi.Logic
{
    public static class XPathStore
    {
        public static readonly ConcurrentDictionary<int, XPathDto> Dictionary = new ConcurrentDictionary<int, XPathDto>();

        static XPathStore()
        {
            var elangPath = PathService.GetElangPath();
            if (File.Exists(elangPath)) Dictionary = JsonConvert.DeserializeObject<ConcurrentDictionary<int, XPathDto>>(File.ReadAllText(elangPath));
        }

        public static XPathDto Get(string uri)
        {
            var key = uri.GetHashCode();
            if (Dictionary.ContainsKey(key)) return Dictionary[key];
            var dto = new XPathDto { XPathUrl = uri };
            Dictionary[key] = dto;
            return dto;
        }

        public static void Post(XPathDto dto)
        {
            if (dto == null) return;
            var key = dto.XPathUrl.GetHashCode();
            Dictionary[key] = dto;
        }
    }
}