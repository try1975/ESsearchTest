namespace Topol.UseApi.Administration
{
    public static class CurrentUser
    {
        public static string Login { get; set; }

        public static string Password { get; set; }

        public static string[] Roles { get; set; }
    }
}