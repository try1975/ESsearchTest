using Newtonsoft.Json;

namespace PriceCommon.Model
{
    public class Syn
    {
        [JsonProperty("inn")]
        public string[] Inn { get; set; }
        [JsonProperty("tn")]
        public string Tn { get; set; }
    }
}