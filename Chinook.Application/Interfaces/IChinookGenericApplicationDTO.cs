using EasyLOB.Application;
using EasyLOB.Data;

namespace Chinook.Application
{
    public interface IChinookGenericApplicationDTO<TEntityDTO, TEntity> : IGenericApplicationDTO<TEntityDTO, TEntity>
        where TEntityDTO : class, IZDTOBase<TEntityDTO, TEntity>
        where TEntity : class, IZDataBase
    {
    }
}