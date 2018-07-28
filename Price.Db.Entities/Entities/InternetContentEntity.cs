using System;
using PriceCommon.Enums;

namespace Price.Db.Entities.Entities
{
    public class InternetContentEntity : IEntity<int>
    {
        public int Id { get; set; }
        public int? spgz_Id { get; set; }
        public DateTime dt { get; set; }
        public float? price { get; set; }
        public string url { get; set; }
        public int? src_id { get; set; }
        public string contact_url { get; set; }

        public int? task_id { get; set; }
        public string session_id { get; set; }
        public string preview { get; set; }
        public int? selected { get; set; }
        public byte[] screenshot { get; set; }
        public string currency { get; set; }
        public int? opt { get; set; }
        public string referer { get; set; }
        public string prices { get; set; }
        public float? unit_price { get; set; }
        public string unit { get; set; }
        public string weight { get; set; }
        public string rate { get; set; }
        public string txt { get; set; }
        public string html { get; set; }
        public int? upd { get; set; }
        public PriceStatus PriceStatus { get; set; }
    }
}