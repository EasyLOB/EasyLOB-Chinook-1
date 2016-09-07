using Chinook.Application;
using EasyLOB.Extensions.Mail;
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

        public static void ExportGenreXLSX()
        {
            LogManager.Trace(GetLog("Export Genre XLSX", "Start"));

            string filePath = "";

            try
            {
                string exePath = Assembly.GetExecutingAssembly().Location;
                FileSystemInfo exeFileInfo = new FileInfo(exePath);
                string fileDirectory = Path.Combine(Path.GetDirectoryName(exePath), LibraryHelper.AppSettings<string>("DirectoryExport"));

                ZOperationResult operationResult = new ZOperationResult();
                ChinookApplication application =
                    (ChinookApplication)Container.Resolve<IChinookApplication>();

                // WorkSheet

                LogManager.Trace(GetLog("Export Genre", "Worksheet"));
                if (application.ExportGenreXLSX(operationResult, fileDirectory,
                    out filePath))
                {
                    LogManager.Trace(GetLog("Export Genre", "e-mail"));

                    string body = @"
Hi,

Attached you will find the [Genre.xlsx] worksheet.

Sincerely,
Chinook";
                    (new MailManager()).Mail("Chinook", "EasyLOB", "easylob@gmail.com",
                        "Chinook - Export Genre", body, false, new string[] { filePath });
                }
                else
                {
                    LogManager.LogOperationResult(operationResult);
                }
            }
            catch (Exception exception)
            {
                LogManager.LogException(exception);
            }
            //finally
            //{
            //    if (!String.IsNullOrEmpty(filePath))
            //    {
            //        File.Delete(filePath);
            //    }
            //}

            LogManager.Trace(GetLog("Export Genre", "Stop"));
        }

        #endregion Methods
    }
}