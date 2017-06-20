using System;
using System.Collections.Concurrent;
using Price.WebApi.Models;

namespace Price.WebApi.Logic
{
    public static class UpdatePrices
    {
        private static readonly ConcurrentDictionary<int, UpdatePriceDto> Dictionary;

        static UpdatePrices()
        {
            Dictionary = new ConcurrentDictionary<int, UpdatePriceDto>();
        }

        public static UpdatePriceDto Get(Uri uri)
        {
            var key = uri.GetHashCode();
            if (Dictionary.ContainsKey(key)) return Dictionary[key];
            var dto = new UpdatePriceDto { Uri = uri, Status = UpdatePriceStatus.NotProcessed };
            Dictionary[key] = dto;
            return dto;
        }

        public static void Post(UpdatePriceDto dto)
        {
            if (dto == null) return;
            var key = dto.Uri.GetHashCode();
            Dictionary[key] = dto;
        }
    }
}