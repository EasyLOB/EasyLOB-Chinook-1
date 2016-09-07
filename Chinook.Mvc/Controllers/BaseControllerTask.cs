using EasyLOB.Activity;
using EasyLOB.Library.Resources;
using EasyLOB.Security;
using System.Web.Mvc;

namespace EasyLOB.Library.Mvc
{
    [Authorize]
    public class BaseControllerTask : BaseController
    {
        #region Properties

        protected ZOperationResult OperationResult { get; }

        #endregion Properties

        #region Methods

        public BaseControllerTask()
        {
            OperationResult = new ZOperationResult();
        }

        protected bool IsAuthorized(string task, ZOperationResult operationResult)
        {
            return SecurityManager.IsAuthorized(ActivityHelper.TaskActivity(Domain, task), ZSecurityOperations.Execute, operationResult);
        }

        #endregion Methods
    }
}