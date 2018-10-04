using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Dto.Model;
using Common.Dto.Model.NewApi;
using Common.Dto.Model.Packet;
using PriceCommon.Model;

namespace Topol.UseApi.Interfaces.Common
{
    public interface IDataMаnager
    {
        Task<List<SearchItemHeaderDto>> PostPacketAsync(List<SearchItemParam> searchItemsParam, string source = "", string keywords = "");
        Task<List<SearchItemHeaderDto>> GetByConditionAsync(SearchItemCondition searchItemCondition);
        Task<SearchItemHeaderDto> GetSearchItemStatus(string id);
        Task<List<ContentExtDto>> GetSearchItemContent(string id);
        Task<bool> PostSearchItemBreak(string id);
        Task<bool> PostSearchItemChecked(string id);
        Task<bool> PostContentItemChecked(string id, string elasticId);
        Task<bool> PostContentItemNotChecked(string id, string elasticId);
        Task<bool> PostContentItemPrice(string id, string elasticId, string price);
        Task<bool> DeleteSearchItem(string id);
        Task<bool> DeleteContentItem(string id, string elasticId);
        Task<IEnumerable<ContentDto>> GetMaybe(string must = "", string should = "", string mustNot = "", string source = "");
        Task<IEnumerable<Okpd2Reverse>> GetOkpd2Reverse(string text);
        Task Post2InternetIndex(IEnumerable<BasicContentDto> list);

        Task<SearchItemHeaderDto> MoveResults(List<ContentMoveDto> list, string id, string name, string extId);
        Task<int> GetSellerCount();
    }
}