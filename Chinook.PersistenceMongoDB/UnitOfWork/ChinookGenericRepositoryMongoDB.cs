using EasyLOB.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookGenericRepositoryMongoDB<TEntity> : GenericRepositoryMongoDB<TEntity>, IChinookGenericRepository<TEntity>
        where TEntity : class, IZDataBase
    {
        #region Methods

        public ChinookGenericRepositoryMongoDB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            Database = (unitOfWork as ChinookUnitOfWorkMongoDB).Database;
        }

        #endregion Methods
    }
}