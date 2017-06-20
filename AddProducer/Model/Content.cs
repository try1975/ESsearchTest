using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AddProducer.Model
{
    public class Content
    {
        public string producer { get; set; }
        public string seller { get; set; }
        public long? collectedAt { get; set; }

        //public virtual DateTime Collected => new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Convert.ToDouble(collectedAt));
        public string uRI { get; set; }
        public string price { get; set; }
        public string name { get; set; }
        public string column6 { get; set; }
        public string column7 { get; set; }
        public string column8 { get; set; }
        public string column9 { get; set; }
        public string id { get; set; }
        [JsonProperty(propertyName:"@timestamp")]
        public DateTime Timestamp { get; set; }
    }
}