using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookAlbumRepositoryRedis : ChinookGenericRepositoryRedis<Album>
    {
        #region Methods

        public ChinookAlbumRepositoryRedis(IUnitOfWork unitOfWork)
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
