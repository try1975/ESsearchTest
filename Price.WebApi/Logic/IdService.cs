using System;

namespace Price.WebApi.Logic
{
    public static class IdService
    {
        private static string GenerateGuidId()
        {
            return Guid.NewGuid().ToString("N");
        }

        private static string GenerateIdByTime()
        {
            return $"{(int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds}";
        }

        public static string GenerateId()
        {
            return $"{GenerateIdByTime()}_{GenerateGuidId()}";
        }
    }
}