using Chinook.Application;
using Chinook.Data;
using EasyLOB.Library;
using Microsoft.Practices.Unity;
using System;
using System.IO;
using System.Reflection;

namespace Chinook.Service
{
    public partial class ChinookServiceHelper
    {
        #region Methods

        public static string ExportGenreTXT()
        {
            LogManager.Trace(GetLog("Export Genre TXT", "Start"));

            string filePath = "";

            try
            {
                string exePath = Assembly.GetExecutingAssembly().Location;
                FileSystemInfo exeFileInfo = new FileInfo(exePath);
                string fileDirectory = Path.Combine(Path.GetDirectoryName(exePath), LibraryHelper.AppSettings<string>("DirectoryExport"));

                ZOperationResult operationResult = new ZOperationResult();
                ChinookApplication application =
                    (ChinookApplication)Container.Resolve<IChinookApplication>();
                ChinookGenericApplication<Genre> genreApplication =
                    (ChinookGenericApplication<Genre>)Container.Resolve<IChinookGenericApplication<Genre>>();

                // Clean Z-Export

                application.Clean(operationResult, fileDirectory);

                // Text File

                LogManager.Trace(GetLog("Export Genre", "Text File"));

                if (!application.ExportGenreTXT(operationResult, fileDirectory, genreApplication,
                    out filePath))
                {
                    LogManager.LogOperationResult(operationResult);
                }
            }
            catch (Exception exception)
            {
                LogManager.LogException(exception);
            }

            LogManager.Trace(GetLog("Export Genre", "Stop"));

            return filePath;
        }

        #endregion Methods
    }
}