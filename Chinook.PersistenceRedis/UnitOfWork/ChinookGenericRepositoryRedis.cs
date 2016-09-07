using EasyLOB.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookGenericRepositoryRedis<TEntity> : GenericRepositoryRedis<TEntity>, IChinookGenericRepository<TEntity>
        where TEntity : class, IZDataBase
    {
        #region Methods

        public ChinookGenericRepositoryRedis(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            Client = (unitOfWork as ChinookUnitOfWorkRedis).Client;
        }

        #endregion Methods
    }
}
