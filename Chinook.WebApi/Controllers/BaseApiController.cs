using EasyLOB.Activity;
using EasyLOB.Data;
using EasyLOB.Persistence;
using EasyLOB.Security;
using System.Web.Http;
using System.Web.Mvc;

namespace EasyLOB.Library.WebApi
{
    public class BaseApiController<TEntity> : ApiController
        where TEntity : class, IZDataBase
    {
        #region Properties

        protected string Entity { get; set; }

        protected ISecurityManager SecurityManager { get; set; }

        protected ZIsSecurityOperations IsSecurityOperations { get; set; }

        #endregion Properties

        #region Methods

        public BaseApiController()
        {
            SecurityManager = DependencyResolver.Current.GetService<ISecurityManager>();
            Entity = typeof(TEntity).Name;
            string activity = ActivityHelper.EntityActivity(AppDefaults.Domain, Entity);
            IsSecurityOperations = SecurityManager.GetOperations(activity);
        }

        protected bool ValidateModelState(ZOperationResult operationResult, IGenericRepository<TEntity> repository)
        {
            bool result = false;

            //ZDataDictionaryAttribute entityDictionary = repository.DataDictionary;
            //if (entityDictionary.IsAuto && ZPersistenceHelper.IsAutoDBMS(repository.DBMS))
            //{
            //    foreach (string key in entityDictionary.Keys)
            //    {
            //        ModelState.Remove(entityDictionary.Entity + "." + key);
            //    }
            //}

            result = ModelState.IsValid;

            ModelState.AddOperationResults(operationResult, typeof(TEntity).Name);

            return result;
        }

        #endregion Methods

        #region Methods IsActivity

        protected bool IsSearch()
        {
            ZOperationResult operationResult = new ZOperationResult();

            return SecurityHelper.IsSearch(IsSecurityOperations, operationResult);
        }

        protected bool IsSearch(ZOperationResult operationResult)
        {
            return SecurityHelper.IsSearch(IsSecurityOperations, operationResult);
        }

        protected bool IsCreate(ZOperationResult operationResult)
        {
            return SecurityHelper.IsCreate(IsSecurityOperations, operationResult);
        }

        protected bool IsRead(ZOperationResult operationResult)
        {
            return SecurityHelper.IsRead(IsSecurityOperations, operationResult);
        }

        protected bool IsUpdate(ZOperationResult operationResult)
        {
            return SecurityHelper.IsUpdate(IsSecurityOperations, operationResult);
        }

        protected bool IsDelete(ZOperationResult operationResult)
        {
            return SecurityHelper.IsDelete(IsSecurityOperations, operationResult);
        }

        protected bool IsExecute(ZOperationResult operationResult)
        {
            return SecurityHelper.IsExecute(IsSecurityOperations, operationResult);
        }

        #endregion Methods IsActivity
    }
}