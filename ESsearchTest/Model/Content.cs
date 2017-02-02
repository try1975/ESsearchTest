using System;
using Nest;

namespace ESsearchTest.Model
{
    [ElasticsearchType(IdProperty = "Id")]
    public class Content
    {
        public bool Selected { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        //public double Devider { get; set; }

        //public double Nprice
        //{
        //    get
        //    {
        //        var devider = 1.0;
        //        if (Math.Abs(Devider) > 0.001) devider = Devider;
        //        return Convert.ToDouble(Price)/devider;
        //    }
        //}

        public double Nprice
        {
            get
            {
                return Convert.ToDouble(Price);
            }
        }

        public string URI { get; set; }

        public string Seller { get; set; }

        public long? CollectedAt { get; set; }

        public DateTime Collected
        {
            get { return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Convert.ToDouble(CollectedAt)); }
        }

        public string Id { get; set; }
    }
}