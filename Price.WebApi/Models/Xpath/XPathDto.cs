using System;
using System.Text.RegularExpressions;
using Common.Dto;
using Newtonsoft.Json;

namespace Price.WebApi.Models.Xpath
{
    /// <summary>
    /// 
    /// </summary>
    public class XPathDto
    {
        private string _price;
        private string _id;
        private string _uri;

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
        public string Uri
        {
            get { return _uri; }
            set
            {
                _uri = value;
                try
                {
                    var uri = new Uri(_uri);
                    Domain = uri.Host;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public string Price
        {
            get { return _price; }
            set { _price = string.IsNullOrEmpty(value) ? "0" : Regex.Replace(value.Replace(".", ","), "[^0-9,]", ""); }
        }

        [JsonProperty("collectedAt")]
        public long? CollectedAt { get; set; }

        [JsonProperty("id")]
        public string Id
        {
            get { return _id ?? Md5Logstah.GetDefaultId(Uri, Name); }
            set { _id = value; }
        }
    }
}