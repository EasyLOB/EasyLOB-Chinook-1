using System.ServiceProcess;

// Create Windows Service in Visual Studio [C#]
// http://www.csharp-examples.net/create-windows-service

// Develop and Install a Windows Service in C#
// http://www.c-sharpcorner.com/UploadFile/naresh.avari/develop-and-install-a-windows-service-in-C-Sharp

// C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe Chinook.WindowsService.exe
// C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /u Chinook.WindowsService.exe

namespace Chinook.WindowsService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ChinookService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}