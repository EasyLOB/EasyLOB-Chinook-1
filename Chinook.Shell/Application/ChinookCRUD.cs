using Chinook.Application;
using Chinook.Data;
using EasyLOB;
using EasyLOB.Library;
using Microsoft.Practices.Unity;
using System;

namespace Chinook.Shell
{
    partial class Program
    {
        private static void ApplicationChinookCRUDDemo()
        {
            Console.WriteLine("\nApplication Chinook CRUD Demo\n");

            var container = new UnityContainer();
            UnityHelper.RegisterMappings(container);

            ChinookGenericApplication<Genre> application =
                (ChinookGenericApplication<Genre>)container.Resolve<IChinookGenericApplication<Genre>>();
            Console.WriteLine(application.GetType().FullName + " with " + application.UnitOfWork.DBMS.ToString() + "\n");

            ZOperationResult operationResult = new ZOperationResult();

            // Count

            Console.WriteLine("COUNT: " + application.CountAll().ToString() + " Genre(s)");

            // Create

            Genre genre = new Genre();
            genre.Name = "A Genre";
            if (application.Create(operationResult, genre))
            {
                Console.WriteLine("CREATE: {0} - {1}", genre.GenreId, genre.Name);

                // Update

                genre.Name = "A Genre Updated";
                if (application.Update(operationResult, genre))
                {
                    Console.WriteLine("UPDATE: {0} - {1}", genre.GenreId, genre.Name);

                    // Delete

                    if (application.Delete(operationResult, genre))
                    {
                        Console.WriteLine("DELETE");
                    }
                }
            }

            if (!operationResult.Ok)
            {
                Console.WriteLine("\n");
                Console.WriteLine(operationResult.Text);
            }
        }
    }
}