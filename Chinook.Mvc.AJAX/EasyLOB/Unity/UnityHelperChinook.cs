using Chinook.Application;
using Chinook.Persistence;
using Microsoft.Practices.Unity;

namespace EasyLOB
{
    public static class UnityHelperChinook
    {
        public static void RegisterMappings(IUnityContainer container)
        {
            container.RegisterType(typeof(IChinookGenericApplication<>), typeof(ChinookGenericApplication<>));
            container.RegisterType(typeof(IChinookGenericApplicationDTO<,>), typeof(ChinookGenericApplicationDTO<,>));
            container.RegisterType(typeof(IChinookApplication), typeof(ChinookApplication));

            // Entity Framework
            container.RegisterType(typeof(IChinookUnitOfWork), typeof(ChinookUnitOfWorkEF));
            container.RegisterType(typeof(IChinookGenericRepository<>), typeof(ChinookGenericRepositoryEF<>));

            // LINQ to DB
            //container.RegisterType(typeof(IChinookUnitOfWork), typeof(ChinookUnitOfWorkLINQ2DB));
            //container.RegisterType(typeof(IChinookGenericRepository<>), typeof(ChinookGenericRepositoryLINQ2DB<>));

            // MongoDB
            //container.RegisterType(typeof(IChinookUnitOfWork), typeof(ChinookUnitOfWorkMongoDB));
            //container.RegisterType(typeof(IChinookGenericRepository<>), typeof(ChinookGenericRepositoryMongoDB<>));

            // NHibernate
            //container.RegisterType(typeof(IChinookUnitOfWork), typeof(ChinookUnitOfWorkNH));
            //container.RegisterType(typeof(IChinookGenericRepository<>), typeof(ChinookGenericRepositoryNH<>));

            // RavenDB
            //container.RegisterType(typeof(IChinookUnitOfWork), typeof(ChinookUnitOfWorkRavenDB));
            //container.RegisterType(typeof(IChinookGenericRepository<>), typeof(ChinookGenericRepositoryRavenDB<>));

            // Redis
            //container.RegisterType(typeof(IChinookUnitOfWork), typeof(ChinookUnitOfWorkRedis));
            //container.RegisterType(typeof(IChinookGenericRepository<>), typeof(ChinookGenericRepositoryRedis<>));
        }
    }
}