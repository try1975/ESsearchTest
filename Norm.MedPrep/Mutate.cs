using Newtonsoft.Json;

namespace Norm.MedPrep
{
    public class Mutate
    {
        public decimal Rate { get; set; }

        [JsonProperty("query")]
        public string QueryString { get; set; }
    }
}