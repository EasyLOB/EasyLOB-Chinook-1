using System;

namespace Chinook.Shell
{
    partial class Program
    {
        private static void ApplicationDemo()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Application Demo\n");
                Console.WriteLine("<0> RETURN");
                Console.WriteLine("<1> Chinook Demo");
                Console.WriteLine("<2> Chinook CRUD Demo");
                Console.WriteLine("<3> Security Demo");
                Console.Write("\nChoose an option... ");

                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();

                switch (key.KeyChar) // <ENTER> = '\r'
                {
                    case ('0'):
                        exit = true;
                        break;

                    case ('1'):
                        ApplicationChinookDemo();
                        break;

                    case ('2'):
                        ApplicationChinookCRUDDemo();
                        break;

                    case ('3'):
                        ApplicationSecurityDemo();
                        break;
                }

                if (!exit)
                {
                    Console.Write("\nPress <KEY> to continue... ");
                    Console.ReadKey();
                }
            }
        }
    }
}