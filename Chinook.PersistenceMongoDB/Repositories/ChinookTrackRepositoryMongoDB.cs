using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookTrackRepositoryMongoDB : ChinookGenericRepositoryMongoDB<Track>
    {
        #region Methods

        public ChinookTrackRepositoryMongoDB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override void Join(Track track)
        {
            if (track != null)
            {
                track.Album = UnitOfWork.GetRepository<Album>().GetById(track.AlbumId);
                track.Genre = UnitOfWork.GetRepository<Genre>().GetById(track.GenreId);
                track.MediaType = UnitOfWork.GetRepository<MediaType>().GetById(track.MediaTypeId);
            }
        }

        #endregion Methods
    }
}
