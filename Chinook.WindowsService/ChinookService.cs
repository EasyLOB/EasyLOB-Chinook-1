using Hangfire;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Timers;
using EasyLOB.Library;

// Create Windows Service in Visual Studio [C#]
// http://www.csharp-examples.net/create-windows-service

//  Windows Services Develop and Install a Windows Service in C#
// http://www.c-sharpcorner.com/UploadFile/naresh.avari/develop-and-install-a-windows-service-in-C-Sharp/

// Hangfire
// http://hangfire.io

// Install-Package Hangfire.Core
// Install-Package Hangfire.SqlServer
// Install-Package Microsoft.Owin.Hosting
// Install-Package Microsoft.Owin.Host.HttpListener

// Hangfire.MongoDB
// Hangfire.MySQL
// Hangfire.PostgreSQL
// Hangfire.Raven
// Hangfire.Redis
// Hangfire.SQLite
// Hangfire.SQLServer

// Hangfire.Unity

namespace Chinook.WindowsService
{
    public partial class ChinookService : ServiceBase
    {
        // Hangfire
        private BackgroundJobServer hangfire;

        //AppDomain.CurrentDomain.BaseDirectory
        //Assembly.GetExecutingAssembly().Location

        private string logFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ChinookService.log");

        //private Timer timer;

        public ChinookService()
        {
            try
            {
                InitializeComponent();

                // Log

                LogDelete();

                // Directory

                string exePath = Assembly.GetExecutingAssembly().Location;

                string directory;
                // Z-Export
                directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LibraryHelper.AppSettings<string>("DirectoryExport"));
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                // Z-Import
                directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LibraryHelper.AppSettings<string>("DirectoryImport"));
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }
            catch (Exception exception)
            {
                LogWrite(exception.Message);
            }
        }

        protected override void OnStart(string[] args)
        {
            // Timer

            //timer = new Timer();
            //timer.Interval = 10000; // 10 seconds
            //timer.Elapsed += new ElapsedEventHandler(OnTimer);
            //timer.Enabled = true;

            // Hangfire

            GlobalConfiguration.Configuration.UseSqlServerStorage("hangfire");
            hangfire = new BackgroundJobServer();

            StartOptions options = new StartOptions();
            //options.Urls.Add("http://localhost:5000");
            //options.Urls.Add("http://127.0.0.1:5000");
            //options.Urls.Add($"http://{Environment.MachineName}:5000");

            WebApp.Start<Startup>(options);

            // Log

            LogWrite("Start");
        }

        protected override void OnStop()
        {
            // Timer

            //timer.Enabled = false;

            // Hangfire

            hangfire.Dispose();

            // Log

            LogWrite("Stop");
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            //try
            //{
            //    timer.Enabled = false;

            //    LogWrite("Timer");
            //}
            //finally
            //{
            //    timer.Enabled = true;
            //}
        }

        private void LogDelete()
        {
            File.Delete(logFileName);
            StreamWriter logStream = new StreamWriter(logFileName);
            logStream.Close();
        }

        private void LogWrite(string log)
        {
            StreamWriter logStream = new StreamWriter(logFileName, true);
            logStream.WriteLine(DateTime.Now.ToString() + " " + log);
            logStream.Flush();
            logStream.Close();
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseHangfireDashboard();
        }
    }
}