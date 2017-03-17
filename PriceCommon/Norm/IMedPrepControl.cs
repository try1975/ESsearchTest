using System.Collections.Generic;

namespace PriceCommon.Norm
{
    public interface IMedPrepControl : IMedPrep
    {
        List<string> LekFormList { get; set; }
        List<string> DozEdList { get; set; }
    }
}