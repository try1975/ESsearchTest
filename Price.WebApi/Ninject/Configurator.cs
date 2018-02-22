using System.Data.Entity;
using AutoMapper;
using AutoMapper.Configuration;
using Ninject;
using Ninject.Web.Common;
using Price.Db.Entities.QueryProcessors;
using Price.Db.MysSql;
using Price.Db.MysSql.QueryProcessors;
using Price.WebApi.AutoMappers;
using Price.WebApi.Logic.Interfaces;
using Price.WebApi.Logic.Internet;
using Price.WebApi.Logic.Packet;
using Price.WebApi.Logic.UpdatePrice;
using Price.WebApi.Maintenance.Classes;
using Price.WebApi.Maintenance.Interfaces;

namespace Price.WebApi.Ninject
{
    /// <summary>
    ///     Class used to set up the Ninject DI container.
    /// </summary>
    public class Configurator
    {
        /// <summary>
        ///     Entry method used by caller to configure the given
        ///     container with all of this application's
        ///     dependencies.
        /// </summary>
        public void Configure(IKernel container)
        {
            Logger.InitLogger();
            AddBindings(container);
        }

        private static void AddBindings(IKernel container)
        {
            ConfigureOrm(container);
            ConfigureAutoMapper();

            #region Api and Query

            container.Bind<ISearchItemApi>().To<SearchItemApi>().InRequestScope();
            container.Bind<ISearchItemQuery>().To<SearchItemQuery>().InRequestScope();
            container.Bind<IInternetContentApi>().To<InternetContentApi>().InRequestScope();
            container.Bind<IInternetContentQuery>().To<InternetContentQuery>().InRequestScope();
            container.Bind<IContentApi>().To<ContentApi>().InRequestScope();
            container.Bind<IContentQuery>().To<ContentQuery>().InRequestScope();



            #endregion
        }

        private static void ConfigureAutoMapper()
        {
            var cfg = new MapperConfigurationExpression();
            ContentAutoMapper.Configure(cfg);
            Mapper.Initialize(cfg);
            //Mapper.AssertConfigurationIsValid();
        }

        private static void ConfigureOrm(IKernel container)
        {
            //container.Bind<DbContext>().To<PriceContext>().InSingletonScope();
            container.Bind<DbContext>().To<PriceContext>().InRequestScope();
            //container.Bind<ExchangeServiceMailSender>().ToSelf().InSingletonScope();
            container.Bind<IUpdatePriceWatcher>().To<UpdatePriceWatcher>().InSingletonScope();
            container.Bind<IInternetSearchWatcher>().To<InternetSearchWatcher>().InSingletonScope();
            //container.Bind<IDetect>().To<Detect>().InRequestScope();
            container.Bind<ISearchItemStore>().To<SearchItemStore>().InSingletonScope();
            //container.Bind<ISearchItemStore>().To<SearchItemDbStore>().InSingletonScope();
        }
    }
}