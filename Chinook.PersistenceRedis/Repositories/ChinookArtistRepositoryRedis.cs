using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookArtistRepositoryRedis : ChinookGenericRepositoryRedis<Artist>
    {
        #region Methods

        public ChinookArtistRepositoryRedis(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override void Join(Artist artist)
        {
            if (artist != null)
            {
            }
        }

        #endregion Methods
    }
}
