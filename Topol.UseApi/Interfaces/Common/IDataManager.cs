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
        #region SearchPacket

        Task<List<SearchItemHeaderDto>> PostPacketAsync(List<SearchItemParam> searchItemsParam, string source = "", string keywords = "");
        Task<SearchItemHeaderDto> GetSearchItemStatus(string id);
        Task<List<ContentExtDto>> GetSearchItemContent(string id);

        Task<IEnumerable<ContentDto>> GetMaybe(string must = "", string should = "", string mustNot = "", string source = "");

        Task<IEnumerable<Okpd2Reverse>> GetOkpd2Reverse(string text);

        Task Post2InternetIndex(IEnumerable<BasicContentDto> list);

        #endregion //SearchPacket

    }
}