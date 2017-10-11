using System.Collections.Concurrent;
using Price.WebApi.Models.Packet;

namespace Price.WebApi.Logic.Packet
{
    public static class SearchPacketTaskStore
    {
        public static readonly ConcurrentDictionary<string, SearchPacketTaskDto> Dictionary;

        static SearchPacketTaskStore()
        {
            Dictionary = new ConcurrentDictionary<string, SearchPacketTaskDto>();
        }

        public static SearchPacketTaskDto Get(string id)
        {
            return !Dictionary.ContainsKey(id) ? null : Dictionary[id];
        }

        public static void Post(SearchPacketTaskDto dto)
        {
            if (dto == null) return;
            var key = dto.Id;
            Dictionary[key] = dto;
        }
    }
}