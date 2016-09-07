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
        private static void PersistenceChinookTransactionDemo(bool isCommit = true)
        {
            if (isCommit)
            {
                Console.WriteLine("\nPersistence Chinook Transaction Demo with Commit");
            }
            else
            {
                Console.WriteLine("\nPersistence Chinook Transaction Demo with Rollback");
            }

            var container = new UnityContainer();
            UnityHelper.RegisterMappings(container);

            IUnitOfWork unitOfWork = (IUnitOfWork)container.Resolve<IChinookUnitOfWork>();
            IGenericRepository<Artist> repository = unitOfWork.GetRepository<Artist>();
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                unitOfWork.BeginTransaction(operationResult);

                Artist artist;
                artist = new Artist(0, "Artist 1");
                if (repository.Create(operationResult, artist) && unitOfWork.Save(operationResult))
                {
                    artist = new Artist(0, "Artist 2");
                    if (repository.Create(operationResult, artist) && unitOfWork.Save(operationResult))
                    {
                        artist = new Artist(0, "Artist 3");
                        if (repository.Create(operationResult, artist))
                        {
                            unitOfWork.Save(operationResult);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseExceptionEntityFramework(exception);
                unitOfWork.RollbackTransaction(operationResult);
            }
            finally
            {
                if (isCommit)
                {
                    unitOfWork.CommitTransaction(operationResult);
                }
                else
                {
                    unitOfWork.RollbackTransaction(operationResult);
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