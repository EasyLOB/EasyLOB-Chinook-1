using EasyLOB.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookGenericRepositoryRavenDB<TEntity> : GenericRepositoryRavenDB<TEntity>, IChinookGenericRepository<TEntity>
        where TEntity : class, IZDataBase
    {
        #region Methods

        public ChinookGenericRepositoryRavenDB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            DocumentStore = (unitOfWork as ChinookUnitOfWorkRavenDB).DocumentStore;
            DocumentSession = (unitOfWork as ChinookUnitOfWorkRavenDB).DocumentSession;
        }

        #endregion Methods
    }
}