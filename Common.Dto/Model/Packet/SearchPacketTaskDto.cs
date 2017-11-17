using System.Collections.Generic;
using System.Linq;
using Common.Dto.Logic;
using Newtonsoft.Json;

namespace Common.Dto.Model.Packet
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

        /// <summary>
        /// 
        /// </summary>
        public void UpdateStatistics()
        {
            TotalCount = SearchItems.Count;
            ProcessedCount = SearchItems.Count(z => z.ProcessedAt != null);
            if(TotalCount>0 && TotalCount==ProcessedCount) ProcessedAt = Utils.GetUtcNow();
        }
    }
}