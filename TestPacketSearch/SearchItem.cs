using Newtonsoft.Json;

namespace TestPacketSearch
{
    public class SearchItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("наименование")]
        public string Name { get; set; }
        [JsonProperty("категория")]
        public string Category { get; set; }
        [JsonProperty("свойства")]
        public ItemSearchProperty[] ItemSearchProperties { get; set; }
        [JsonProperty("инфо")]
        public string Info { get; set; }
        [JsonProperty("синонимы")]
        public string[] Syn { get; set; }
        [JsonProperty("нормализатор")]
        public string Norm { get; set; }
    }
}