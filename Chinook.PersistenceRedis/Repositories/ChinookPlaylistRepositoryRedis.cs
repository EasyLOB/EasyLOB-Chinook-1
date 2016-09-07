using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookPlaylistRepositoryRedis : ChinookGenericRepositoryRedis<Playlist>
    {
        #region Methods

        public ChinookPlaylistRepositoryRedis(IUnitOfWork unitOfWork)
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
