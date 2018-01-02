using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace BlueTapeCrew
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            UnityConfig.RegisterTypes(container);
            config.DependencyResolver = new UnityDependencyResolver(container);

            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);
        }
    }
}
