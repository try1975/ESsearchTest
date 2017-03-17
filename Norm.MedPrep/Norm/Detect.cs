using PriceCommon.Norm;

namespace Norm.MedPrep.Norm
{
    public class Detect : IDetect
    {
        public string[] QueryStrings { get; set; }
        public string[] RegExpDetectors { get; set; }
    }
}