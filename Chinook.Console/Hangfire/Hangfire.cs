using Hangfire;
using Microsoft.Owin.Hosting;
using Owin;
using System.Diagnostics;

// Hangfire
// http://hangfire.io/overview.html

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

namespace Chinook.Console
{
    public partial class Program
    {
        private static void MainHangfire()
        {
            BackgroundJobServer hangfire = null;

            try
            {
                System.Console.Clear();
                System.Console.WriteLine("Hangfire");
                System.Console.WriteLine("http://localhost:5000/hangfire");

                GlobalConfiguration.Configuration.UseSqlServerStorage("hangfire");
                hangfire = new BackgroundJobServer();

                StartOptions options = new StartOptions();
                //options.Urls.Add("http://localhost:5000");
                //options.Urls.Add("http://127.0.0.1:5000");
                //options.Urls.Add($"http://{Environment.MachineName}:5000");

                WebApp.Start<Startup>(options);

                ProcessStartInfo process = new ProcessStartInfo("chrome.exe");
                process.Arguments = "http://localhost:5000/hangfire";
                Process.Start(process);

                System.Console.Write("\nPress <KEY> to continue... ");
                System.Console.ReadKey();
            }
            finally
            {
                if (hangfire != null)
                {
                    hangfire.Dispose();
                }
            }
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