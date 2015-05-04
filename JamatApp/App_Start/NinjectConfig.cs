
using Ninject;
using Jamat.EntityFramework;
using Jamat.DC;

namespace JamatApp.App_Start
{
    public static class NinjectConfig
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            //Create the bindings
            //kernel.Bind<IProductsRepository>().To<ProductRepository>();
            kernel.Bind<DbEntityContext>().To<DbEntityContext>();
            kernel.Bind<ITajneedRepository>().To<TajneedRepository>();
            kernel.Bind<IValidationRepository>().To<ValidationRepository>();
            kernel.Bind<ICountryRepository>().To<CountryRepository>();
            kernel.Bind<IRegionRepository>().To<RegionRepository>();
            kernel.Bind<IJalsaRepository>().To<JalsaRepository>();
            kernel.Bind<IFinanceRepository>().To<FinanceRepository>();

            return kernel;
        }
    }
}