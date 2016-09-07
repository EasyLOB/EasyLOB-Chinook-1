using EasyLOB.Library.Syncfusion;
using EasyLOB.Security;
using EasyLOB.Security.Application;
using Syncfusion.EJ.Export;
using Syncfusion.JavaScript.Models;
using Syncfusion.XlsIO;
using System.Collections;
using System.Net;
using System.Web.Mvc;

// Newtonsoft.JsonResult
// https://github.com/kemmis/Newtonsoft.JsonResult

/*
BaseController
    BaseControllerDashboard
    BaseControllerReport
    BaseControllerSCRUD
        BaseControllerSCRUDApplication
        BaseControllerSCRUDPersistence
    BaseControllerTask
 */

namespace EasyLOB.Library.Mvc
{
    [Authorize]
    public class BaseController : Controller
    {
        #region Properties

        protected virtual string Domain
        {
            get { return ""; }
        }

        protected ISecurityManager SecurityManager { get; }

        #endregion

        #region Methods

        public BaseController()
        {
            SecurityManager = DependencyResolver.Current.GetService<ISecurityManager>();
        }

        protected bool ValidateModelState()
        {
            return ModelState.IsValid;
        }

        #endregion Methods

        #region Methods Controller

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new Newtonsoft.JsonResult.JsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        #endregion Methods Controller

        #region Methods JsonResult

        protected JsonResult JsonResultFailure()
        {
            return JsonResultFailure(null);
        }

        protected JsonResult JsonResultFailure(object data)
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            if (data != null)
            {
                return new JsonResult()
                {
                    Data = data,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        protected JsonResult JsonResultOperationResult(ZOperationResult operationResult)
        {
            if (operationResult.Ok)
            {
                return JsonResultSuccess(new { OperationResult = operationResult });
            }
            else
            {
                return JsonResultFailure(new { OperationResult = operationResult });
            }
        }

        protected JsonResult JsonResultSuccess()
        {
            return JsonResultSuccess(null);
        }

        protected JsonResult JsonResultSuccess(object data)
        {
            Response.StatusCode = (int)HttpStatusCode.OK;
            if (data != null)
            {
                return new JsonResult()
                {
                    Data = data,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        #endregion Methods JsonResult

        #region Methods Syncfusion

        protected void ExportToExcel(string gridModel, IEnumerable data, string theme, string fileName)
        {
            GridProperties gridProperties = SyncfusionGrid.ModelToObject(gridModel);

            ExcelExport export = new ExcelExport();
            IWorkbook excel = export.Export(gridProperties, data, fileName, ExcelVersion.Excel2013, false, false, theme, true);
            excel.ActiveSheet.DeleteRow(1, 1);
            excel.SaveAs(fileName, ExcelSaveType.SaveAsXLS, System.Web.HttpContext.Current.Response, ExcelDownloadType.Open);
            //excel.SaveAs(fileName, ExcelSaveType.SaveAsXLS, Controller.Response, ExcelDownloadType.Open);
        }

        #endregion Methods Syncfusion

        #region Methods URL Dictionary

        protected void ClearUrlDictionary()
        {
            HttpContext.Session[MvcDefaults.UrlDictionarySessionName] = null;
        }

        protected void WriteUrlDictionary(string uri = null)
        {
            MvcHelper.WriteUrlDictionary(HttpContext, ControllerContext.RouteData.Values["controller"].ToString(), uri);
        }

        protected string ReadUrlDictionary()
        {
            return MvcHelper.ReadUrlDictionary(HttpContext, ControllerContext.RouteData.Values["controller"].ToString());
        }

        protected ActionResult RedirectToUrlDictionary()
        {
            return Redirect(ReadUrlDictionary());
        }

        #endregion Methods URL Dictionary
    }
}