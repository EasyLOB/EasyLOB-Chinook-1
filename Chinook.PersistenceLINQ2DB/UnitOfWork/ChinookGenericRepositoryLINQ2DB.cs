using EasyLOB.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookGenericRepositoryLINQ2DB<TEntity> : GenericRepositoryLINQ2DB<TEntity>, IChinookGenericRepository<TEntity>
        where TEntity : class, IZDataBase
    {
        #region Methods

        public ChinookGenericRepositoryLINQ2DB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            Connection = (unitOfWork as ChinookUnitOfWorkLINQ2DB).Connection;
        }

        #endregion Methods
    }
}