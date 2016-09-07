using EasyLOB;
using EasyLOB.Data;
using EasyLOB.Persistence;
using EasyLOB.Security.Data;
using EasyLOB.Security.Persistence;
using Microsoft.Practices.Unity;
using System;
using System.Linq;

namespace Chinook.Shell
{
    partial class Program
    {
        private static void PersistenceSecurityDemo()
        {
            Console.WriteLine("\nPersistence Security Demo\n");

            var container = new UnityContainer();
            UnityHelper.RegisterMappings(container);

            IUnitOfWork unitOfWork = (IUnitOfWork)container.Resolve<ISecurityUnitOfWork>();
            Console.WriteLine(unitOfWork.GetType().FullName + " with " + unitOfWork.DBMS.ToString() + "\n");

            PersistenceSecurityData<Activity>(unitOfWork);
            PersistenceSecurityData<ActivityRole>(unitOfWork);
            PersistenceSecurityData<Role>(unitOfWork);
            PersistenceSecurityData<UserClaim>(unitOfWork);
            PersistenceSecurityData<UserLogin>(unitOfWork);
            PersistenceSecurityData<UserRole>(unitOfWork);
            PersistenceSecurityData<User>(unitOfWork);
        }

        private static void PersistenceSecurityData<TEntity>(IUnitOfWork unitOfWork)
            where TEntity : ZDataBase
        {
            IGenericRepository<TEntity> repository = unitOfWork.GetRepository<TEntity>();
            TEntity entity = repository.Query.FirstOrDefault();
            Console.WriteLine(typeof(TEntity).Name + ": " + repository.CountAll());
        }
    }
}