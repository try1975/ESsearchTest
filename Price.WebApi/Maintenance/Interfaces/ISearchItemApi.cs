using System.Collections.Generic;
using Common.Dto.Model.NewApi;

namespace Price.WebApi.Maintenance.Interfaces
{
    public interface ISearchItemApi : ITypedApi<SearchItemExtDto, string>
    {
        IEnumerable<SearchItemHeaderDto> GetItemsByCondition(SearchItemCondition searchItemCondition);
        SearchItemHeaderDto GetItemHeader(string id);
        List<ContentExtDto> GetItemContents(string id);
        bool SearchItemBreak(string id);
        bool SearchItemChecked(string id);
        string BaseUrl { get; set; }

        SearchItemHeaderDto MoveContents(IEnumerable<ContentMoveDto> dtos, string id, string name, string extId);
    }
}
