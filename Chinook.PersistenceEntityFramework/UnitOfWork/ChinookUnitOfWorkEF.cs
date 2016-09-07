using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookUnitOfWorkEF : UnitOfWorkEF, IChinookUnitOfWork
    {
        #region Methods

        public ChinookUnitOfWorkEF()
            : base(new ChinookDbContext())
        {
            Domain = "Chinook";

            //ChinookDbContext context = (ChinookDbContext)base.Context;
        }

        public override IGenericRepository<TEntity> GetRepository<TEntity>()
        {
            if (!Repositories.Keys.Contains(typeof(TEntity)))
            {
                var repository = new ChinookGenericRepositoryEF<TEntity>(this);
                Repositories.Add(typeof(TEntity), repository);
            }

            return Repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
        }

        #endregion Methods
    }
}