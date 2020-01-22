using Db.Entities;
using PriceCommon.Enums;
using System;

namespace Price.Db.Entities.Entities
{
    public class ScheduleEntity : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime NextDate { get; set; }
        public Frequency Frequency { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
    }
}
