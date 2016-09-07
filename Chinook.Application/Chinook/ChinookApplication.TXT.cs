using Chinook.Data;
using Chinook.Persistence;
using EasyLOB.Library;
using EasyLOB.Persistence;
using FileHelpers;
using System;
using System.IO;

// Install-Package FileHelpers

namespace Chinook.Application
{
    public partial class ChinookApplication : IChinookApplication
    {
        #region Methods

        public bool ExportGenreTXT(ZOperationResult operationResult, string fileDirectory, IChinookGenericApplication<Genre> genreApplication,
            out string filePath)
        {
            string template = "Genre";
            string file = template + "." + String.Format("{0:yyyyMMdd.HHmmssfff}", DateTime.Now) + ".txt";
            filePath = Path.Combine(fileDirectory, file);

            try
            {
                /*
                // write file

                var engine = new FileHelperEngine<Genre>();

                List<FileHelpersGenre> list = new List<FileHelpersGenre>();
                foreach (Genre genre in genreApplication.Select(null, null, null, null, 10).MyToList<Genre>())
                {
                    FileHelpersGenre fileHelpersGenre = new FileHelpersGenre();

                    fileHelpersGenre.GenreId = genre.GenreId;
                    fileHelpersGenre.Name = genre.Name;

                    list.Add(fileHelpersGenre);
                }

                engine.WriteFile(filePath, list);
                 */

                // write record-by-record

                var asyncEngine = new FileHelperAsyncEngine<FileHelpersGenre>();

                using (asyncEngine.BeginWriteFile(filePath))
                {
                    FileHelpersGenre fileHelpersGenre = new FileHelpersGenre();
                    foreach (Genre genre in genreApplication.Select(operationResult, null, null, null, null, 10).MyToList<Genre>())
                    {
                        fileHelpersGenre.GenreId = genre.GenreId;
                        fileHelpersGenre.Name = genre.Name;

                        asyncEngine.WriteNext(fileHelpersGenre);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return operationResult.Ok;
        }

        public bool ImportGenreTXTApplication(ZOperationResult operationResult, string filePath, IChinookGenericApplication<Genre> genreApplication)
        {
            int genres = 0;

            try
            {
                /*
                // read file

                var engine = new FixedFileEngine<FileHelpersGenre>();

                FileHelpersGenre[] array = engine.ReadFile(filePath);

                foreach (FileHelpersGenre element in array)
                {
                    Genre genre = new Genre(
                        0,
                        element.Name
                    );

                    genreApplication.Create(operationResult, genre);

                    if (!operationResult.Ok)
                    {
                        break;
                    }

                    genres++;
                }

                operationResult.StatusMessage = String.Format("{0} Genres imported", genres);
                 */

                // read record-by-record

                var asyncEngine = new FileHelperAsyncEngine<FileHelpersGenre>();

                using (asyncEngine.BeginReadFile(filePath))
                {
                    foreach (FileHelpersGenre element in asyncEngine)
                    {
                        //operationResult.AddOperationError("", String.Format("Genre Name = {0}", element.Name));

                        Genre genre = new Genre(
                            0,
                            element.Name + " Application"
                        );

                        if (genreApplication.Create(operationResult, genre))
                        {
                            genres++;
                        }

                        if (!operationResult.Ok)
                        {
                            break;
                        }
                    }

                    operationResult.StatusMessage = String.Format("{0} Genres imported", genres);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return operationResult.Ok;
        }

        public bool ImportGenreTXTPersistence(ZOperationResult operationResult, string filePath, IChinookUnitOfWork unitOfWork)
        {
            int genres = 0;

            try
            {

                // read record-by-record

                var asyncEngine = new FileHelperAsyncEngine<FileHelpersGenre>();
                IGenericRepository<Genre> repository = unitOfWork.GetRepository<Genre>();

                // Read
                using (asyncEngine.BeginReadFile(filePath))
                {
                    if (unitOfWork.BeginTransaction(operationResult))
                    {
                        try
                        {
                            foreach (FileHelpersGenre element in asyncEngine)
                            {
                                //operationResult.AddOperationError("", String.Format("Genre Name = {0}", element.Name));

                                Genre genre = new Genre(
                                    0,
                                    element.Name + " Persistence"
                                );

                                if (repository.Create(operationResult, genre))
                                {
                                    if (unitOfWork.Save(operationResult))
                                    {
                                        genres++;
                                    }
                                }

                                if (!operationResult.Ok)
                                {
                                    break;
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            unitOfWork.RollbackTransaction(operationResult);
                            operationResult.ParseException(exception);
                        }
                        finally
                        {
                            unitOfWork.CommitTransaction(operationResult);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            if (operationResult.Ok)
            {
                operationResult.StatusMessage = String.Format("{0} Genres imported", genres);
            }

            return operationResult.Ok;
        }

        #endregion Methods
    }
}