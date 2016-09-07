using System.Web.Mvc;

namespace Chinook.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult APIIndex()
        {
            ViewBag.Title = "API Index";

            return View();
        }
    }
}