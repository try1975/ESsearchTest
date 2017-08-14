using Newtonsoft.Json;

namespace Norm.MedPrep
{
    public class DozDetector
    {
        public Mutate[] Mutates;

        [JsonProperty("regexp")]
        public string[] RegExpDetectors { get; set; }
    }
}