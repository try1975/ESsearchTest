using System.Collections.Concurrent;
using Price.WebApi.Models;

namespace Price.WebApi.Logic.Fetchee
{
    public static class FetcheeTasks
    {
        private static readonly ConcurrentDictionary<string, FetcheeDto> Dictionary;

        static FetcheeTasks()
        {
            Dictionary = new ConcurrentDictionary<string, FetcheeDto>();
        }

        public static FetcheeDto Get(string id)
        {
            return !Dictionary.ContainsKey(id) ? null : Dictionary[id];
        }

        public static void Post(FetcheeDto dto)
        {
            if (dto == null) return;
            var key = dto.Id;
            Dictionary[key] = dto;
        }
    }
}