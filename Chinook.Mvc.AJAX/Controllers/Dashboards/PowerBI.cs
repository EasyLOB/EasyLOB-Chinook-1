using System.Web.Mvc;

namespace EasyLOB.Library.Mvc
{
    public partial class DashboardsController : BaseControllerDashboard
    {
        [HttpGet]
        public ActionResult PowerBI(string dashboardId)
        {
            ViewBag.Url = "https://app.powerbi.com/view?r=" + dashboardId;
            return View("PowerBI");
        }
    }
}
