using Chinook.Persistence;
using Chinook.Persistence.Chinook.Data;
using Chinook.Persistence.Default;
using EasyLOB.Library;
using EasyLOB.Persistence;
using Microsoft.OData.Client;
using System;
using System.Linq;
using System.Linq.Dynamic;

// Install-Package Microsoft.OData.Client
// Install-Package System.Linq.Dynamic

namespace Chinook.ODataClient
{
    partial class Program
    {
        private static void Main(string[] args)
        {
            bool exit = false;

            Container container = new Container(new Uri(LibraryHelper.AppSettings<string>("OData.Chinook")));

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Chinook OData Client Demo\n");
                Console.WriteLine("<0> EXIT");
                Console.WriteLine("<1> Album Demo");
                Console.WriteLine("<2> Genre Demo");
                Console.WriteLine("<3> LINQ Demo");
                Console.WriteLine("<4> CRUD Demo");
                Console.WriteLine("<5> UnitOfWork Demo { ZPersistenceOData }");
                Console.Write("\nChoose an option... ");

                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();

                switch (key.KeyChar) // <ENTER> = '\r'
                {
                    case ('0'):
                        exit = true;
                        break;

                    case ('1'):
                        ODataAlbumDemo(container);
                        break;

                    case ('2'):
                        ODataGenreDemo(container);
                        break;

                    case ('3'):
                        ODataLINQDemo(container);
                        break;

                    case ('4'):
                        ODataCRUDDemo(container);
                        break;

                    case ('5'):
                        ODataUnitOfWorkDemo();
                        break;
                }

                if (!exit)
                {
                    Console.Write("\nPress <KEY> to continue... ");
                    Console.ReadKey();
                }
            }
        }

        private static void ODataAlbumDemo(Container container)
        {
            Console.WriteLine("\nAlbum Demo\n");

            foreach (AlbumDTO album in container.Album)
            {
                Console.WriteLine("{0} {1} [{2}]", album.AlbumId, album.Title, album.ArtistLookupText);
            }
        }

        private static void ODataGenreDemo(Container container)
        {
            Console.WriteLine("\nGenre Demo\n");

            try
            {
                foreach (GenreDTO genre in container.Genre)
                {
                    Console.WriteLine("{0} {1}", genre.GenreId, genre.Name);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.GetType().Name);
                Console.WriteLine(exception.Message);
            }
        }

        private static void ODataLINQDemo(Container container)
        {
            Console.WriteLine("\nLINQ Demo");

            IQueryable<GenreDTO> queryGenre = container.CreateQuery<GenreDTO>("Genres").AsQueryable<GenreDTO>();

            Console.WriteLine("\nLINQ");
            var query = container.Genre
                .Where(x => x.GenreId < 7)
                .OrderByDescending(x => x.GenreId);
            foreach (GenreDTO genre in query)
            {
                Console.WriteLine("{0} {1}", genre.GenreId, genre.Name);
            }

            Console.WriteLine("\nDynamic LINQ");
            var queryDynamic = container.Genre
                .Where("GenreId < 7")
                .OrderBy("GenreId descending");
            foreach (GenreDTO genre in queryDynamic)
            {
                Console.WriteLine("{0} {1}", genre.GenreId, genre.Name);
            }
        }

        private static void ODataCRUDDemo(Container container)
        {
            Console.WriteLine("\nCRUD Demo\n");

            try
            {
                DataServiceResponse response;

                // Count

                Console.WriteLine("COUNT: " + container.Genre.Count().ToString() + " Genre(s)");

                // Create

                GenreDTO genre = new GenreDTO();
                genre.Name = "A Genre";
                //container.AddToGenres(genre);
                container.AddObject("Genre", genre);
                response = container.SaveChanges();
                Console.WriteLine("CREATE: {0} {1}", genre.GenreId, genre.Name);

                // Update

                genre.Name = "A Genre Updated";
                container.UpdateObject(genre);
                response = container.SaveChanges();
                Console.WriteLine("UPDATE: {0} {1}", genre.GenreId, genre.Name);

                // Delete

                container.DeleteObject(genre);
                response = container.SaveChanges();
                Console.WriteLine("DELETE");
            }
            catch (Exception exception)
            {
                WriteException(exception);
            }
        }

        private static void ODataUnitOfWorkDemo()
        {
            Console.WriteLine("\nUnit of Work Demo\n");

            try
            {
                IUnitOfWorkDTO unitOfWork = new ChinookUnitOfWorkOData();
                IGenericRepositoryDTO<Chinook.Data.GenreDTO, Chinook.Data.Genre> repository = unitOfWork.GetRepository<Chinook.Data.GenreDTO, Chinook.Data.Genre>();
                ZOperationResult operationResult = new ZOperationResult();

                Chinook.Data.GenreDTO genre = new Chinook.Data.GenreDTO();

                // Count

                Console.WriteLine("COUNT: " + repository.CountAll().ToString() + " Genre(s)");

                // Create

                if (operationResult.Ok)
                {
                    genre.Name = "A Genre";
                    if (repository.Create(operationResult, genre))
                    {
                        if (unitOfWork.Save(operationResult))
                        {
                            Console.WriteLine("CREATE: {0} {1}", genre.GenreId, genre.Name);
                        }
                    }
                }

                // Update

                if (operationResult.Ok)
                {
                    genre.Name = "A Genre Updated";
                    if (repository.Update(operationResult, genre))
                    {
                        if (unitOfWork.Save(operationResult))
                        {
                            Console.WriteLine("UPDATE: {0} {1}", genre.GenreId, genre.Name);
                        }
                    }
                }

                // Delete

                if (operationResult.Ok)
                {
                    if (repository.Delete(operationResult, genre))
                    {
                        if (unitOfWork.Save(operationResult))
                        {
                            Console.WriteLine("DELETE");
                        }
                    }
                }

                if (!operationResult.Ok)
                {
                    Console.WriteLine(operationResult.Text);
                }
            }
            catch (Exception exception)
            {
                WriteException(exception);
            }
        }

        private static void WriteException(Exception exception)
        {
            ZOperationResult operationResult = exception.OperationResult();
            if (operationResult != null)
            {
                Console.WriteLine("\n" + operationResult.Text);
            }
            else
            {
                Console.WriteLine("\n" + exception.ExceptionMessage());
            }
        }
    }
}