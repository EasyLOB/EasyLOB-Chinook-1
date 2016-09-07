using System.Data.Entity;
using Chinook.Data;

namespace Chinook.Persistence
{
    public partial class ChinookDbContext : DbContext
    {
        #region Properties

        //public DbSet<ModuleInfo> ModulesInfo { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceLine> InvoiceLines { get; set; }

        public DbSet<MediaType> MediaTypes { get; set; }

        public DbSet<Playlist> Playlists { get; set; }

        public DbSet<PlaylistTrack> PlaylistTracks { get; set; }

        public DbSet<Track> Tracks { get; set; }

        #endregion Properties

        #region Methods

        static ChinookDbContext()
        {
            /*
            // Refer to <configuration><entityframework><contexts> section in Web.config or App.config
            //Database.SetInitializer<ChinookDbContext>(null);
            //Database.SetInitializer<ChinookDbContext>(new CreateDatabaseIfNotExists<ChinookDbContext>());
             */
        }

        public ChinookDbContext()
            : base("Name=Chinook")
        {
            Setup();
        }

        //public ChinookDbContext(string connectionString)
        //    : base(connectionString)
        //{
        //    Setup();
        //}

        //public ChinookDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
        //    : base(objectContext, dbContextOwnsObjectContext)
        //{
        //    Setup();
        //}

        //public ChinookDbContext(DbConnection connection)
        //    : base(connection, false)
        //{
        //    Setup();
        //}

        private void Setup()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.Log = null;
            //Database.Log = Console.Write;
            //Database.Log = log => EntityFrameworkHelper.Log(log, ZLibrary.ZDatabaseLogger.File);
            //Database.Log = log => EntityFrameworkHelper.Log(log, ZLibrary.ZDatabaseLogger.NLog);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ModuleInfo>().Map(t =>
            //{
            //    t.ToTable("ModuleInfo");
            //});

            modelBuilder.Configurations.Add(new AlbumConfiguration());
            modelBuilder.Configurations.Add(new ArtistConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new CustomerDocumentConfiguration());
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new InvoiceConfiguration());
            modelBuilder.Configurations.Add(new InvoiceLineConfiguration());
            modelBuilder.Configurations.Add(new MediaTypeConfiguration());
            modelBuilder.Configurations.Add(new PlaylistConfiguration());
            modelBuilder.Configurations.Add(new PlaylistTrackConfiguration());
            modelBuilder.Configurations.Add(new TrackConfiguration());
        }

        #endregion Methods
    }
}