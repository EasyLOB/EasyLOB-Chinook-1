using EasyLOB.Log;
using Microsoft.Practices.Unity;

namespace EasyLOB
{
    internal static class UnityHelperLog
    {
        public static void RegisterMappings(IUnityContainer container)
        {
            container.RegisterType(typeof(ILogManager), typeof(LogManagerMock));
            //container.RegisterType(typeof(ILogManager), typeof(LogManagerNLog));
        }
    }
}