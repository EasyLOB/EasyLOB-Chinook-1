using Microsoft.Practices.Unity;

namespace EasyLOB
{
    internal static class UnityHelper
    {
        public static void RegisterMappings(IUnityContainer container)
        {
            UnityHelperChinook.RegisterMappings(container);

            UnityHelperAuditTrail.RegisterMappings(container);
            UnityHelperLog.RegisterMappings(container);
        }
    }
}