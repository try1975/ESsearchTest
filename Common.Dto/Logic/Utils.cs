using System;
using System.Reflection;

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

        public static DateTime? UnixTimeStampToDateTime(long unixTimeStamp)
        {
            if (unixTimeStamp == 0) return null;
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static string GetDescription(Enum en)

        {

            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)

            {

                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumLocalizeAttribite),
                    false);

                if (attrs != null && attrs.Length > 0)

                    return ((EnumLocalizeAttribite)attrs[0]).Text;

            }

            return en.ToString();

        }
    }
}