using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookArtistRepositoryMongoDB : ChinookGenericRepositoryMongoDB<Artist>
    {
        #region Methods

        public ChinookArtistRepositoryMongoDB(IUnitOfWork unitOfWork)
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
