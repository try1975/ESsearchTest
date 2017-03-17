using Newtonsoft.Json;

namespace PriceCommon.Model
{
    public class Expert
    {
        public string Name { get; set; }

        [JsonProperty("price_2016")]
        public string Price2016 { get; set; }

        [JsonProperty("price_2017")]
        public string Price2017 { get; set; }

        [JsonProperty("price_2018")]
        public object Price2018 { get; set; }

        [JsonProperty("tru_prim")]
        public string TruPrim { get; set; }

        public string Edizm { get; set; }

        [JsonProperty("expert_date")]
        public string ExpertDate { get; set; }

        [JsonProperty("expert_name")]
        public string ExpertName { get; set; }

        [JsonProperty("expert_number")]
        public string ExpertNumber { get; set; }

        [JsonProperty("zakupki_link")]
        public string ZakupkiLink { get; set; }

        public string Npp { get; set; }

        [JsonProperty("kpgz_name")]
        public string KpgzName { get; set; }

        [JsonProperty("kpgz_code")]
        public string KpgzCode { get; set; }

        [JsonProperty("spgz_name")]
        public string SpgzName { get; set; }

        public string Okpd2 { get; set; }

        public string Comment { get; set; }

        public string Comment2 { get; set; }

        public string Id { get; set; }
    }
}