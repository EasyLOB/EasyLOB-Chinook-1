using EasyLOB.Identity.Mvc;
using Microsoft.Practices.Unity;

namespace EasyLOB
{
    public static class UnityHelper
    {
        public static void RegisterMappings(IUnityContainer container)
        {
            UnityHelperChinook.RegisterMappings(container);

            UnityHelperAuditTrail.RegisterMappings(container);
            UnityHelperLog.RegisterMappings(container);
            UnityHelperSecurity.RegisterMappings(container);

            // Account

            container.RegisterType<AccountController>(new InjectionConstructor());
            //container.RegisterType<ManageController>(new InjectionConstructor());
            //container.RegisterType<RolesAdminController>(new InjectionConstructor());
            //container.RegisterType<UsersAdminController>(new InjectionConstructor());        }
        }
    }
}