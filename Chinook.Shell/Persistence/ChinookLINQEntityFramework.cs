using Chinook.Data;
using Chinook.Persistence;
using EasyLOB.Data;
using EasyLOB.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

// Loading Related Entities
// https://msdn.microsoft.com/en-us/data/jj574232.aspx

namespace Chinook.Shell
{
    partial class Program
    {
        public static void PersistenceLINQEntityFrameworkDemo()
        {
            Console.WriteLine("\nPersistence Chinook LINQ Entity Framework Demo\n");

            ChinookDbContext context = new ChinookDbContext();

            // EF < 6.0 Log
            //context.Configuration.LazyLoadingEnabled = false; // DEFAULT
            //context.Configuration.ProxyCreationEnabled = false; // DEFAULT
            //context.Database.Log = log => EntityFrameworkHelper.Log(log, ZDatabaseLogger.File);

            // EF 6.0 Log
            DbInterception.Add(new NLogCommandInterceptor()); // Entity Framework

            IQueryable<Album> query;
            IEnumerable<Album> collection;
            Album album;
            JsonSerializerSettings settings;
            string json;

            // LINQ - Query Syntax

            query =
                from x in context.Albums
                where x.AlbumId == 1
                orderby x.Title descending
                select x;
            collection = query
                .ToList();
            Console.WriteLine(collection.Count().ToString() + " Album(s).");
            settings = new JsonSerializerSettings
            {
                //Formatting = Formatting.Indented,
                Formatting = Formatting.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            json = JsonConvert.SerializeObject(collection, settings);

            query =
                from x in context.Albums
                where x.AlbumId == 1
                orderby x.Title descending
                select x;
            collection = query
                .Include(x => x.Artist) // System.Data.Entity
                                        //.Include("Artist") // System.Data.Entity
                                        //.Include(x => x.Tracks) // System.Data.Entity
                .Include("Tracks") // System.Data.Entity
                .ToList();
            Console.WriteLine(collection.Count().ToString() + " Album(s) by " +
                collection.FirstOrDefault().Artist.Name + " with " +
                collection.FirstOrDefault().Tracks.Count().ToString() + " Track(s).");

            // A circular reference was detected while serializing an object of type 'Chinook.Album'.
            //json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(collection.ToArray<Album>());

            // Self referencing loop detected with type 'Chinook.Album'. Path '[0].Artist.Albums'.
            //json = JsonConvert.SerializeObject(ie);

            // Newtonsoft.Json :-)
            settings = new JsonSerializerSettings
            {
                //Formatting = Formatting.Indented,
                Formatting = Formatting.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            json = JsonConvert.SerializeObject(collection, settings);

            // LINQ - Lambda Syntax

            query = context.Albums
                .Where(x => x.AlbumId == 1)
                .OrderByDescending(x => x.Title);
            collection = query
                .ToList();
            Console.WriteLine(collection.Count().ToString() + " Album(s).");

            // The method 'Skip' is only supported for sorted input in LINQ to Entities.
            // The method 'OrderBy' must be called before the method 'Skip'.
            query = context.Albums
                .Where(x => x.AlbumId >= 1)
                .OrderByDescending(x => x.Title)
                .Skip(0)
                .Take(10);
            collection = query
                .ToList();
            Console.WriteLine(collection.Count().ToString() + " Album(s).");

            query = context.Albums
                .Where(x => x.AlbumId == 1)
                .OrderByDescending(x => x.Title)
                .Include(x => x.Artist) // System.Data.Entity
                                        //.Include("Artist") // System.Data.Entity
                                        //.Include(x => x.Tracks) // System.Data.Entity
                .Include("Tracks"); // System.Data.Entity
            collection = query
                .ToList();
            Console.WriteLine(collection.Count().ToString() + " Album(s) by " +
                collection.FirstOrDefault().Artist.Name + " with " +
                collection.FirstOrDefault().Tracks.Count().ToString() + " Track(s).");

            album = query.FirstOrDefault(); // SQL
            context.Entry(album).Reference(x => x.Artist).Load(); // SQL
            context.Entry(album).Collection(x => x.Tracks).Load(); // SQL
            Console.WriteLine("\nAlbum [" + album.Title +
                "] by [" + album.Artist.Name +
                "] with " + album.Tracks.Count() + " Track(s).");

            // Dynamic LINQ - Lambda Syntax

            query = context.Albums
                .Where("AlbumId == @0", 1)
                .OrderBy("Title descending");
            collection = query
                .ToList();
            Console.WriteLine(collection.Count().ToString() + " Album(s).");

            query = context.Albums
                .Where("AlbumId == @0", 1)
                .OrderBy("Title descending");
            collection = query
                .Include("Artist") // System.Data.Entity
                .Include("Tracks") // System.Data.Entity
                .ToList();
            Console.WriteLine(collection.Count().ToString() + " Album(s) by " +
                collection.FirstOrDefault().Artist.Name + " with " +
                collection.FirstOrDefault().Tracks.Count().ToString() + " Track(s).");

            album = query.FirstOrDefault(); // SQL
            context.Entry(album).Reference("Artist").Load(); // SQL
            context.Entry(album).Collection("Tracks").Load(); // SQL
            Console.WriteLine("\nAlbum [" + album.Title +
                "] by [" + album.Artist.Name +
                "] with " + album.Tracks.Count() + " Track(s).");
        }

        public static void PersistenceLINQEntityFrameworkUnitOfWorkDemo()
        {
            Console.WriteLine("\nPersistence LINQ Entity Framework UnitOfWork Demo\n");

            IUnitOfWork unitOfWork = new ChinookUnitOfWorkEF();
            unitOfWork.DatabaseLogger = ZDatabaseLogger.File;

            IGenericRepository<Album> repository = unitOfWork.GetRepository<Album>();

            IQueryable<Album> query;
            IEnumerable<Album> collection;
            string json;

            // Count()

            Console.WriteLine("Count(): " + repository.CountAll().ToString() + " Album(s).");

            // LINQ - Select()

            Expression<Func<Album, bool>> where;
            Func<IQueryable<Album>, IOrderedQueryable<Album>> orderBy;
            List<string> associationStrings = new List<string>();
            List<Expression<Func<Album, object>>> associationExpressions = new List<Expression<Func<Album, object>>>();

            where = (x => x.AlbumId == 1);
            Console.WriteLine("Count(): " + repository.Count(where).ToString() + " Album(s).");

            query =
                from x in unitOfWork.GetQuery<Album>()
                where x.AlbumId == 1
                orderby x.Title descending
                select x;
            collection = query
                .ToList();
            Console.WriteLine("UnitOfWork.GetQuery<Album>() Count(): " + collection.Count().ToString() + " Album(s).");

            query =
                from x in repository.Query
                where x.AlbumId == 1
                orderby x.Title descending
                select x;
            collection = query
                .ToList();
            Console.WriteLine("Repository.Query Count(): " + collection.Count().ToString() + " Album(s).");

            // Album ClassDictionary.Associations = { "Artist" }
            where = (x => x.AlbumId == 1);
            orderBy = (o => o.OrderByDescending(x => x.Title));
            collection = repository.Select(where, orderBy);
            Console.WriteLine(collection.Count().ToString() + " Album(s) by " +
                collection.FirstOrDefault().Artist.Name + ".");

            // Album ClassDictionary.Associations = { "Artist" }
            // Album ClassDictionary.OrderByExpression = "Title"
            // The method 'Skip' is only supported for sorted input in LINQ to Entities.
            // The method 'OrderBy' must be called before the method 'Skip'.
            where = (x => x.AlbumId >= 1);
            //orderBy = (q => q.OrderByDescending(x => x.Title));
            collection = repository.Select(where, orderBy, 0, 10);
            Console.WriteLine(collection.Count().ToString() + " Album(s).");

            where = (x => x.AlbumId == 1);
            //orderBy = (q => q.OrderByDescending(x => x.Title));
            //associations.Clear();
            //associations.Add("Artist");
            //associations.Add("Tracks");
            //collection = repository.Select(where, orderBy, null, null, association.ToArray());
            associationExpressions.Clear();
            associationExpressions.Add(x => x.Artist);
            associationExpressions.Add(x => x.Tracks);
            collection = repository.Select(where, orderBy, null, null, associationExpressions);
            Console.WriteLine(collection.Count().ToString() + " Album(s) by " +
                collection.FirstOrDefault().Artist.Name + " with " +
                collection.FirstOrDefault().Tracks.Count().ToString() + " Track(s).");

            // A circular reference was detected while serializing an object of type 'Chinook.Album'.
            //json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(collection.ToArray<Album>());

            // Self referencing loop detected with type 'Chinook.Album'. Path '[0].Artist.Albums'.
            //json = JsonConvert.SerializeObject(ie);

            // Newtonsoft.Json :-)
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                //Formatting = Formatting.Indented,
                Formatting = Formatting.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            json = JsonConvert.SerializeObject(collection, settings);

            // Dynamic LINQ - Select()

            Console.WriteLine("Count(): " + repository.Count("AlbumId == @0", new object[] { 1 }).ToString() + " Album(s).");

            // Album ClassDictionary.Associations = { "Artist" }
            // Album ClassDictionary.OrderByExpression = "Title"
            collection = repository.Select("AlbumId == @0", new object[] { 1 }, "Title descending");
            Console.WriteLine(collection.Count().ToString() + " Album(s) by " +
                collection.FirstOrDefault().Artist.Name + ".");

            // Album ClassDictionary.Associations = { "Artist" }
            // Album ClassDictionary.OrderByExpression = "Title"
            // The method 'Skip' is only supported for sorted input in LINQ to Entities.
            // The method 'OrderBy' must be called before the method 'Skip'.
            collection = repository.Select("AlbumId >= @0", new object[] { 1 }, "Title descending", 0, 10);
            Console.WriteLine(collection.Count().ToString() + " Album(s).");

            collection = repository.Select("AlbumId == @0", new object[] { 1 }, "Title descending", null, null, new string[] { "Artist", "Tracks" });
            Console.WriteLine(collection.Count().ToString() + " Album(s) by " +
                collection.FirstOrDefault().Artist.Name + " with " +
                collection.FirstOrDefault().Tracks.Count().ToString() + " Track(s).");
        }

        public static void DemoLINQSQL()
        {
            Console.WriteLine("\nLINQ -> SQL\n");

            ChinookDbContext context = new ChinookDbContext();

            IQueryable<Album> query;
            string sql;

            // LINQ - Lambda Syntax

            query = context.Albums
                .Where(x => x.AlbumId < 10)
                .OrderByDescending(x => x.Title)
                .Include(x => x.Artist);
            //.Include("Tracks");
            sql = query.ToString();
            Console.WriteLine(sql);

            //SELECT
            //    [Extent1].[AlbumId] AS [AlbumId],
            //    [Extent1].[Title] AS [Title],
            //    [Extent1].[ArtistId] AS [ArtistId],
            //    [Extent2].[ArtistId] AS [ArtistId1],
            //    [Extent2].[Name] AS [Name]
            //FROM
            //    [dbo].[Album] AS [Extent1]
            //    INNER JOIN [dbo].[Artist] AS [Extent2] ON
            //        [Extent1].[ArtistId] = [Extent2].[ArtistId]
            //WHERE
            //    [Extent1].[AlbumId] < 10
            //ORDER BY
            //    [Extent1].[Title] DESC

            // Dynamic LINQ - Lambda Syntax

            query = context.Albums
                 .Where("AlbumId < @0", 10)
                 .OrderBy("Title descending")
                 .Include(x => x.Artist);
            //.Include("Tracks");
            sql = query.ToString();
            Console.WriteLine(sql);
        }
    }
}