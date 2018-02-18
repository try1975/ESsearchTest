using System.Collections.Generic;
using Common.Dto.Model.NewApi;

namespace Price.WebApi.Maintenance.Interfaces
{
    public interface ISearchItemApi : ITypedApi<SearchItemExtDto, string>
    {
        bool Exists(string id);
        IEnumerable<SearchItemExtDto> GetItemsByCondition(SearchItemCondition searchItemCondition);

    }
}