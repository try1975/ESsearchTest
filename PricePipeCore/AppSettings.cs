using System.Configuration;

namespace PricePipeCore
{
    public static class AppSettings
    {
        public static string DefaultIndex { get; }

        public static string Md5Host { get; }
        public static string Md5UserName { get; }
        public static string Md5Password { get; }

        public static string GzHost { get; }
        public static string GzUserName { get; }
        public static string GzPassword { get; }

        public static string OtherHost { get; }
        public static string OtherUserName { get; }
        public static string OtherPassword { get; }

        public static string UpdatePriceApi { get; }
        public static string ExternalToken { get; }

        public static int MaxResultCount{ get; }

        static AppSettings()
        {
            DefaultIndex = ConfigurationManager.AppSettings[nameof(DefaultIndex)];

            Md5Host = ConfigurationManager.AppSettings[nameof(Md5Host)];
            Md5UserName = ConfigurationManager.AppSettings[nameof(Md5UserName)];
            Md5Password = ConfigurationManager.AppSettings[nameof(Md5Password)];

            GzHost = ConfigurationManager.AppSettings[nameof(GzHost)];
            GzUserName = ConfigurationManager.AppSettings[nameof(GzUserName)];
            GzPassword = ConfigurationManager.AppSettings[nameof(GzPassword)];

            OtherHost = ConfigurationManager.AppSettings[nameof(OtherHost)];
            OtherUserName = ConfigurationManager.AppSettings[nameof(OtherUserName)];
            OtherPassword = ConfigurationManager.AppSettings[nameof(OtherPassword)];

            UpdatePriceApi = ConfigurationManager.AppSettings[nameof(UpdatePriceApi)];
            ExternalToken = ConfigurationManager.AppSettings[nameof(ExternalToken)];

            MaxResultCount = int.TryParse(ConfigurationManager.AppSettings[nameof(MaxResultCount)], out var maxResultCount) ? maxResultCount : 1000;
        }
    }
}