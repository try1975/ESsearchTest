using System.Collections.Concurrent;
using Common.Dto.Model.Packet;

namespace Price.WebApi.Logic.Packet
{
    /// <summary>
    /// 
    /// </summary>
    public static class SearchItemStore
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly ConcurrentDictionary<int, SearchItemDto> Dictionary= new ConcurrentDictionary<int, SearchItemDto>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public static SearchItemDto Get(string aKey)
        {
            var key = aKey.GetHashCode();
            if (Dictionary.ContainsKey(key)) return Dictionary[key];
            var dto = new SearchItemDto();
            Dictionary[key] = dto;
            return dto;
        }
    }
}