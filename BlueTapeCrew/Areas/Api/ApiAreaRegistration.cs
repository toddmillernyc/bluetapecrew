using System.Web.Http;
using System.Web.Mvc;

namespace BlueTapeCrew.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "api";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapHttpRoute(
                name: "Api",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}