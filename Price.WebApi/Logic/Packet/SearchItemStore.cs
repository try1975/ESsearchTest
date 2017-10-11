using System.Collections.Concurrent;
using Price.WebApi.Models.Packet;

namespace Price.WebApi.Logic.Packet
{
    public static class SearchItemStore
    {
        public static readonly ConcurrentDictionary<int, SearchItemDto> Dictionary;
        static SearchItemStore()
        {
            Dictionary = new ConcurrentDictionary<int, SearchItemDto>();
        }

        public static int Count()
        {
            return Dictionary.Count;
        }

        public static SearchItemDto Get(string aKey)
        {
            var key = aKey.GetHashCode();
            if (Dictionary.ContainsKey(key)) return Dictionary[key];
            var dto = new SearchItemDto();
            Dictionary[key] = dto;
            return dto;
        }

        public static void Post(SearchItemDto dto)
        {
            if (dto == null) return;
            var key = dto.Key.GetHashCode();
            Dictionary[key] = dto;
        }
    }
}