using Hangfire;
using System;

namespace Chinook.Shell
{
    partial class Program
    {
        private static void Main(string[] args)
        {

            bool exit = false;

            // Hangfire
            //GlobalConfiguration.Configuration.UseSqlServerStorage("hangfire");

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Chinook Shell\n");
                Console.WriteLine("<0> EXIT");
                Console.WriteLine("<1> Chinook Application Demo");
                Console.WriteLine("<2> Chinook Persistence Demo");
                Console.WriteLine("<3> Chinook SQL Server => MongoDB");
                Console.WriteLine("<4> Chinook SQL Server => RavenDB");
                Console.WriteLine("<5> Chinook SQL Server => Redis");
                Console.WriteLine("<6> Chinook RESET");
                Console.WriteLine("<7> Hangfire");
                Console.Write("\nChoose an option... ");

                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();

                switch (key.KeyChar) // <ENTER> = '\r'
                {
                    case ('0'):
                        exit = true;
                        break;

                    case ('1'):
                        ApplicationDemo();
                        break;

                    case ('2'):
                        PersistenceDemo();
                        break;

                    case ('3'):
                        SQLServer2MongoDB();
                        break;

                    case ('4'):
                        SQLServer2RavenDB();
                        break;

                    case ('5'):
                        SQLServer2Redis();
                        break;

                    case ('6'):
                        ApplicationChinookReset();
                        break;

                    case ('7'):
                        HangfireDemo();
                        break;
                }

                if (!exit && "345".IndexOf(key.KeyChar) >= 0)
                {
                    Console.Write("\nPress <KEY> to continue... ");
                    Console.ReadKey();
                }
            }
        }
    }
}