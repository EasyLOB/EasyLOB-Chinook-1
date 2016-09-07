using Chinook.Application;
using Chinook.Persistence;
using EasyLOB.Persistence;
using Microsoft.Practices.Unity;
using NHibernate;

namespace EasyLOB
{
    public static class UnityHelperChinook
    {
        public static void RegisterMappings(IUnityContainer container)
        {
            container.RegisterType(typeof(IChinookGenericApplication<>), typeof(ChinookGenericApplication<>));
            container.RegisterType(typeof(IChinookGenericApplicationDTO<,>), typeof(ChinookGenericApplicationDTO<,>));

            container.RegisterType(typeof(IChinookApplication), typeof(ChinookApplication));
        }

        public static void RegisterMappingsEF(IUnityContainer container)
        {
            container.RegisterType(typeof(IChinookUnitOfWork), typeof(ChinookUnitOfWorkEF));
            container.RegisterType(typeof(IChinookGenericRepository<>), typeof(ChinookGenericRepositoryEF<>));
        }

        public static void RegisterMappingsLINQ2DB(IUnityContainer container)
        {
            container.RegisterType(typeof(IChinookUnitOfWork), typeof(ChinookUnitOfWorkLINQ2DB));
            container.RegisterType(typeof(IChinookGenericRepository<>), typeof(ChinookGenericRepositoryLINQ2DB<>));
        }

        public static void RegisterMappingsMongoDB(IUnityContainer container)
        {
            container.RegisterType(typeof(IChinookUnitOfWork), typeof(ChinookUnitOfWorkMongoDB));
            container.RegisterType(typeof(IChinookGenericRepository<>), typeof(ChinookGenericRepositoryMongoDB<>));
        }

        public static void RegisterMappingsNH(IUnityContainer container)
        {
            container.RegisterType(typeof(IChinookUnitOfWork), typeof(ChinookUnitOfWorkNH));
            container.RegisterType(typeof(IChinookGenericRepository<>), typeof(ChinookGenericRepositoryNH<>));
        }

        public static void RegisterMappingsRavenDB(IUnityContainer container)
        {
            container.RegisterType(typeof(IChinookUnitOfWork), typeof(ChinookUnitOfWorkRavenDB));
            container.RegisterType(typeof(IChinookGenericRepository<>), typeof(ChinookGenericRepositoryRavenDB<>));
        }

        public static void RegisterMappingsRedis(IUnityContainer container)
        {
            container.RegisterType(typeof(IChinookUnitOfWork), typeof(ChinookUnitOfWorkRedis));
            container.RegisterType(typeof(IChinookGenericRepository<>), typeof(ChinookGenericRepositoryRedis<>));
        }
    }
}