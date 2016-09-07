using EasyLOB.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookGenericRepositoryNH<TEntity> : GenericRepositoryNH<TEntity>, IChinookGenericRepository<TEntity>
        where TEntity : class, IZDataBase
    {
        #region Methods

        public ChinookGenericRepositoryNH(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            Session = (unitOfWork as ChinookUnitOfWorkNH).Session;
        }

        #endregion Methods
    }
}