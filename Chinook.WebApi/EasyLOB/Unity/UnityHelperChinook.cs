using Microsoft.Practices.Unity;
using Chinook.Application;
using Chinook.Persistence;

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
        }
    }
}