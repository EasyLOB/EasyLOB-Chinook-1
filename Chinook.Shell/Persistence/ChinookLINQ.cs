using Chinook.Data;
using Chinook.Persistence;
using EasyLOB;
using EasyLOB.Library;
using EasyLOB.Persistence;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Linq.Dynamic;

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
        private static void PersistenceChinookLINQDemo()
        {
            Console.WriteLine("\nPersistence Chinook LINQ Demo");

            var container = new UnityContainer();
            UnityHelper.RegisterMappings(container);

            IUnitOfWork unitOfWork = (IUnitOfWork)container.Resolve<IChinookUnitOfWork>();
            Console.WriteLine("\n" + unitOfWork.GetType().FullName + " with " + unitOfWork.DBMS.ToString());

            IQueryable<Genre> query = unitOfWork.GetQuery<Genre>();
            IEnumerable<Genre> enumerable;

            Console.WriteLine("\nLINQ");
            enumerable = query
                .Where(x => x.GenreId < 7)
                .OrderByDescending(x => x.GenreId);
            foreach (Genre genre in query)
            {
                Console.WriteLine("{0} {1}", genre.GenreId, genre.Name);
            }

            Console.WriteLine("\nDynamic LINQ");
            enumerable = query
                .Where("GenreId < 7")
                .OrderBy("GenreId descending");
            foreach (Genre genre in enumerable)
            {
                Console.WriteLine("{0} {1}", genre.GenreId, genre.Name);
            }
        }

        private static void PersistenceChinookLINQDataContextDemo()
        {
            Console.WriteLine("\nPersistence Chinook LINQ DataContext Demo");

            //string connectionString = LibraryHelper.ConnectionStrings("Chinook");
            //MyChinookDataContext dataContext = new MyChinookDataContext(connectionString);

            //Console.WriteLine("\nLINQ DataContext");
            //var genres =
            //    from g in dataContext.Genres
            //    select new
            //    {
            //        g.GenreId,
            //        g.Name
            //    };
            //foreach (var genre in genres)
            //{
            //    Console.WriteLine("{0} {1}", genre.GenreId, genre.Name);
            //}
        }
    }

    //[Table("Genre")]
    ////[Table(Name = "Genre")]
    //public class MyGenre : Genre
    //{
    //}

    //public class MyChinookDataContext : DataContext
    //{
    //    public Table<MyGenre> Genres;

    //    public MyChinookDataContext(String connString)
    //        : base(connString)
    //    {
    //    }
    //}
}