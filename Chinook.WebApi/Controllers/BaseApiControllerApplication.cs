using EasyLOB.Application;
using EasyLOB.Data;

namespace EasyLOB.Library.WebApi
{
    public class BaseApiControllerApplication<TEntityDTO, TEntity> : BaseApiController<TEntity>
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
        where TEntity : class, IZDataBase
    {
        #region Properties

        protected IGenericApplicationDTO<TEntityDTO, TEntity> Application { get; set; }

        #endregion Properties
    }
}