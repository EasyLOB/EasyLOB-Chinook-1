using EasyLOB.Persistence;
using EasyLOB.Library;

namespace Chinook.Persistence
{
    public class ChinookUnitOfWorkRavenDB : UnitOfWorkRavenDB, IChinookUnitOfWork
    {
        #region Methods

        public ChinookUnitOfWorkRavenDB()
            : base(LibraryHelper.AppSettings<string>("RavenDB.Chinook.Url"), LibraryHelper.AppSettings<string>("RavenDB.Chinook.Database"))
        {
            Domain = "Chinook";

            //IRavenDatabase database = base.Database;
        }

        public override IGenericRepository<TEntity> GetRepository<TEntity>()
        {
            if (!Repositories.Keys.Contains(typeof(TEntity)))
            {
                var repository = new ChinookGenericRepositoryRavenDB<TEntity>(this);
                Repositories.Add(typeof(TEntity), repository);
            }

            return Repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
        }

        #endregion Methods
    }
}