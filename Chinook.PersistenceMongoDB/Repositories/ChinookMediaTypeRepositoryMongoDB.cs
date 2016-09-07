using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookMediaTypeRepositoryMongoDB : ChinookGenericRepositoryMongoDB<MediaType>
    {
        #region Methods

        public ChinookMediaTypeRepositoryMongoDB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override void Join(MediaType mediaType)
        {
            if (mediaType != null)
            {
            }
        }

        #endregion Methods
    }
}
