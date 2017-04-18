using System;

namespace PriceCommon.Model
{
    public class Content
    {
        public bool Selected { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public double Nprice => Convert.ToDouble(Price);

        public string Uri { get; set; }

        public string Seller { get; set; }

        public long? CollectedAt { get; set; }

        public virtual DateTime Collected => new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Convert.ToDouble(CollectedAt))/*.Date*/;

        public string Id { get; set; }
    }
}