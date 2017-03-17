using System.Collections.Generic;
using Nest;

namespace PriceCommon.Norm
{
    public interface INormResult
    {
        List<QueryContainer> QueryContainer { get; }
    }
}