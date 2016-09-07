using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookUnitOfWorkLINQ2DB : UnitOfWorkLINQ2DB, IChinookUnitOfWork
    {
        #region Methods

        public ChinookUnitOfWorkLINQ2DB()
            : base(new ChinookLINQ2DB())
        {
            Domain = "Chinook";

            Repositories.Add(typeof(Album), new ChinookAlbumRepositoryLINQ2DB(this));
            Repositories.Add(typeof(Artist), new ChinookArtistRepositoryLINQ2DB(this));
            Repositories.Add(typeof(Customer), new ChinookCustomerRepositoryLINQ2DB(this));
            Repositories.Add(typeof(CustomerDocument), new ChinookCustomerDocumentRepositoryLINQ2DB(this));
            Repositories.Add(typeof(Employee), new ChinookEmployeeRepositoryLINQ2DB(this));
            Repositories.Add(typeof(Genre), new ChinookGenreRepositoryLINQ2DB(this));
            Repositories.Add(typeof(Invoice), new ChinookInvoiceRepositoryLINQ2DB(this));
            Repositories.Add(typeof(InvoiceLine), new ChinookInvoiceLineRepositoryLINQ2DB(this));
            Repositories.Add(typeof(MediaType), new ChinookMediaTypeRepositoryLINQ2DB(this));
            Repositories.Add(typeof(Playlist), new ChinookPlaylistRepositoryLINQ2DB(this));
            Repositories.Add(typeof(PlaylistTrack), new ChinookPlaylistTrackRepositoryLINQ2DB(this));
            Repositories.Add(typeof(Track), new ChinookTrackRepositoryLINQ2DB(this));

            //ChinookLINQ2DB connection = (ChinookLINQ2DB)base.Connection;
        }

        public override IGenericRepository<TEntity> GetRepository<TEntity>()
        {
            if (!Repositories.Keys.Contains(typeof(TEntity)))
            {
                var repository = new ChinookGenericRepositoryLINQ2DB<TEntity>(this);
                Repositories.Add(typeof(TEntity), repository);
            }

            return Repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
        }

        #endregion Methods
    }
}