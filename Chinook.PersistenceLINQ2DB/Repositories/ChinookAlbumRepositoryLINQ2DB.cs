using System.Linq;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookAlbumRepositoryLINQ2DB : ChinookGenericRepositoryLINQ2DB<Album>
    {
        #region Methods

        public ChinookAlbumRepositoryLINQ2DB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override IQueryable<Album> Join(IQueryable<Album> query)
        {
            return
                from album in query
                join artist in UnitOfWork.GetQuery<Artist>() on album.ArtistId equals artist.ArtistId // INNER JOIN
                select new Album
                {
                    AlbumId = album.AlbumId,
                    Title = album.Title,
                    ArtistId = album.ArtistId,
                    Artist = artist
                };
        }

        #endregion Methods
    }
}

