using EasyLOB.Activity;
using EasyLOB.Security;
using System.Web.Mvc;

namespace EasyLOB.Library.Mvc
{
    [Authorize]
    public class BaseControllerDashboard : BaseController
    {
        #region Methods

        protected bool IsAuthorized(string dashboardDirectory, string dashboardName, ZOperationResult operationResult)
        {
            return SecurityManager.IsAuthorized(ActivityHelper.DashboardActivity(Domain, dashboardDirectory, dashboardName), ZSecurityOperations.Execute, operationResult);
        }

        #endregion Methods
    }
}