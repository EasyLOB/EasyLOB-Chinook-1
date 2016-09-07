using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookMediaTypeRepositoryRedis : ChinookGenericRepositoryRedis<MediaType>
    {
        #region Methods

        public ChinookMediaTypeRepositoryRedis(IUnitOfWork unitOfWork)
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
