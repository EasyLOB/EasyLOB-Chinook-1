using EasyLOB.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookGenericRepositoryEF<TEntity> : GenericRepositoryEF<TEntity>, IChinookGenericRepository<TEntity>
        where TEntity : class, IZDataBase
    {
        #region Methods

        public ChinookGenericRepositoryEF(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            Context = (unitOfWork as ChinookUnitOfWorkEF).Context;
        }

        #endregion Methods
    }
}