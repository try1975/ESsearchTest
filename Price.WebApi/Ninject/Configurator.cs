using System.Data.Entity;
using AutoMapper;
using AutoMapper.Configuration;
using FindCompany.Db.Entities.QueryProcessors;
using FindCompany.Db.Postgress;
using FindCompany.Db.Postgress.QueryProcessors;
using Ninject;
using Ninject.Web.Common;
using Price.Db.Entities.QueryProcessors;
using Price.Db.Postgress;
using Price.Db.Postgress.QueryProcessors;
using Price.WebApi.AutoMappers;
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

            container.Bind<IEnricheApi>().To<EnricheApi>().InSingletonScope();
            container.Bind<IFindCompanyQuery>().To<FindCompanyQuery>().InSingletonScope();

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
            container.Bind<DbContext>().To<PriceContext>().WhenInjectedInto<ContentQuery>().InRequestScope();
            container.Bind<DbContext>().To<PriceContext>().WhenInjectedInto<InternetContentQuery>().InRequestScope();
            container.Bind<DbContext>().To<PriceContext>().WhenInjectedInto<SearchItemQuery>().InRequestScope();
            
            container.Bind<DbContext>().To<FindCompanyContext>().WhenInjectedInto<FindCompanyQuery>().InSingletonScope();
            //container.Bind<ExchangeServiceMailSender>().ToSelf().InSingletonScope();
        }
    }
}