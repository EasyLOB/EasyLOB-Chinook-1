using EasyLOB.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public interface IChinookGenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IZDataBase
    {
    }
}