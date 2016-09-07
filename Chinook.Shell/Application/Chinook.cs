using Chinook.Application;
using Chinook.Data;
using EasyLOB;
using EasyLOB.Data;
using EasyLOB.Library;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinook.Shell
{
    partial class Program
    {
        private static void ApplicationChinookDemo()
        {
            Console.WriteLine("\nApplication Chinook Demo\n");

            var container = new UnityContainer();
            UnityHelper.RegisterMappings(container);

            ApplicationChinookData<Album>(container);
            ApplicationChinookDTO<AlbumDTO, Album>(container);

            ApplicationChinookData<Artist>(container);
            ApplicationChinookDTO<ArtistDTO, Artist>(container);

            ApplicationChinookData<Customer>(container);
            ApplicationChinookDTO<CustomerDTO, Customer>(container);

            ApplicationChinookData<Employee>(container);
            ApplicationChinookDTO<EmployeeDTO, Employee>(container);

            ApplicationChinookData<Genre>(container);
            ApplicationChinookDTO<GenreDTO, Genre>(container);

            ApplicationChinookData<Invoice>(container);
            ApplicationChinookDTO<InvoiceDTO, Invoice>(container);

            ApplicationChinookData<InvoiceLine>(container);
            ApplicationChinookDTO<InvoiceLineDTO, InvoiceLine>(container);

            ApplicationChinookData<MediaType>(container);
            ApplicationChinookDTO<MediaTypeDTO, MediaType>(container);

            ApplicationChinookData<Playlist>(container);
            ApplicationChinookDTO<PlaylistDTO, Playlist>(container);

            ApplicationChinookData<PlaylistTrack>(container);
            ApplicationChinookDTO<PlaylistTrackDTO, PlaylistTrack>(container);

            ApplicationChinookData<Track>(container);
            ApplicationChinookDTO<TrackDTO, Track>(container);
        }

        private static void ApplicationChinookData<TEntity>(UnityContainer container)
            where TEntity : ZDataBase
        {
            ChinookGenericApplication<TEntity> application =
                (ChinookGenericApplication<TEntity>)container.Resolve<IChinookGenericApplication<TEntity>>();
            ZOperationResult operationResult = new ZOperationResult();
            IEnumerable<TEntity> enumerable = application.SelectAll(operationResult);
            Console.WriteLine(typeof(TEntity).Name + ": {0}", enumerable.Count());
        }

        private static void ApplicationChinookDTO<TEntityDTO, TEntity>(UnityContainer container)
            where TEntityDTO : ZDTOBase<TEntityDTO, TEntity>
            where TEntity : ZDataBase
        {
            ChinookGenericApplicationDTO<TEntityDTO, TEntity> application =
                (ChinookGenericApplicationDTO<TEntityDTO, TEntity>)container.Resolve<IChinookGenericApplicationDTO<TEntityDTO, TEntity>>();
            ZOperationResult operationResult = new ZOperationResult();
            IEnumerable<TEntityDTO> enumerable = application.SelectAll(operationResult);
            Console.WriteLine(typeof(TEntity).Name + "DTO: {0}", enumerable.Count());
        }
    }
}