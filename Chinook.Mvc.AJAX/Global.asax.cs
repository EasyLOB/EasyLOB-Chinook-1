using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EasyLOB;
using EasyLOB.Library.Mvc;

namespace Chinook.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start() // !!!
        {
            AreaRegistration.RegisterAllAreas();

            // Syncfusion Report Viewer
            
            GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Json.NET

            JsonConvert.DefaultSettings = () => AppDefaults.JsonSettings;

            // NHibernate

            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new NHibernateContractResolver();

            // Razor
        
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CustomRazorViewEngine());

            // Unity

            UnityConfig.RegisterMappings();

            // Validation
            // /App_GlobalResources/ValidationResources.*.resx

            ClientDataTypeModelValidatorProvider.ResourceClassKey = "ValidationResources";
            DefaultModelBinder.ResourceClassKey = "ValidationResources";
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["ZCulture"];
            if (cookie != null)
            {
                CultureInfo ci = new CultureInfo(cookie.Value);
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = ci;
            }
        }
    }
}