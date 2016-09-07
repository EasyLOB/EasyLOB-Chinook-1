using Chinook.Persistence;
using EasyLOB.Application;
using EasyLOB.AuditTrail;
using EasyLOB.Data;
using EasyLOB.Log;
using EasyLOB.Security;

namespace Chinook.Application
{
    public class ChinookGenericApplication<TEntity> : GenericApplication<TEntity>, IChinookGenericApplication<TEntity>
        where TEntity : class, IZDataBase
    {     
        #region Methods

        public ChinookGenericApplication(IChinookUnitOfWork unitOfWork, IAuditTrailManager auditTrailManager, ILogManager logManager, ISecurityManager securityManager)
            : base(unitOfWork, auditTrailManager, logManager, securityManager)
        {
        }

        #endregion Methods
    }
}