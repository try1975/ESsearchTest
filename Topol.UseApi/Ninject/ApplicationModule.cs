using Ninject.Modules;
using Topol.UseApi.Controls;
using Topol.UseApi.Data;
using Topol.UseApi.Data.Common;
using Topol.UseApi.Interfaces;
using Topol.UseApi.Interfaces.Common;
using Topol.UseApi.Interfaces.Data;

namespace Topol.UseApi.Ninject
{
    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataMаnager>().To<DataMаnager>().InSingletonScope();

            Bind<ISellerView>().To<SellerControl>().InSingletonScope();
            Bind<ISellerDataManager>().To<SellerDataManager>().InSingletonScope();
            Bind<IGzDocSearchView>().To<GzDocSearchControl>().InSingletonScope();
        }
    }
}