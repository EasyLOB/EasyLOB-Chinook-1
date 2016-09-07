using EasyLOB;
using EasyLOB.Library;
using EasyLOB.Library.Resources;
using EasyLOB.Log;
using Microsoft.Practices.Unity;
using System;
using System.IO;
using System.Reflection;

namespace Chinook.Console
{
    public partial class Program
    {
        private static ILogManager LogManager;

        private static UnityContainer Container = new UnityContainer();

        private static void Main(string[] args)
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            FileSystemInfo exeFileInfo = new FileInfo(exePath);

            // Is application already running ?

            if (LibraryHelper.IsApplicationAlreadyRunning())
            {
                System.Console.WriteLine("Another instance of \"{0}\" is already running", exeFileInfo.Name);
                //NLogHelper.Warning("Another instance of \"{0}\" is already running", fileInfo.Name);

                return;
            }

            // Unity

            UnityHelper.RegisterMappings(Container);

            LogManager = (ILogManager)Container.Resolve<ILogManager>();

            // Directory

            string directory;
            // Z-Export
            directory = Path.Combine(Path.GetDirectoryName(exePath), LibraryHelper.AppSettings<string>("DirectoryExport"));
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            // Z-Import
            directory = Path.Combine(Path.GetDirectoryName(exePath), LibraryHelper.AppSettings<string>("DirectoryImport"));
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Shell Server
            //MainShell();

            // Hangfire Server
            MainHangfire();
        }

        private static string GetLog(string activity, string description)
        {
            return
                String.Format(PatternResources.DataFormat_DateTime, DateTime.Now.ToString()) +
                " - " + activity +
                (String.IsNullOrEmpty(description) ? "" : ": " + description);
        }
    }
}