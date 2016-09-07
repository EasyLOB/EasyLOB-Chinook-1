using System.Web.OData;
using EasyLOB.Data;
using EasyLOB.Security;

namespace EasyLOB.Library.WebApi
{
    public class BaseODataController<TEntity> : ODataController
        where TEntity : class, IZDataBase
    {
        #region Properties

        protected string Entity { get; set; }

        protected ZIsSecurityOperations IsSecurityOperations { get; set; }

        protected ZOperationResult OperationResult;

        #endregion Properties

        #region Methods

        public BaseODataController(string entity)
        {
            Entity = entity;
            //IsSecurityOperations = ActivityManager.GetOperations(ZActivityHelper.EntityActivity(Entity)); // ???
            IsSecurityOperations = new ZIsSecurityOperations();
            OperationResult = new ZOperationResult();
        }

        protected bool IsSearch(ZOperationResult operationResult)
        {
            bool result = true;

            //if (!IsSecurityOperations.IsSearch)
            //{
            //    result = false;
            //    operationResult.AddOperationError("", ZActivityHelper.MessageNonAuthorized(Entity, ZSecurityOperations.Search));
            //}

            return result;
        }

        protected bool IsCreate(ZOperationResult operationResult)
        {
            bool result = true;

            //if (!IsSecurityOperations.IsCreate)
            //{
            //    result = false;
            //    operationResult.AddOperationError("", ZActivityHelper.MessageNonAuthorized(Entity, ZSecurityOperations.Create));
            //}

            return result;
        }

        protected bool IsRead(ZOperationResult operationResult)
        {
            bool result = true;

            //if (!IsSecurityOperations.IsRead)
            //{
            //    result = false;
            //    operationResult.AddOperationError("", ZActivityHelper.MessageNonAuthorized(Entity, ZSecurityOperations.Read));
            //}

            return result;
        }

        protected bool IsUpdate(ZOperationResult operationResult)
        {
            bool result = true;

            //if (!IsSecurityOperations.IsUpdate)
            //{
            //    result = false;
            //    operationResult.AddOperationError("", ZActivityHelper.MessageNonAuthorized(Entity, ZSecurityOperations.Update));
            //}

            return result;
        }

        protected bool IsDelete(ZOperationResult operationResult)
        {
            bool result = true;

            //if (!IsSecurityOperations.IsDelete)
            //{
            //    result = false;
            //    operationResult.AddOperationError("", ZActivityHelper.MessageNonAuthorized(Entity, ZSecurityOperations.Delete));
            //}

            return result;
        }

        protected bool ValidateModelState(ZOperationResult operationResult)
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
    }
}