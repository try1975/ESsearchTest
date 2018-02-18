using System;
using PriceCommon.Enums;

namespace Common.Dto.Model.NewApi
{
    public class SearchItemCondition
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public TaskStatus Status { get; set; }
        public string Name { get; set; }
        public string ExtId { get; set; }
    }
}