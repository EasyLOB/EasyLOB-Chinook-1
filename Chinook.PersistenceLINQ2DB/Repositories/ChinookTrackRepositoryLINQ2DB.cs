using System.Linq;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookTrackRepositoryLINQ2DB : ChinookGenericRepositoryLINQ2DB<Track>
    {
        #region Methods

        public ChinookTrackRepositoryLINQ2DB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override IQueryable<Track> Join(IQueryable<Track> query)
        {
            return
                from track in query
                from album in UnitOfWork.GetQuery<Album>().Where(x => x.AlbumId == track.AlbumId).DefaultIfEmpty() // LEFT JOIN
                from genre in UnitOfWork.GetQuery<Genre>().Where(x => x.GenreId == track.GenreId).DefaultIfEmpty() // LEFT JOIN
                join mediaType in UnitOfWork.GetQuery<MediaType>() on track.MediaTypeId equals mediaType.MediaTypeId // INNER JOIN
                select new Track
                {
                    TrackId = track.TrackId,
                    Name = track.Name,
                    AlbumId = track.AlbumId,
                    MediaTypeId = track.MediaTypeId,
                    GenreId = track.GenreId,
                    Composer = track.Composer,
                    Milliseconds = track.Milliseconds,
                    Bytes = track.Bytes,
                    UnitPrice = track.UnitPrice,
                    Album = album,
                    Genre = genre,
                    MediaType = mediaType
                };
        }

        #endregion Methods
    }
}

