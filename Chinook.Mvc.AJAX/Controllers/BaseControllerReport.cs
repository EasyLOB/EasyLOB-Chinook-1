using EasyLOB.Activity;
using EasyLOB.Security;
using System.Web.Mvc;

namespace EasyLOB.Library.Mvc
{
    [Authorize]
    public class BaseControllerReport : BaseController
    {
        #region Methods

        protected bool IsAuthorized(string reportDirectory, string reportName, ZOperationResult operationResult)
        {
            return SecurityManager.IsAuthorized(ActivityHelper.ReportActivity(Domain, reportDirectory, reportName), ZSecurityOperations.Execute, operationResult);
        }

        #endregion Methods
    }
}