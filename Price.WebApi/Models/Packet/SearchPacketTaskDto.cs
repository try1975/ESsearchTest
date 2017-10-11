using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Price.WebApi.Models.Packet
{
    public class SearchPacketTaskDto
    {
        public SearchPacketTaskDto(int capacity = 200)
        {
            SearchItems = new List<SearchItemDto>(capacity);
        }

        [JsonIgnore]
        public string BaseUri { get; set; }

        public long? ProcessedAt { get; set; }

        public string Id { get; set; }
        public string Source { get; set; }

        /// <summary>
        /// Всего объектов в задаче
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// Уже обработано объектов в задаче
        /// </summary>
        public int ProcessedCount { get; set; }

        /// <summary>
        /// Список объектов в задаче
        /// </summary>
        public List<SearchItemDto> SearchItems { get; }

        public void UpdateStatistics()
        {
            TotalCount = SearchItems.Count;
            ProcessedCount = SearchItems.Count(z => z.ProcessedAt != null);
        }
    }
}