using System.Collections.Generic;
using PriceCommon.Enums;

namespace Price.Db.Entities.Entities
{
    public class SearchItemEntity : IEntity<string>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string InternetSessionId { get; set; }

        public long? StartProcessed { get; set; }
        public long? LastUpdate { get; set; }
        public long? ProcessedAt { get; set; }

        public TaskStatus Status { get; set; }

        public string Source { get; set; }
        public string ExtId { get; set; }

        public string Normalizer { get; set; }
        public string JsonText { get; set; }
    }
}