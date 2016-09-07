using Chinook.Data;
using EasyLOB.Persistence;
using EasyLOB.Library;

namespace Chinook.Persistence
{
    public class ChinookUnitOfWorkMongoDB : UnitOfWorkMongoDB, IChinookUnitOfWork
    {
        #region Methods

        public ChinookUnitOfWorkMongoDB()
            : base(LibraryHelper.AppSettings<string>("MongoDB.Chinook.Url"), LibraryHelper.AppSettings<string>("MongoDB.Chinook.Database"))
        {
            Domain = "Chinook";

            ChinookMongoDBMap.AlbumMap();
            ChinookMongoDBMap.ArtistMap();
            ChinookMongoDBMap.CustomerMap();
            ChinookMongoDBMap.CustomerDocumentMap();
            ChinookMongoDBMap.EmployeeMap();
            ChinookMongoDBMap.GenreMap();
            ChinookMongoDBMap.InvoiceMap();
            ChinookMongoDBMap.InvoiceLineMap();
            ChinookMongoDBMap.MediaTypeMap();
            ChinookMongoDBMap.PlaylistMap();
            ChinookMongoDBMap.PlaylistTrackMap();
            ChinookMongoDBMap.TrackMap();

            Repositories.Add(typeof(Album), new ChinookAlbumRepositoryMongoDB(this));
            Repositories.Add(typeof(Artist), new ChinookArtistRepositoryMongoDB(this));
            Repositories.Add(typeof(Customer), new ChinookCustomerRepositoryMongoDB(this));
            Repositories.Add(typeof(CustomerDocument), new ChinookCustomerDocumentRepositoryMongoDB(this));
            Repositories.Add(typeof(Employee), new ChinookEmployeeRepositoryMongoDB(this));
            Repositories.Add(typeof(Genre), new ChinookGenreRepositoryMongoDB(this));
            Repositories.Add(typeof(Invoice), new ChinookInvoiceRepositoryMongoDB(this));
            Repositories.Add(typeof(InvoiceLine), new ChinookInvoiceLineRepositoryMongoDB(this));
            Repositories.Add(typeof(MediaType), new ChinookMediaTypeRepositoryMongoDB(this));
            Repositories.Add(typeof(Playlist), new ChinookPlaylistRepositoryMongoDB(this));
            Repositories.Add(typeof(PlaylistTrack), new ChinookPlaylistTrackRepositoryMongoDB(this));
            Repositories.Add(typeof(Track), new ChinookTrackRepositoryMongoDB(this));

            //IMongoDatabase database = base.Database;
        }

        public override IGenericRepository<TEntity> GetRepository<TEntity>()
        {
            if (!Repositories.Keys.Contains(typeof(TEntity)))
            {
                var repository = new ChinookGenericRepositoryMongoDB<TEntity>(this);
                Repositories.Add(typeof(TEntity), repository);
            }

            return Repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
        }

        #endregion Methods
    }
}