using System.Configuration;

namespace PricePipeCore
{
    public static class AppSettings
    {
        static AppSettings()
        {
            DefaultIndex = ConfigurationManager.AppSettings[nameof(DefaultIndex)];
            Host = ConfigurationManager.AppSettings[nameof(Host)];
            UserName = ConfigurationManager.AppSettings[nameof(UserName)];
            Password = ConfigurationManager.AppSettings[nameof(Password)];
        }

        public static string DefaultIndex { get; }
        public static string Host { get; }
        public static string UserName { get; }
        public static string Password { get; }
    }
}