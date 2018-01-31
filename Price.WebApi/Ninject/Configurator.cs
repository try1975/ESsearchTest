using AutoMapper;
using AutoMapper.Configuration;
using Ninject;
using Price.WebApi.AutoMappers;
using Price.WebApi.Logic.Interfaces;
using Price.WebApi.Logic.Internet;
using Price.WebApi.Logic.Packet;
using Price.WebApi.Logic.UpdatePrice;

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

            //container.Bind<ITransactionApi>().To<TransactionApi>().InRequestScope();
            //container.Bind<ITransactionQuery>().To<TransactionQuery>().InRequestScope();
            

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
            //container.Bind<WalletContext>().ToSelf().InRequestScope();
            //container.Bind<WalletContext>().ToSelf().InThreadScope();
            //container.Bind<ExchangeServiceMailSender>().ToSelf().InSingletonScope();
            container.Bind<IUpdatePriceWatcher>().To<UpdatePriceWatcher>().InSingletonScope();
            container.Bind<IInternetSearchWatcher>().To<InternetSearchWatcher>().InSingletonScope();
            //container.Bind<IDetect>().To<Detect>().InRequestScope();
            container.Bind<ISearchItemStore>().To<SearchItemStore>().InSingletonScope();
        }
    }
}