using System.Collections.Generic;
using Common.Dto.Model.Packet;

namespace Price.WebApi.Logic.Packet
{
    public interface ISearchItemStore
    {
        SearchItemDto Get(string aKey);
        int GetInProcessCount();
        IEnumerable<SearchItemDto> TakeCountInQueue(int count);
        SearchItemDto GetOneByKeyInProcess(string aKey);
    }
}