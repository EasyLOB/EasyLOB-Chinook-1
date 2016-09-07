using Chinook.Data;
using Chinook.Persistence;
using EasyLOB.Persistence;
using Newtonsoft.Json;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

// Loading Related Entities
// https://msdn.microsoft.com/en-us/data/jj574232.aspx

namespace Chinook.Shell
{
    partial class Program
    {
        private static void PersistenceChinookLINQNHibernateDemo()
        {
            Console.WriteLine("\nPersistence Chinook LINQ NHibernate Demo\n");

            ISession session = ChinookFactory.Session;

            IQueryable<Album> query;
            IEnumerable<Album> collection;
            //Album a;
            //string json;

            // LINQ - Query Syntax

            query =
                from x in session.Query<Album>()
                where x.AlbumId == 1
                orderby x.Title descending
                select x;
            collection = query
                .ToList();
            Console.WriteLine(collection.Count().ToString() + " Album(s).");
            //json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(ie); // collection.ToArray<Album>()

            query =
                from x in session.Query<Album>()
                where x.AlbumId == 1
                orderby x.Title descending
                select x;
            collection = query
                .Fetch(x => x.Artist)
                .Fetch(x => x.Tracks)
                .ToList();
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
                // "Error getting value from 'DefaultValue' on 'NHibernate.Type.DateTimeOffsetType'."
                ContractResolver = new NHibernateContractResolver(),
                //Formatting = Formatting.Indented,
                Formatting = Formatting.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            // !?!
            //json = JsonConvert.SerializeObject(ie, settings);

            // LINQ - Lambda Syntax

            query = session.Query<Album>()
                .Where(x => x.AlbumId == 1)
                .OrderByDescending(x => x.Title);
            collection = query
                .ToList();
            Console.WriteLine(collection.Count().ToString() + " Album(s).");

            // The method 'Skip' is only supported for sorted input in LINQ to Entities.
            // The method 'OrderBy' must be called before the method 'Skip'.
            query = session.Query<Album>()
                .Where(x => x.AlbumId >= 1)
                .OrderByDescending(x => x.Title)
                .Skip(0)
                .Take(10);
            collection = query
                .ToList();
            Console.WriteLine(collection.Count().ToString() + " Album(s).");

            query = session.Query<Album>()
                .Where(x => x.AlbumId == 1)
                .OrderByDescending(x => x.Title)
                .Fetch(x => x.Artist)
                .Fetch(x => x.Tracks);
            collection = query
                .ToList();
            Console.WriteLine(collection.Count().ToString() + " Album(s) by " +
                collection.FirstOrDefault().Artist.Name + " with " +
                collection.FirstOrDefault().Tracks.Count().ToString() + " Track(s).");

            //a = query.FirstOrDefault(); // SQL
            //context.Entry(a).Reference(x => x.Artist).Load(); // SQL
            //context.Entry(a).Collection(x => x.Tracks).Load(); // SQL
            //Console.WriteLine("\nAlbum [" + a.Title +
            //    "] by [" + a.Artist.Name +
            //    "] with " + a.Tracks.Count() + " Track(s).");

            // Dynamic LINQ - Lambda Syntax

            Expression<Func<Album, Artist>> includeArtist = System.Linq.Dynamic.DynamicExpression.ParseLambda<Album, Artist>("Artist");
            Expression<Func<Album, List<Track>>> includeTracks = System.Linq.Dynamic.DynamicExpression.ParseLambda<Album, List<Track>>("Tracks");

            query = session.Query<Album>()
                .Where("AlbumId == @0", 1)
                .OrderBy("Title descending");
            collection = query
                .ToList();
            Console.WriteLine(collection.Count().ToString() + " Album(s).");

            query = session.Query<Album>()
                .Where("AlbumId == @0", 1)
                .OrderBy("Title descending");
            collection = query
                .Fetch(includeArtist)
                .Fetch(includeTracks)
                .ToList();
            Console.WriteLine(collection.Count().ToString() + " Album(s) by " +
                collection.FirstOrDefault().Artist.Name + " with " +
                collection.FirstOrDefault().Tracks.Count().ToString() + " Track(s).");

            //a = query.FirstOrDefault(); // SQL
            //context.Entry(a).Reference("Artist").Load(); // SQL
            //context.Entry(a).Collection("Tracks").Load(); // SQL
            //Console.WriteLine("\nAlbum [" + a.Title +
            //    "] by [" + a.Artist.Name +
            //    "] with " + a.Tracks.Count() + " Track(s).");
        }

        public static void PersistenceLINQNHibernateUnitOfWorkDemo()
        {
            Console.WriteLine("\nPersistence LINQ NHibernate UnitOfWork Demo\n");

            IUnitOfWork unitOfWork = new ChinookUnitOfWorkNH();
            // !?!
            //unitOfWork.SetLogger(DatabaseLogger.File);

            IGenericRepository<Album> repository = unitOfWork.GetRepository<Album>();

            IQueryable<Album> query;
            IEnumerable<Album> collection;
            //string json;

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
            orderBy = (q => q.OrderByDescending(x => x.Title));
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
                // "Error getting value from 'DefaultValue' on 'NHibernate.Type.DateTimeOffsetType'."
                ContractResolver = new NHibernateContractResolver(),
                //Formatting = Formatting.Indented,
                Formatting = Formatting.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            // !?!
            //json = JsonConvert.SerializeObject(ie, settings);

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
    }
}