using EasyLOB.Application;
using EasyLOB.Data;

namespace EasyLOB.Library.WebApi
{
    public class BaseODataControllerApplication<TEntityDTO, TEntity> : BaseODataController<TEntity>
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
        where TEntity : class, IZDataBase
    {
        #region Properties

        protected IGenericApplicationDTO<TEntityDTO, TEntity> Application { get; set; }

        #endregion Properties

        #region Methods

        public BaseODataControllerApplication(string entity)
            : base(entity)
        {
        }

        #endregion Methods
    }
}