using EasyLOB.Application;
using EasyLOB.Data;

namespace Chinook.Application
{
    public interface IChinookGenericApplication<TEntity> : IGenericApplication<TEntity>
        where TEntity : class, IZDataBase
    {
    }
}