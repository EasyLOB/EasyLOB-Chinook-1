using Chinook.Persistence;
using EasyLOB.Application;
using EasyLOB.AuditTrail;
using EasyLOB.Data;
using EasyLOB.Log;
using EasyLOB.Security;

namespace Chinook.Application
{
    public class ChinookGenericApplicationDTO<TEntityDTO, TEntity> : GenericApplicationDTO<TEntityDTO, TEntity>, IChinookGenericApplicationDTO<TEntityDTO, TEntity>
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
        where TEntity : class, IZDataBase
    {
        #region Methods

        public ChinookGenericApplicationDTO(IChinookUnitOfWork unitOfWork, IAuditTrailManager auditTrailManager, ILogManager logManager, ISecurityManager securityManager)
            : base(unitOfWork, auditTrailManager, logManager, securityManager)
        {
        }

        #endregion Methods
    }
}