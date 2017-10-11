using System.Collections.Concurrent;
using Price.WebApi.Models.UpdatePrice;

namespace Price.WebApi.Logic.UpdatePrice
{
    public static class UpdatePriceTaskStore
    {
        private static readonly ConcurrentDictionary<string, UpdatePriceTaskDto> Dictionary;

        static UpdatePriceTaskStore()
        {
            Dictionary = new ConcurrentDictionary<string, UpdatePriceTaskDto>();
        }

        public static UpdatePriceTaskDto Get(string id)
        {
            return !Dictionary.ContainsKey(id) ? null : Dictionary[id];
        }

        public static void Post(UpdatePriceTaskDto dto)
        {
            if (dto == null) return;
            var key = dto.Id;
            Dictionary[key] = dto;
        }
    }
}