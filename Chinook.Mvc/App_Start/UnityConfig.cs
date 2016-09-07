using EasyLOB;
using EasyLOB.Identity.Mvc;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Unity.Mvc5;

namespace Chinook.Mvc
{
    public static class UnityConfig
    {
        public static void RegisterMappings()
        {
            var container = new UnityContainer();

            UnityHelperChinook.RegisterMappings(container);
            UnityHelperEDM.RegisterMappings(container);

            UnityHelperAuditTrail.RegisterMappings(container);
            UnityHelperLog.RegisterMappings(container);
            UnityHelperSecurity.RegisterMappings(container);

            // Account

            container.RegisterType<AccountController>(new InjectionConstructor());
            //container.RegisterType<ManageController>(new InjectionConstructor());
            //container.RegisterType<RolesAdminController>(new InjectionConstructor());
            //container.RegisterType<UsersAdminController>(new InjectionConstructor());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}