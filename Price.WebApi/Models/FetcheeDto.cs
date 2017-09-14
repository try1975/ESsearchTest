using System;

namespace Price.WebApi.Models
{
    public class FetcheeDto
    {
        public string id { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public float price { get; set; }
        public string currency { get; set; }
        public string brand { get; set; }
        public bool out_of_stock { get; set; }
        public bool removed { get; set; }
        public bool not_shop { get; set; }
        public bool not_product { get; set; }
        public bool unprocessed { get; set; }
        public string img_url { get; set; }
        public DateTime created_at { get; set; }
        public DateTime last_track_at { get; set; }
    }

    public class FetcheeTaskReturnDto
    {

        public bool success { get; set; }
        public string id { get; set; }
    }


    public class FetsheeTaskDto
    {
        public string url { get; set; }
        public string api_key { get; set; }
        public string callback_url { get; set; }
    }


}