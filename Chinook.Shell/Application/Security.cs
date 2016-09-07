using EasyLOB;
using EasyLOB.Data;
using EasyLOB.Library;
using EasyLOB.Security.Application;
using EasyLOB.Security.Data;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinook.Shell
{
    partial class Program
    {
        private static void ApplicationSecurityDemo()
        {
            Console.WriteLine("\nApplication Security Demo\n");

            var container = new UnityContainer();
            UnityHelper.RegisterMappings(container);

            ApplicationSecurityData<Activity>(container);
            ApplicationSecurityDTO<ActivityDTO, Activity>(container);

            ApplicationSecurityData<ActivityRole>(container);
            ApplicationSecurityDTO<ActivityRoleDTO, ActivityRole>(container);

            ApplicationSecurityData<Role>(container);
            ApplicationSecurityDTO<RoleDTO, Role>(container);

            ApplicationSecurityData<UserClaim>(container);
            ApplicationSecurityDTO<UserClaimDTO, UserClaim>(container);

            ApplicationSecurityData<UserLogin>(container);
            ApplicationSecurityDTO<UserLoginDTO, UserLogin>(container);

            ApplicationSecurityData<UserRole>(container);
            ApplicationSecurityDTO<UserRoleDTO, UserRole>(container);

            ApplicationSecurityData<User>(container);
            ApplicationSecurityDTO<UserDTO, User>(container);
        }

        private static void ApplicationSecurityData<TEntity>(UnityContainer container)
            where TEntity : ZDataBase
        {
            SecurityGenericApplication<TEntity> application =
                (SecurityGenericApplication<TEntity>)container.Resolve<ISecurityGenericApplication<TEntity>>();
            ZOperationResult operationResult = new ZOperationResult();
            IEnumerable<TEntity> enumerable = application.SelectAll(operationResult);
            Console.WriteLine(typeof(TEntity).Name + ": {0}", enumerable.Count());
        }

        private static void ApplicationSecurityDTO<TEntityDTO, TEntity>(UnityContainer container)
            where TEntityDTO : ZDTOBase<TEntityDTO, TEntity>
            where TEntity : ZDataBase
        {
            SecurityGenericApplicationDTO<TEntityDTO, TEntity> application =
                (SecurityGenericApplicationDTO<TEntityDTO, TEntity>)container.Resolve<ISecurityGenericApplicationDTO<TEntityDTO, TEntity>>();
            ZOperationResult operationResult = new ZOperationResult();
            IEnumerable<TEntityDTO> enumerable = application.SelectAll(operationResult);
            Console.WriteLine(typeof(TEntity).Name + "DTO: {0}", enumerable.Count());
        }
    }
}