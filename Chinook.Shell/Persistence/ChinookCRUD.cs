using Chinook.Data;
using Chinook.Persistence;
using EasyLOB;
using EasyLOB.Library;
using EasyLOB.Persistence;
using Microsoft.Practices.Unity;
using System;

namespace Chinook.Shell
{
    partial class Program
    {
        private static void PersistenceChinookCRUDDemo()
        {
            Console.WriteLine("\nPersistence Chinook CRUD Demo");

            var container = new UnityContainer();
            UnityHelper.RegisterMappings(container);

            IUnitOfWork unitOfWork = (IUnitOfWork)container.Resolve<IChinookUnitOfWork>();
            Console.WriteLine("\n" + unitOfWork.GetType().FullName + " with " + unitOfWork.DBMS.ToString() + "\n");

            IGenericRepository<Genre> repository = unitOfWork.GetRepository<Genre>();
            ZOperationResult operationResult = new ZOperationResult();

            // Count

            Console.WriteLine("COUNT: " + repository.CountAll().ToString() + " Genre(s)");

            // Create

            Genre genre = new Genre();
            genre.Name = "A Genre";
            if (repository.Create(operationResult, genre) && unitOfWork.Save(operationResult))
            {
                Console.WriteLine("CREATE: {0} - {1}", genre.GenreId, genre.Name);

                // Update

                genre.Name = "A Genre Updated";
                if (repository.Update(operationResult, genre) && unitOfWork.Save(operationResult))
                {
                    Console.WriteLine("UPDATE: {0} - {1}", genre.GenreId, genre.Name);

                    // Delete

                    if (repository.Delete(operationResult, genre) && unitOfWork.Save(operationResult))
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