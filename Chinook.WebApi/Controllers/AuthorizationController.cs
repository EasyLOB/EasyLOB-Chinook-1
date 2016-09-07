using EasyLOB;
using EasyLOB.Activity;
using EasyLOB.Library;
using EasyLOB.Library.WebApi;
using EasyLOB.Security;
using System.Web.Mvc;

namespace Chinook.WebApi
{
    public class AuthorizationController : System.Web.Http.ApiController
    {
        #region Methods

        [Authorize]
        [HttpGet]
        [Route("api/Authorize")]
        public string Authorize()
        {
            return "Authorized";
        }

        [HttpGet]
        [Route("api/AuthorizeActivity")]
        public System.Web.Http.IHttpActionResult AuthorizeActivity()
        {
            ZOperationResult operationResult = new ZOperationResult();

            ISecurityManager securityManager = System.Web.Mvc.DependencyResolver.Current.GetService<ISecurityManager>();

            string activity = ActivityHelper.TaskActivity(AppDefaults.Domain, "AuthorizeActivity");
            if (!securityManager.IsAuthorized(activity, ZSecurityOperations.Execute))
            {
                operationResult.AddOperationError("", SecurityHelper.MessageNotAuthorized(activity, ZSecurityOperations.Execute));
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        #endregion Methods
    }
}