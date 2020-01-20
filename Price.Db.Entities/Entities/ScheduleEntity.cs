using Db.Entities;
using PriceCommon.Enums;
using System;

namespace Price.Db.Entities.Entities
{
    public class ScheduleEntity : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime? LastDate { get; set; }
        public DateTime? NextRequestDate { get; set; }
        public Frequency Frequency { get; set; }
        public bool IsActive { get; set; }
        public string Note { get; set; }
    }
}
