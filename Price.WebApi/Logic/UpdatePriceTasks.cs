using System.Collections.Concurrent;
using Price.WebApi.Models;

namespace Price.WebApi.Logic
{
    public static class UpdatePriceTasks
    {
        private static readonly ConcurrentDictionary<string, TaskDto> Dictionary;

        static UpdatePriceTasks()
        {
            Dictionary = new ConcurrentDictionary<string, TaskDto>();
        }

        public static TaskDto Get(string id)
        {
            return !Dictionary.ContainsKey(id) ? null : Dictionary[id];
        }

        public static void Post(TaskDto dto)
        {
            if (dto == null) return;
            var key = dto.Id;
            Dictionary[key] = dto;
        }
    }
}