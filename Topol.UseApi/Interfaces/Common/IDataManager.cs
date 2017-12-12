using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Dto.Model;
using Common.Dto.Model.Packet;
using PriceCommon.Model;

namespace Topol.UseApi.Interfaces.Common
{
    public interface IDataMаnager
    {
        #region SearchPacket

        Task<SearchPacketTaskDto> PostPacketAsync(List<SearchItemParam> searchItemsParam, string source = "");
        Task<SearchPacketTaskDto> GetPacketStatus(string id, string source = "");

        Task<IEnumerable<ContentDto>> GetMaybe(string must = "", string should = "", string mustNot = "", string source = "");

        Task<IEnumerable<Okpd2Reverse>> GetOkpd2Reverse(string text);

        Task Post2InternetIndex(IEnumerable<BasicContentDto> list);

        #endregion //SearchPacket

    }
}