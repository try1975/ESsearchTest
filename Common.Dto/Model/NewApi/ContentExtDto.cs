using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PriceCommon.Enums;

namespace Common.Dto.Model.NewApi
{
    public class ContentExtDto : IDto<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Uri { get; set; }
        public long? CollectedAt { get; set; }
        public string ElasticId { get; set; }
        public string Okpd2 { get; set; }
        public string SearchItemId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public PriceType PriceType { get; set; }
    }
}