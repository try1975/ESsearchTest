using Newtonsoft.Json;

namespace Common.Dto.Model.XPath
{
    /// <summary>
    /// 
    /// </summary>
    public class XPathDto
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("xpathName")]
        public string XPathName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("xpathPrice")]
        public string XPathPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("idndomain")]
        public string IdnDomain { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("collectedAt")]
        public long? CollectedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}