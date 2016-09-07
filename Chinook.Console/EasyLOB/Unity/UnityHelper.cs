using Microsoft.Practices.Unity;

namespace EasyLOB
{
    public static class UnityHelper
    {
        public static void RegisterMappings(IUnityContainer container)
        {
            UnityHelperLog.RegisterMappings(container);
        }
    }
}