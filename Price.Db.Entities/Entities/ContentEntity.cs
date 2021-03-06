﻿using Db.Entities;
using PriceCommon.Enums;

namespace Price.Db.Entities.Entities
{
    public class ContentEntity : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Uri { get; set; }
        public long? CollectedAt { get; set; }
        public string ElasticId { get; set; }
        public string Okpd2 { get; set; }
        public string SearchItemId { get; set; }
        public SearchItemEntity SearchItem { get; set; }
        public string Screenshot { get; set; }
        public PriceStatus PriceStatus { get; set; }
        public string Seller { get; set; }
        public string Producer { get; set; }
        public string Phones { get; set; }
        public string RejectReason { get; set; }
        public int ManualPrice { get; set; }
        public ProdStatus? ProdStatus { get; set; }
    }
}