using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookPlaylistRepositoryMongoDB : ChinookGenericRepositoryMongoDB<Playlist>
    {
        #region Methods

        public ChinookPlaylistRepositoryMongoDB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override void Join(Playlist playlist)
        {
            if (playlist != null)
            {
            }
        }

        #endregion Methods
    }
}
