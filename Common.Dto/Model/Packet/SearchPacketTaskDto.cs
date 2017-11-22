using System;
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
            if (Source.ToLower().Contains("internet") && ProcessedAt == null)
            {
                foreach (var searchItem in SearchItems)
                {
                    if (searchItem.Status != TaskStatus.InProcess) continue;
                    var span = Utils.GetUtcNow() - searchItem.StartProcessed;
                    // 2 minute
                    if (span >= 120)
                    {
                        searchItem.ProcessedAt = Utils.GetUtcNow();
                        searchItem.Status = TaskStatus.Ok;
                        continue;
                    }
                    if (searchItem.Content == null) continue;
                    var cnt = searchItem.Content.Count();
                    if (cnt == 0) continue;
                    if (cnt >= 60)
                    {
                        searchItem.ProcessedAt = Utils.GetUtcNow();
                        searchItem.Status = TaskStatus.Ok;
                    }
                }
            }
            TotalCount = SearchItems.Count;
            ProcessedCount = SearchItems.Count(z => z.ProcessedAt != null);
            if (TotalCount > 0 && TotalCount == ProcessedCount) ProcessedAt = Utils.GetUtcNow();
        }
    }
}