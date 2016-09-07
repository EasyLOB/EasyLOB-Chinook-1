using Chinook.Data;
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
        private static void PersistenceChinookAlbumDemo()
        {
            Console.WriteLine("\nPersistence Chinook Album Demo");

            var container = new UnityContainer();
            UnityHelper.RegisterMappings(container);

            IUnitOfWork unitOfWork = (IUnitOfWork)container.Resolve<IChinookUnitOfWork>();
            Console.WriteLine("\n" + unitOfWork.GetType().FullName + " with " + unitOfWork.DBMS.ToString() + "\n");

            IGenericRepository<Album> repository = unitOfWork.GetRepository<Album>();

            {
                Album album = repository.GetById(1);
                if (album != null)
                {
                    Console.WriteLine("Album: {0} - {1} - {2}", album.AlbumId, album.Title,
                        (album.Artist == null ? "?" : album.Artist.Name));
                }
                else
                {
                    Console.WriteLine("\nAlbum: null");
                }
            }

            {
                IEnumerable<Album> albums = repository.Select(null, null, null, null, 20).ToList<Album>();
                int index = 0;
                Console.WriteLine();
                foreach (Album album in albums)
                {
                    Console.WriteLine("Album {0}: {1} - {2} - {3}", index++, album.AlbumId, album.Title,
                        (album.Artist == null ? "?" : album.Artist.Name));

                    if (album.Tracks != null)
                    {
                        foreach (Track track in album.Tracks)
                        {
                            Console.WriteLine("Tracks: {0} - {1}", track.TrackId, track.Name);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tracks: null");
                    }
                }
            }
        }
    }
}