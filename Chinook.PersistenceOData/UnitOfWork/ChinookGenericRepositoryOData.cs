using EasyLOB.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookGenericRepositoryOData<TEntityDTO, TEntity> : GenericRepositoryOData<TEntityDTO, TEntity>, IChinookGenericRepositoryDTO<TEntityDTO, TEntity>
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
        where TEntity : class, IZDataBase
    {
        #region Methods

        public ChinookGenericRepositoryOData(IUnitOfWorkDTO unitOfWork)
            : base(unitOfWork)
        {
            Container = (unitOfWork as ChinookUnitOfWorkOData).Container;
        }

        #endregion Methods
    }
}