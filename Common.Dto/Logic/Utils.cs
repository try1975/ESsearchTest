using System;

namespace Common.Dto.Logic
{
    /// <summary>
    /// 
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static long GetUtcNow()
        {
            return (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}