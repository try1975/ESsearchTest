using System.Collections.Concurrent;
using Common.Dto.Model.Packet;

namespace Common.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public static class SearchPacketTaskStore
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly ConcurrentDictionary<string, SearchPacketTaskDto> Dictionary= new ConcurrentDictionary<string, SearchPacketTaskDto>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public static SearchPacketTaskDto Get(string aKey)
        {
            return !Dictionary.ContainsKey(aKey) ? null : Dictionary[aKey];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        public static void Post(SearchPacketTaskDto dto)
        {
            if (dto == null) return;
            var key = dto.Id;
            Dictionary[key] = dto;
        }
    }
}