using Chinook.Service;
using Hangfire;
using System;

// Install-Package Hangfire.Core
// Install-Package Hangfire.SqlServer

namespace Chinook.Shell
{
    partial class Program
    {
        private static void HangfireDemo()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Hangfire Demo\n");
                Console.WriteLine("<0> RETURN");
                Console.WriteLine("<1> Export Genre TXT");
                Console.WriteLine("<2> Export Genre XLSX");
                Console.Write("\nChoose an option... ");

                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();

                switch (key.KeyChar) // <ENTER> = '\r'
                {
                    case ('0'):
                        exit = true;
                        break;

                    case ('1'):
                        HangfireExportGenreTXT();
                        break;

                    case ('2'):
                        HangfireExportGenreXLSX();
                        break;
                }

                if (!exit)
                {
                    Console.Write("\nPress <KEY> to continue... ");
                    Console.ReadKey();
                }
            }
        }

        private static void HangfireExportGenreTXT()
        {
            BackgroundJob.Enqueue(() =>
                ChinookServiceHelper.ExportGenreTXT()
            );
        }

        private static void HangfireExportGenreXLSX()
        {
            BackgroundJob.Schedule(() =>
                ChinookServiceHelper.ExportGenreXLSX(),
                TimeSpan.FromSeconds(30)
            );
        }
    }
}