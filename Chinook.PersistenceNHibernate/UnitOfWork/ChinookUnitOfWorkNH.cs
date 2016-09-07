using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookUnitOfWorkNH : UnitOfWorkNH, IChinookUnitOfWork
    {
        #region Methods

        public ChinookUnitOfWorkNH()
            : base(ChinookFactory.Session)
        {
            Domain = "Chinook";

            Repositories.Add(typeof(PlaylistTrack), new ChinookPlaylistTrackRepositoryNH(this));

            //ISession session = base.Session;
        }

        public override IGenericRepository<TEntity> GetRepository<TEntity>()
        {
            if (!Repositories.Keys.Contains(typeof(TEntity)))
            {
                var repository = new ChinookGenericRepositoryNH<TEntity>(this);
                Repositories.Add(typeof(TEntity), repository);
            }

            return Repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
        }

        #endregion Methods
    }
}