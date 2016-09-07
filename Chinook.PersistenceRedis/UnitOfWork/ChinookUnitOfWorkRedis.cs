using ServiceStack;
using Chinook.Data;
using EasyLOB.Persistence;
using EasyLOB.Library;

namespace Chinook.Persistence
{
    public class ChinookUnitOfWorkRedis : UnitOfWorkRedis, IChinookUnitOfWork
    {
        #region Methods

        public ChinookUnitOfWorkRedis()
            : base(LibraryHelper.AppSettings<string>("Redis.Chinook"))
        {
            Domain = "Chinook";

            ModelConfig<Album>.Id(x => x.AlbumId);
            ModelConfig<Artist>.Id(x => x.ArtistId);
            ModelConfig<Customer>.Id(x => x.CustomerId);
            ModelConfig<CustomerDocument>.Id(x => x.CustomerDocumentId);
            ModelConfig<Employee>.Id(x => x.EmployeeId);
            ModelConfig<Genre>.Id(x => x.GenreId);
            ModelConfig<Invoice>.Id(x => x.InvoiceId);
            ModelConfig<InvoiceLine>.Id(x => x.InvoiceLineId);
            ModelConfig<MediaType>.Id(x => x.MediaTypeId);
            ModelConfig<Playlist>.Id(x => x.PlaylistId);
            ModelConfig<PlaylistTrack>.Id(x => x.PlaylistTrackId); // !!!
            ModelConfig<Track>.Id(x => x.TrackId);

            Repositories.Add(typeof(Album), new ChinookAlbumRepositoryRedis(this));
            Repositories.Add(typeof(Artist), new ChinookArtistRepositoryRedis(this));
            Repositories.Add(typeof(Customer), new ChinookCustomerRepositoryRedis(this));
            Repositories.Add(typeof(CustomerDocument), new ChinookCustomerDocumentRepositoryRedis(this));
            Repositories.Add(typeof(Employee), new ChinookEmployeeRepositoryRedis(this));
            Repositories.Add(typeof(Genre), new ChinookGenreRepositoryRedis(this));
            Repositories.Add(typeof(Invoice), new ChinookInvoiceRepositoryRedis(this));
            Repositories.Add(typeof(InvoiceLine), new ChinookInvoiceLineRepositoryRedis(this));
            Repositories.Add(typeof(MediaType), new ChinookMediaTypeRepositoryRedis(this));
            Repositories.Add(typeof(Playlist), new ChinookPlaylistRepositoryRedis(this));
            Repositories.Add(typeof(PlaylistTrack), new ChinookPlaylistTrackRepositoryRedis(this));
            Repositories.Add(typeof(Track), new ChinookTrackRepositoryRedis(this));

            //IRedisClient client = base.Client;
        }

        public override IGenericRepository<TEntity> GetRepository<TEntity>()
        {
            if (!Repositories.Keys.Contains(typeof(TEntity)))
            {
                var repository = new ChinookGenericRepositoryRedis<TEntity>(this);
                Repositories.Add(typeof(TEntity), repository);
            }

            return Repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
        }

        #endregion Methods
    }
}
