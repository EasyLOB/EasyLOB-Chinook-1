using Chinook.Data;
using Chinook.Persistence;
using EasyLOB;
using EasyLOB.Data;
using EasyLOB.Persistence;
using Microsoft.Practices.Unity;
using System;

namespace Chinook.Shell
{
    partial class Program
    {
        private static void PersistenceChinookGetByIdDemo()
        {
            Console.WriteLine("\nPersistence Chinook GetById() Demo");

            var container = new UnityContainer();
            UnityHelper.RegisterMappings(container);

            IUnitOfWork unitOfWork = (IUnitOfWork)container.Resolve<IChinookUnitOfWork>();

            GetById<Album>(container);
            GetById<Artist>(container);
            GetById<Customer>(container);
            GetById<CustomerDocument>(container);
            GetById<Employee>(container);
            GetById<Genre>(container);
            GetById<Invoice>(container);
            GetById<InvoiceLine>(container);
            GetById<MediaType>(container);
            GetById<Playlist>(container);
            GetById<PlaylistTrack>(container, new object[] { 1, 1 });
            GetById<Track>(container);
        }

        private static void GetById<TEntity>(UnityContainer container, object[] ids = null)
            where TEntity : ZDataBase
        {
            IUnitOfWork unitOfWork = (IUnitOfWork)container.Resolve<IChinookUnitOfWork>();
            IGenericRepository<TEntity> repository = unitOfWork.GetRepository<TEntity>();

            TEntity entity;
            object[] getByIds;

            getByIds = ids ?? new object[] { 1 };
            entity = repository.GetById(getByIds);
            Console.WriteLine("\n{0}: {1}", typeof(TEntity).Name, (entity == null ? "null" : entity.GetType().Name));

            if (ids == null)
            {
                getByIds = new object[] { 100001 };
                entity = repository.GetById(getByIds);
                Console.WriteLine("{0}: {1}", typeof(TEntity).Name, (entity == null ? "null" : entity.GetType().Name));
            }
        }
    }
}