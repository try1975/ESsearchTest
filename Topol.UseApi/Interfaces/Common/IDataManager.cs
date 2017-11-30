using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Dto.Model;
using Common.Dto.Model.Packet;

namespace Topol.UseApi.Interfaces.Common
{
    public interface IDataMаnager
    {
        #region SearchPacket

        Task<SearchPacketTaskDto> PostPacket2(List<SearchItemParam> searchItemsParam, string source = "");
        Task<SearchPacketTaskDto> GetPacketStatus(string id, string source = "");

        Task<IEnumerable<ContentDto>> GetMaybe(string must = "", string should = "", string mustNot = "", string source = "");

        #endregion //SearchPacket

    }
}