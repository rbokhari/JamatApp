using JamatApp.App_Start;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using WebApiContrib.IoC.Ninject;

[assembly: OwinStartup(typeof(JamatApp.Startup))]
namespace JamatApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.DependencyResolver = new NinjectResolver(NinjectConfig.CreateKernel());

            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

        }
    }
}