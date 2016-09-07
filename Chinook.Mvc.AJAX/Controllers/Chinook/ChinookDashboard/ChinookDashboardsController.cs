using EasyLOB.Library;
using EasyLOB.Library.Mvc;
using EasyLOB.Library.Syncfusion;
using System;
using System.IO;
using System.Web.Mvc;

namespace Chinook.Mvc
{
    public partial class ChinookDashboardsController : DashboardsController
    {
        #region Properties

        protected override string Domain
        {
            get { return "Chinook"; }
        }

        #endregion Properties

        #region Methods

        [HttpGet]
        public ActionResult AutoRefresh()
        {
            string dashboardDirectory = "Chinook";

            string dashboardName = (string)HttpContext.Session["DashboardSyncfusionAutoRefresh"];
            if (String.IsNullOrEmpty(dashboardName))
            {
                dashboardName = "Dashboard";
            }
            else if (dashboardName == "Dashboard")
            {
                dashboardName = "Dashboard-2";
            }
            else if (dashboardName == "Dashboard-2")
            {
                dashboardName = "Dashboard-3";
            }
            else
            {
                dashboardName = "Dashboard";
            }
            HttpContext.Session["DashboardSyncfusionAutoRefresh"] = dashboardName;

            ViewBag.ReportPath = Path.Combine(Server.MapPath(LibraryHelper.AppSettings<string>("Dashboard.SyncfusionDirectory")), dashboardDirectory, dashboardName + ".sydx")
                .Replace("\\", "\\\\");

            DashboardViewer dashboardViewer = new DashboardViewer();
            ViewBag.Errormessage = dashboardViewer.Errormessage;
            string url = LibraryHelper.AppSettings<string>("Dashboard.SyncfusionUrl");
            ViewBag.ServiceURL = String.IsNullOrEmpty(url) ? dashboardViewer.ServiceUrl : url;

            Response.AddHeader("Refresh", "10");

            return View("DashboardSyncfusion");
        }

        #endregion Methods
    }
}