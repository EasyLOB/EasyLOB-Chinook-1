using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookAlbumRepositoryMongoDB : ChinookGenericRepositoryMongoDB<Album>
    {
        #region Methods

        public ChinookAlbumRepositoryMongoDB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override void Join(Album album)
        {
            if (album != null)
            {
                album.Artist = UnitOfWork.GetRepository<Artist>().GetById(album.ArtistId);
            }
        }

        #endregion Methods
    }
}
