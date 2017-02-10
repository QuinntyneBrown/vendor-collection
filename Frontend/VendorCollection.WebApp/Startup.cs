using Owin;
using System.Web.Http;
using Microsoft.Owin;
using Unity.WebApi;
using static VendorCollection.ApiConfiguration;

[assembly: OwinStartup(typeof(VendorCollection.WebApp.Startup))]

namespace VendorCollection.WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configure(config =>
            {
                config.DependencyResolver = new UnityDependencyResolver(UnityConfiguration.GetContainer());
                Install(config, app);
            });
        }
    }
}
