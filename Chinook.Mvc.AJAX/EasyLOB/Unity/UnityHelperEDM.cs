using EasyLOB.Extensions.Edm;
using Microsoft.Practices.Unity;

namespace EasyLOB
{
    public static class UnityHelperEDM
    {
        public static void RegisterMappings(IUnityContainer container)
        {
            //container.RegisterType(typeof(IEdmManager), typeof(EdmManagerMock));
            container.RegisterType(typeof(IEdmManager), typeof(EdmManagerFileSystem), new InjectionConstructor());
            //container.RegisterType(typeof(IEdmManager), typeof(EdmFtpSystem), new InjectionConstructor());
        }
    }
}