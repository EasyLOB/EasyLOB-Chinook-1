using Chinook.Data;
using Chinook.Persistence;
using EasyLOB;
using EasyLOB.Data;
using EasyLOB.Persistence;
using Microsoft.Practices.Unity;
using System;
using System.Linq;

namespace Chinook.Shell
{
    partial class Program
    {
        private static void PersistenceChinookDemo()
        {
            Console.WriteLine("\nPersistence Chinook Demo");

            var container = new UnityContainer();
            UnityHelper.RegisterMappings(container);

            IUnitOfWork unitOfWork = (IUnitOfWork)container.Resolve<IChinookUnitOfWork>();
            Console.WriteLine("\n" + unitOfWork.GetType().FullName + " with " + unitOfWork.DBMS.ToString() + "\n");

            PersistenceChinookData<Album>(unitOfWork);
            PersistenceChinookData<Artist>(unitOfWork);
            PersistenceChinookData<Customer>(unitOfWork);
            PersistenceChinookData<Employee>(unitOfWork);
            PersistenceChinookData<Genre>(unitOfWork);
            PersistenceChinookData<Invoice>(unitOfWork);
            PersistenceChinookData<InvoiceLine>(unitOfWork);
            PersistenceChinookData<MediaType>(unitOfWork);
            PersistenceChinookData<Playlist>(unitOfWork);
            PersistenceChinookData<PlaylistTrack>(unitOfWork);
            PersistenceChinookData<Track>(unitOfWork);
        }

        private static void PersistenceChinookData<TEntity>(IUnitOfWork unitOfWork)
            where TEntity : ZDataBase
        {
            IGenericRepository<TEntity> repository = unitOfWork.GetRepository<TEntity>();
            TEntity entity = repository.Query.FirstOrDefault();
            Console.WriteLine(typeof(TEntity).Name + ": " + repository.CountAll());
        }
    }
}