using EasyLOB.Security.Application;
using EasyLOB.Security.Persistence;
using Microsoft.Practices.Unity;

namespace EasyLOB
{
    public static class UnityHelperSecurity
    {
        public static void RegisterMappings(IUnityContainer container)
        {
            container.RegisterType(typeof(ISecurityGenericApplication<>), typeof(SecurityGenericApplication<>));
            container.RegisterType(typeof(ISecurityGenericApplicationDTO<,>), typeof(SecurityGenericApplicationDTO<,>));

            // Entity Framework
            container.RegisterType(typeof(ISecurityUnitOfWork), typeof(SecurityUnitOfWorkEF));
            container.RegisterType(typeof(ISecurityGenericRepository<>), typeof(SecurityGenericRepositoryEF<>));

            // NHibernate
            //container.RegisterType(typeof(ISecurityUnitOfWork), typeof(SecurityUnitOfWorkNH));
            //container.RegisterType(typeof(ISecurityGenericRepository<>), typeof(SecurityGenericRepositoryNH<>));
        }
    }
}