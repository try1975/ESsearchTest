using System;
using System.Windows.Forms;
using AutoMapper;
using AutoMapper.Configuration;
using log4net.Config;
using Topol.UseApi.Data.Common;
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
            var cfg = new MapperConfigurationExpression();
            AutoMapperConfig.RegisterMappings(cfg);
            Mapper.Initialize(cfg);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AuthenticationForm());
        }
    }
}
