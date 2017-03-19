using PriceCommon.Norm;

namespace Norm.MedPrep
{
    public class Detect : IDetect
    {
        public string[] QueryStrings { get; set; }
        public string[] RegExpDetectors { get; set; }
    }
}