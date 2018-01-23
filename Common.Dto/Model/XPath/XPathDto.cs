using System;
using System.Globalization;
using System.Text.RegularExpressions;
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
        [JsonProperty("XPathName")]
        public string XPathName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("XPathPrice")]
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

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Domain))
            {
                var uri = new Uri(Uri);
                Domain = uri.Host;
                //рекламное-производство.рф == http://xn----7sbhajcbriqlnnocdckjk1aw.xn--p1ai/
                if (string.IsNullOrEmpty(IdnDomain))
                {
                    var idn = new IdnMapping();
                    IdnDomain = idn.GetUnicode(uri.Host);
                }
            }
            Name = Name.Trim();
            Price = string.IsNullOrEmpty(Price) ? "0" : Regex.Replace(Price.Replace(".", ","), "[^0-9,]", "");
            if (string.IsNullOrEmpty(Id)) Id = Md5Logstah.GetDefaultId(Uri, Name);
        }
    }
}