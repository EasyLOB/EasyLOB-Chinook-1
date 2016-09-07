using System.Web.Mvc;

namespace Chinook.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new ValidateModelStateAttribute());
        }
    }
}
