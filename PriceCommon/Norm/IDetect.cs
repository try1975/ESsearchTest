using Newtonsoft.Json;

namespace PriceCommon.Norm
{
    public interface IDetect
    {
        [JsonProperty("query")]
        string[] QueryStrings { get; set; }

        [JsonProperty("regexp")]
        string[] RegExpDetectors { get; set; }
    }
}