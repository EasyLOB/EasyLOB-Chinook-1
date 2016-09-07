using System.Linq;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookGenreRepositoryLINQ2DB : ChinookGenericRepositoryLINQ2DB<Genre>
    {
        #region Methods

        public ChinookGenreRepositoryLINQ2DB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override IQueryable<Genre> Join(IQueryable<Genre> query)
        {
            return
                from genre in query
                select new Genre
                {
                    GenreId = genre.GenreId,
                    Name = genre.Name
                };
        }

        #endregion Methods
    }
}

