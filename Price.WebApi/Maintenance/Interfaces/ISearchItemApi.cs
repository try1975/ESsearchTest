using System.Collections.Generic;
using System.Web.Http;
using Common.Dto.Model.NewApi;

namespace Price.WebApi.Maintenance.Interfaces
{
    public interface ISearchItemApi : ITypedApi<SearchItemExtDto, string>
    {
        IEnumerable<SearchItemExtDto> GetItemsByCondition(SearchItemCondition searchItemCondition);
        SearchItemHeaderDto GetItemHeader(string id);
        List<ContentExtDto> GetItemContents(string id);
        bool SetCompleted(string id);

        string BaseUrl { get; set; }
    }
}
