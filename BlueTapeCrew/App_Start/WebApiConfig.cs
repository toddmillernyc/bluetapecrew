using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace BlueTapeCrew
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config
            var container = new UnityContainer();
            UnityConfig.RegisterTypes(container);
            config.DependencyResolver = new UnityDependencyResolver(container);

            // routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
