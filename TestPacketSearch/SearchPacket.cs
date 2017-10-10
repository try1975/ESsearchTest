using Newtonsoft.Json;

namespace TestPacketSearch
{
    public class ItemSearchProperty
    {
        [JsonProperty("имя")]
        public string Key { get; set; }
        [JsonProperty("значение")]
        public string Value { get; set; }
        //[JsonProperty("группировка")]
        //public string Group { get; set; }
    }

}