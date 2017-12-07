using System.Text.RegularExpressions;
using Common.Dto;

namespace Price.WebApi.Models.Xpath
{
    /// <summary>
    /// 
    /// </summary>
    public class XPathDto
    {
        private string _price;
        private string _id;

        /// <summary>
        /// 
        /// </summary>
        public string XPathName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string XPathPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Uri { get; set; }

        public string Name { get; set; }

        public string Price
        {
            get { return _price; }
            set { _price = string.IsNullOrEmpty(value) ? "0" : Regex.Replace(value.Replace(".", ","), "[^0-9,]", ""); }
        }

        public long? CollectedAt { get; set; }

        public string Id
        {
            get { return _id ?? Md5Logstah.GetDefaultId(Uri, Name); }
            set { _id = value; }
        }
    }
}