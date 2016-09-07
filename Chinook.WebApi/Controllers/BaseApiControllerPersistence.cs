using EasyLOB.Data;
using EasyLOB.Persistence;

namespace EasyLOB.Library.WebApi
{
    public class BaseApiControllerPersistence<TEntity> : BaseApiController<TEntity>
        where TEntity : class, IZDataBase
    {
        #region Properties

        protected IUnitOfWork UnitOfWork { get; set; }

        protected IGenericRepository<TEntity> Repository { get { return UnitOfWork.GetRepository<TEntity>(); } }

        #endregion Properties
    }
}