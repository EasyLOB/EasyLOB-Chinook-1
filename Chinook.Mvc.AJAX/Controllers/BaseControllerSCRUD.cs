using EasyLOB.Activity;
using EasyLOB.Data;
using EasyLOB.Persistence;
using EasyLOB.Security;
using System;
using System.Web.Mvc;

namespace EasyLOB.Library.Mvc
{
    [Authorize]
    public class BaseControllerSCRUD<TEntity> : BaseController
        where TEntity : class, IZDataBase
    {
        #region Properties

        protected virtual string Entity
        {
            get { return ""; }
        }

        protected ZIsSecurityOperations _isSecurityOperations;

        protected ZIsSecurityOperations IsSecurityOperations
        {
            get
            {
                if (_isSecurityOperations == null)
                {
                    _isSecurityOperations = SecurityManager.GetOperations(ActivityHelper.EntityActivity(Domain, Entity));
                }

                return _isSecurityOperations;
            }
        }

        #endregion Properties

        #region Methods

        protected bool ValidateModelState(IGenericRepository<TEntity> repository)
        {
            //ZDataDictionaryAttribute entityDictionary = repository.DataDictionary;
            //if (entityDictionary.IsAuto && ZPersistenceHelper.IsAutoDBMS(repository.DBMS))
            //{
            //    foreach (string key in entityDictionary.Keys)
            //    {
            //        ModelState.Remove(entityDictionary.Entity + "." + key);
            //    }
            //}

            return ModelState.IsValid;
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

        protected bool IsExport(ZOperationResult operationResult)
        {
            return SecurityHelper.IsExport(IsSecurityOperations, operationResult);
        }

        protected bool IsImport(ZOperationResult operationResult)
        {
            return SecurityHelper.IsImport(IsSecurityOperations, operationResult);
        }

        protected bool IsExecute(ZOperationResult operationResult)
        {
            return SecurityHelper.IsExecute(IsSecurityOperations, operationResult);
        }

        #endregion Methods Activity
    }
}