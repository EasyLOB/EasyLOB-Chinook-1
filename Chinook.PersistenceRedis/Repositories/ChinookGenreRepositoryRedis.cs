using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookGenreRepositoryRedis : ChinookGenericRepositoryRedis<Genre>
    {
        #region Methods

        public ChinookGenreRepositoryRedis(IUnitOfWork unitOfWork)
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
