using Chinook.Data;
using Chinook.Mvc;
using Chinook.Persistence;
using EasyLOB;
using EasyLOB.Persistence;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chinook.Shell
{
    partial class Program
    {
        private static void PersistenceChinookSelectorDemo()
        {
            Console.WriteLine("\nPersistence Chinook Selector Demo");

            var container = new UnityContainer();
            UnityHelper.RegisterMappings(container);

            IUnitOfWork unitOfWork = (IUnitOfWork)container.Resolve<IChinookUnitOfWork>();
            IGenericRepository<Album> repository = unitOfWork.GetRepository<Album>();
            Console.WriteLine("\n" + unitOfWork.GetType().FullName + " with " + unitOfWork.DBMS.ToString());

            IQueryable<Album> query = unitOfWork.GetQuery<Album>();
            IEnumerable<Album> enumerable;
            IEnumerable<AlbumDTO> enumerableDTO;

            {
                Console.WriteLine("\nQuery");
                enumerable = query
                    .Where(x => x.AlbumId <= 3)
                    .ToList<Album>();
                foreach (Album album in enumerable)
                {
                    Console.WriteLine("Album: {0} - {1} - {2}", album.AlbumId, album.Title, (album.Artist == null ? "?" : album.Artist.Name));
                }
            }

            {
                Console.WriteLine("\nQuery DTO");
                AlbumDTO dto = new AlbumDTO();
                enumerableDTO = query
                    .Where(x => x.AlbumId <= 3)
                    .Select(dto.GetDTOSelector());
                foreach (AlbumDTO album in enumerableDTO)
                {
                    Console.WriteLine("Album: {0} - {1} - {2}", album.AlbumId, album.Title, album.ArtistLookupText);
                }
            }

            {
                Console.WriteLine("\nData Model => DTO => View Model => DTO => Data Model");

                // Data Model
                Album data = repository.GetById(1);
                if (data != null)
                {
                    Console.WriteLine("Album Data Model: {0} - {1} - {2} - {3}", data.AlbumId, data.Title, data.ArtistId,
                        (data.Artist == null ? "?" : data.Artist.Name));

                    // Data Model => DTO
                    AlbumDTO dto = new AlbumDTO(data);
                    Console.WriteLine("       Album DTO: {0} - {1} - {2} - {3}", dto.AlbumId, dto.Title, dto.ArtistId,
                        dto.ArtistLookupText);

                    // DTO => View Model
                    AlbumViewModel view = new AlbumViewModel(dto);
                    Console.WriteLine("Album View Model: {0} - {1} - {2} - {3}", view.AlbumId, view.Title, view.ArtistId,
                        view.ArtistLookupText);

                    // Data Model => DTO
                    dto = (AlbumDTO)view.ToDTO();
                    Console.WriteLine("       Album DTO: {0} - {1} - {2} - {3}", dto.AlbumId, dto.Title, dto.ArtistId,
                        dto.ArtistLookupText);

                    // Data Model
                    data = (Album)dto.ToData();
                    Console.WriteLine("Album Data Model: {0} - {1} - {2} - {3}", data.AlbumId, data.Title, data.ArtistId,
                        (data.Artist == null ? "?" : data.Artist.Name));
                }
            }
        }
    }
}