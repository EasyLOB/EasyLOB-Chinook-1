using Chinook.Service;
using System.IO;
using System.Reflection;

namespace Chinook.Console
{
    public partial class Program
    {
        private static void MainShell()
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            FileSystemInfo exeFileInfo = new FileInfo(exePath);

            try
            {
                LogManager.Trace(GetLog(exeFileInfo.Name, "Start"));

                // Export Genre TXT
                string filePath = ChinookServiceHelper.ExportGenreTXT();

                // Export Genre XLSX
                ChinookServiceHelper.ExportGenreXLSX();
            }
            finally
            {
                LogManager.Trace(GetLog(exeFileInfo.Name, "Stop"));
            }
        }
    }
}