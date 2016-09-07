using EasyLOB.Data;
using EasyLOB.Library.Resources;
using System;
using System.Web.Mvc;

// Syncfusion Report Viewer
// http://help.syncfusion.com/aspnetmvc/reportviewer/getting-started
// http://help.syncfusion.com/aspnetmvc/reportviewer/report-controller

namespace EasyLOB.Library.Mvc
{
    public partial class ReportsController : BaseControllerReport
    {
        [HttpGet]
        public ActionResult RDL(string reportDirectory, string reportName)
        {
            OperationResultModel operationResultModel = new OperationResultModel();

            if (String.IsNullOrEmpty(reportDirectory) || String.IsNullOrEmpty(reportName))
            {
                operationResultModel.OperationResult.ErrorMessage = ErrorResources.RDL_Parameters;

                return View("OperationResult", operationResultModel);
            }
            else
            {
                if (!IsAuthorized(reportDirectory, reportName, operationResultModel.OperationResult))
                {
                    return View("OperationResult", operationResultModel);
                }
                else
                {
                    ReportModelRDL reportModel = new ReportModelRDL();

                    reportModel.ReportDirectory = reportDirectory;
                    reportModel.ReportName = reportName;

                    return View("RDL", reportModel);
                }
            }
        }
    }
}