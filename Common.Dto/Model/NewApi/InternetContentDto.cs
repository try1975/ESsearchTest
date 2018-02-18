using System;
using Newtonsoft.Json;

namespace Common.Dto.Model.NewApi
{
    public class InternetContentDto : IDto<int>
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonProperty("Dt")]
        public DateTime dt { get; set; }
        [JsonProperty("Price")]
        public float? price { get; set; }
        [JsonProperty("Url")]
        public string url { get; set; }
    }
}