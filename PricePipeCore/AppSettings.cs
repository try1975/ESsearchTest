using System;
using System.Configuration;

namespace PricePipeCore
{
    public static class AppSettings
    {
        public static string DefaultIndex { get; }
        public static string Host { get; }
        public static string UserName { get; }
        public static string Password { get; }

        public static string UpdatePriceApi { get; }
        public static string ExternalToken { get; }

        public static int MaxResultCount{ get; }

        static AppSettings()
        {
            DefaultIndex = ConfigurationManager.AppSettings[nameof(DefaultIndex)];
            Host = ConfigurationManager.AppSettings[nameof(Host)];
            UserName = ConfigurationManager.AppSettings[nameof(UserName)];
            Password = ConfigurationManager.AppSettings[nameof(Password)];
            UpdatePriceApi = ConfigurationManager.AppSettings[nameof(UpdatePriceApi)];
            ExternalToken = ConfigurationManager.AppSettings[nameof(ExternalToken)];

            int maxResultCount;
            int.TryParse(ConfigurationManager.AppSettings[nameof(MaxResultCount)], out maxResultCount);
            MaxResultCount = maxResultCount;
        }
    }
}