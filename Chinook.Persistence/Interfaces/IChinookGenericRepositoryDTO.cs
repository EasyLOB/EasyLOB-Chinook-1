using EasyLOB.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public interface IChinookGenericRepositoryDTO<TEntityDTO, TEntity> : IGenericRepositoryDTO<TEntityDTO, TEntity>
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
        where TEntity : class, IZDataBase
    {
    }
}