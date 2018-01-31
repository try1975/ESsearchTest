using Common.Dto.Model.Packet;

namespace Common.Dto
{
    public interface ISearchPacketTaskStore
    {
        SearchPacketTaskDto Get(string aKey);
        void Post(SearchPacketTaskDto dto);
    }
}