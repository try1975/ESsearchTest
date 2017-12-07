using System;
using System.Text.RegularExpressions;

namespace Common.Dto.Model
{
    public class BasicContentDto
    {
        private string _price;
        private string _id;
        private long? _collectedAt;

        public string Uri { get; set; }

        public string Name { get; set; }

        public string Price
        {
            get { return _price; }
            set { _price = string.IsNullOrEmpty(value) ? "0" : Regex.Replace(value.Replace(".", ","), "[^0-9,]", ""); }
        }

        public long? CollectedAt
        {
            get { return _collectedAt ?? (long?)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds; }
            set { _collectedAt = value; }
        }

        public string Id
        {
            get { return _id ?? Md5Logstah.GetDefaultId(Uri, Name); }
            set { _id = value; }
        }
    }
}