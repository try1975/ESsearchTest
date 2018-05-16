using System;
using System.Windows.Forms;
using log4net.Config;
using Topol.UseApi.Forms;
using Topol.UseApi.Ninject;

[assembly: XmlConfigurator(Watch = true)]

namespace Topol.UseApi
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            CompositionRoot.Wire(new ApplicationModule());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AuthenticationForm());
        }
    }
}
