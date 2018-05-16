using Ninject.Modules;
using Topol.UseApi.Data.Common;
using Topol.UseApi.Interfaces.Common;

namespace Topol.UseApi.Ninject
{
    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataMаnager>().To<DataMаnager>().InSingletonScope();
        }
    }
}