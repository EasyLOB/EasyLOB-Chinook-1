using EasyLOB.AuditTrail;
using Microsoft.Practices.Unity;

namespace EasyLOB
{
    public static class UnityHelperAuditTrail
    {
        public static void RegisterMappings(IUnityContainer container)
        {
            container.RegisterType(typeof(IAuditTrailManager), typeof(AuditTrailManagerMock));
            //container.RegisterType(typeof(IAuditTrailManager), typeof(AuditTrailManager));

            //container.RegisterType(typeof(IAuditTrailGenericApplication<>), typeof(AuditTrailGenericApplication<>));
            //container.RegisterType(typeof(IAuditTrailGenericApplicationDTO<,>), typeof(AuditTrailGenericApplicationDTO<,>));

            // Entity Framework
            //container.RegisterType(typeof(IAuditTrailUnitOfWork), typeof(AuditTrailUnitOfWorkEF));
            //container.RegisterType(typeof(IAuditTrailGenericRepository<>), typeof(AuditTrailGenericRepositoryEF<>));

            // NHibernate
            //container.RegisterType(typeof(IAuditTrailUnitOfWork), typeof(AuditTrailUnitOfWorkNH));
            //container.RegisterType(typeof(IAuditTrailGenericRepository<>), typeof(AuditTrailGenericRepositoryNH<>));

            // LINQ to DB
            //container.RegisterType(typeof(IAuditTrailUnitOfWork), typeof(AuditTrailUnitOfWorkLINQ2DB));
            //container.RegisterType(typeof(IAuditTrailGenericRepository<>), typeof(AuditTrailGenericRepositoryLINQ2DB<>));
        }
    }
}