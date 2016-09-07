using System;
using System.Web.Services.Protocols;

namespace Chinook.WebServiceClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Chinook Web Service Client\n");
                Console.WriteLine("<0> EXIT");
                Console.WriteLine("<1> Hello World");
                Console.WriteLine("<2> SELECT Genres");
                Console.Write("\nChoose an option... ");

                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();

                switch (key.KeyChar) // <ENTER> = '\r'
                {
                    case ('0'):
                        exit = true;
                        break;

                    case ('1'):
                        HelloWorld();
                        break;

                    case ('2'):
                        SelectGenres();
                        break;
                }

                if (!exit)
                {
                    Console.Write("\nPress <KEY> to continue... ");
                    Console.ReadKey();
                }
            }
        }

        private static void HelloWorld()
        {
            ChinookServiceReference.ChinookWebServiceSoapClient service = new ChinookServiceReference.ChinookWebServiceSoapClient();

            Console.WriteLine("\nHello World\n{");

            try
            {
                string s = service.HelloWorld();
                Console.WriteLine(s);
            }
            catch (SoapException exception)
            {
                Console.WriteLine("SOAP Exception");
                WriteException(exception);
            }
            catch (Exception exception)
            {
                WriteException(exception);
            }

            Console.WriteLine("}");
        }

        private static void SelectGenres()
        {
            ChinookServiceReference.ChinookWebServiceSoapClient service = new ChinookServiceReference.ChinookWebServiceSoapClient();

            Console.WriteLine("\nSelect Genres\n{");

            try
            {
                ChinookServiceReference.AuthenticationHeader authentication = new ChinookServiceReference.AuthenticationHeader();
                authentication.Username = "UserName";
                authentication.Password = "Password";
                //authentication.Password = "";

                ChinookServiceReference.GenreDTO[] result = service.SelectGenres(authentication);
                foreach (ChinookServiceReference.GenreDTO genreDTO in result)
                {
                    Console.WriteLine("Id: {0} - Name: {1}", genreDTO.GenreId, genreDTO.Name);
                }
            }
            catch (SoapException exception)
            {
                Console.WriteLine("SOAP Exception");
                WriteException(exception);
            }
            catch (Exception exception)
            {
                WriteException(exception);
            }

            Console.WriteLine("}");
        }

        private static void WriteException(Exception exception)
        {
            Console.WriteLine(exception.Message, "");
        }

        private static void WriteException(Exception exception, string spaces)
        {
            Console.WriteLine(exception.Message);
            if (exception.InnerException != null)
            {
                WriteException(exception.InnerException, spaces + "  ");
            }
        }
    }
}