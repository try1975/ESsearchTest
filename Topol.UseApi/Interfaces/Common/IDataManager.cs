using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Dto.Model.Packet;

namespace Topol.UseApi.Interfaces.Common
{
    public interface IDataMаnager
    {
        #region SearchPacket

        Task<SearchPacketTaskDto> PostPacket2(List<SearchItemParam> searchItemsParam, string source = "");

        #endregion //SearchPacket

    }
}