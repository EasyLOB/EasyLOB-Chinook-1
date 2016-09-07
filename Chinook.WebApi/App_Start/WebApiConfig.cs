using EasyLOB;
using Microsoft.Practices.Unity;
using System.Web.Http;

// Install-Package CommonServiceLocator

namespace Chinook.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config) // !!!
        {
            // Web API configuration and services

            // Web API routes

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Unity

            var container = new UnityContainer();

            UnityHelper.RegisterMappings(container);

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}