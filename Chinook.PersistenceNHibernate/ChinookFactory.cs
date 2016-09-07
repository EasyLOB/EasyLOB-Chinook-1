using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Chinook.Persistence
{
    public static class ChinookFactory
    {
        #region Fields

        private static object _lock = new object();

        #endregion Fields

        #region Properties

        public static string ConnectionString { get { return "Chinook"; } }

        private static ISessionFactory _sessionFactory = null;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = Fluently.Configure()
                        .Database(MsSqlConfiguration
                            .MsSql2008
                            .ConnectionString(x => x.FromConnectionStringWithKey(ConnectionString))
                        )
                        .ExposeConfiguration(x => new SchemaExport(x).Create(false, false))
                        .Mappings(x => x.FluentMappings
                            .Add<AlbumMap>()
                            .Add<ArtistMap>()
                            .Add<CustomerMap>()
                            .Add<CustomerDocumentMap>()
                            .Add<EmployeeMap>()
                            .Add<GenreMap>()
                            .Add<InvoiceMap>()
                            .Add<InvoiceLineMap>()
                            .Add<MediaTypeMap>()
                            .Add<PlaylistMap>()
                            .Add<PlaylistTrackMap>()
                            .Add<TrackMap>()
                        )
                        //.Mappings(x => x.FluentMappings.AddFromAssemblyOf<?Map>())
                        //.Mappings(x => x.FluentMappings.AddFromAssembly(typeof(Chinook.Persistence.?Map).Assembly))
                        //.Mappings(x => x.FluentMappings.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly()))
                        .BuildSessionFactory();
                }

                return _sessionFactory;
            }
        }

        private static ISession _session = null;

        public static ISession Session
        {
            get
            {
                if (_session == null)
                {
                    lock (_lock) // Singleton
                    {
                        if (_session == null)
                        {
                            _session = SessionFactory.OpenSession();
                        }
                    }
                }

                return _session;
            }
        }

        #endregion Properties
    }
}