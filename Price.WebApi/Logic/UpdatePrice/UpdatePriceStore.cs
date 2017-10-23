using System;
using System.Collections.Concurrent;
using Price.WebApi.Models.UpdatePrice;

namespace Price.WebApi.Logic.UpdatePrice
{
    /// <summary>
    /// 
    /// </summary>
    public static class UpdatePriceStore
    {
        private static readonly ConcurrentDictionary<int, UpdatePriceDto> Dictionary = new ConcurrentDictionary<int, UpdatePriceDto>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static UpdatePriceDto Get(Uri uri)
        {
            var key = uri.GetHashCode();
            if (Dictionary.ContainsKey(key)) return Dictionary[key];
            var dto = new UpdatePriceDto { Uri = uri, Status = UpdatePriceStatus.NotProcessed };
            Dictionary[key] = dto;
            return dto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        public static void Post(UpdatePriceDto dto)
        {
            if (dto == null) return;
            var key = dto.Uri.GetHashCode();
            Dictionary[key] = dto;
        }
    }
}