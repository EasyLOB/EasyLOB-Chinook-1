using System.Web;
using System.Web.Mvc;
using EasyLOB.Library.Mvc;

namespace Chinook.Mvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) // !!!
        {
            filters.Add(new HandleErrorAttribute());

            // Filters
            
            filters.Add(new ValidateModelStateAttribute());
        }
    }
}
