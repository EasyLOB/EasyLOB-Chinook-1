using Chinook.Data;
using Chinook.Persistence;
using EasyLOB;
using EasyLOB.Library;
using EasyLOB.Persistence;
using Microsoft.Practices.Unity;
using System;
using System.Linq;

// http://stackoverflow.com/questions/13692015/how-to-rewrite-this-linq-using-join-with-lambda-expressions

//from a in AA
//join b in BB on
//a.Y equals b.Y
//select new {a, b}

//AA.Join(
//  BB,
//  a => a.Y, b => b.Y
//  (a, b) => new {a, b})

namespace Chinook.Shell
{
    partial class Program
    {
        private static void PersistenceChinookLINQJoinDemo()
        {
            Console.WriteLine("\nPersistence Chinook LINQ Join Demo");

            var container = new UnityContainer();
            UnityHelper.RegisterMappings(container);

            IUnitOfWork unitOfWork = (IUnitOfWork)container.Resolve<IChinookUnitOfWork>();
            Console.WriteLine("\n" + unitOfWork.GetType().FullName + " with " + unitOfWork.DBMS.ToString());

            IGenericRepository<Album> repository = unitOfWork.GetRepository<Album>();

            IQueryable<Album> albums = unitOfWork.GetQuery<Album>();
            IQueryable<Track> tracks = unitOfWork.GetQuery<Track>();

            var result1 = albums
                .Join(tracks, a => a.AlbumId, t => t.AlbumId, (a, t) => new { a, t })
                .Where(x => x.a.AlbumId <= 3)
                .OrderByDescending(x => x.a.Title)
                .Select(x => new { x.a, x.t });
            Console.WriteLine();
            foreach (object o in result1)
            {
                //Console.WriteLine(o.ToString()); // { a = ZData.Chinook.Album, t = Chinook.Data.Track }
                Album album = (Album)LibraryHelper.GetPropertyValue(o, "a");
                Track track = (Track)LibraryHelper.GetPropertyValue(o, "t");
                Console.WriteLine(album.AlbumId + " - " + album.Title + " : " + track.Name);
            }

            var result2 =
                from a in albums
                join t in tracks on a.AlbumId equals t.AlbumId
                where a.AlbumId <= 3
                orderby a.Title descending
                select new { a, t };
            Console.WriteLine();
            foreach (object o in result2)
            {
                //Console.WriteLine(o.ToString()); // { a = ZData.Chinook.Album, t = Chinook.Data.Track }
                Album album = (Album)LibraryHelper.GetPropertyValue(o, "a");
                Track track = (Track)LibraryHelper.GetPropertyValue(o, "t");
                Console.WriteLine(album.AlbumId + " - " + album.Title + " : " + track.Name);
            }
        }
    }
}