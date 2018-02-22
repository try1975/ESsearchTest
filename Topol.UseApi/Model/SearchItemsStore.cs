using System.Collections.Concurrent;
using Common.Dto.Model.NewApi;

namespace Topol.UseApi.Model
{
    /// <summary>
    /// 
    /// </summary>
    public static class SearchItemsStore 
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly ConcurrentDictionary<string, SearchItemHeaderDto> Dictionary;

        static SearchItemsStore()
        {
            Dictionary = new ConcurrentDictionary<string, SearchItemHeaderDto>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public static SearchItemHeaderDto Get(string aKey)
        {
            return !Dictionary.ContainsKey(aKey) ? null : Dictionary[aKey];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        public static void Post(SearchItemHeaderDto dto)
        {
            if (dto == null) return;
            var key = dto.Id;
            Dictionary[key] = dto;
        }
    }
}