using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookGenreRepositoryMongoDB : ChinookGenericRepositoryMongoDB<Genre>
    {
        #region Methods

        public ChinookGenreRepositoryMongoDB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override void Join(Genre genre)
        {
            if (genre != null)
            {
            }
        }

        #endregion Methods
    }
}
