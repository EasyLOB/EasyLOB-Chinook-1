using EasyLOB.Data;
using EasyLOB.Persistence;

namespace EasyLOB.Library.WebApi
{
    public class BaseODataControllerPersistence<TEntity> : BaseODataController<TEntity>
        where TEntity : class, IZDataBase
    {
        #region Properties

        protected IUnitOfWork UnitOfWork { get; set; }

        #endregion Properties

        #region Methods

        public BaseODataControllerPersistence(string entity)
            : base(entity)
        {
        }

        #endregion Methods
    }
}