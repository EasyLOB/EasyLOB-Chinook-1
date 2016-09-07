using System.Linq;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookMediaTypeRepositoryLINQ2DB : ChinookGenericRepositoryLINQ2DB<MediaType>
    {
        #region Methods

        public ChinookMediaTypeRepositoryLINQ2DB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override IQueryable<MediaType> Join(IQueryable<MediaType> query)
        {
            return
                from mediaType in query
                select new MediaType
                {
                    MediaTypeId = mediaType.MediaTypeId,
                    Name = mediaType.Name
                };
        }

        #endregion Methods
    }
}

