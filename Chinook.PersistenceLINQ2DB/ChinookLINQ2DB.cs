using LinqToDB;
using LinqToDB.Data;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class ChinookLINQ2DB : DataConnection
    {
        public ChinookLINQ2DB()
            : base("Chinook")
        {
            ChinookLINQ2DBMap.AlbumMap(MappingSchema);
            ChinookLINQ2DBMap.ArtistMap(MappingSchema);
            ChinookLINQ2DBMap.CustomerMap(MappingSchema);
            ChinookLINQ2DBMap.EmployeeMap(MappingSchema);
            ChinookLINQ2DBMap.GenreMap(MappingSchema);
            ChinookLINQ2DBMap.InvoiceMap(MappingSchema);
            ChinookLINQ2DBMap.InvoiceLineMap(MappingSchema);
            ChinookLINQ2DBMap.MediaTypeMap(MappingSchema);
            ChinookLINQ2DBMap.PlaylistMap(MappingSchema);
            ChinookLINQ2DBMap.PlaylistTrackMap(MappingSchema);
            ChinookLINQ2DBMap.TrackMap(MappingSchema);
        }

        public ITable<Album> Album
        {
            get { return GetTable<Album>(); }
        }

        public ITable<Artist> Artist
        {
            get { return GetTable<Artist>(); }
        }

        public ITable<Customer> Customer
        {
            get { return GetTable<Customer>(); }
        }

        public ITable<Employee> Employee
        {
            get { return GetTable<Employee>(); }
        }

        public ITable<Genre> Genre
        {
            get { return GetTable<Genre>(); }
        }

        public ITable<Invoice> Invoice
        {
            get { return GetTable<Invoice>(); }
        }

        public ITable<InvoiceLine> InvoiceLine
        {
            get { return GetTable<InvoiceLine>(); }
        }

        public ITable<MediaType> MediaType
        {
            get { return GetTable<MediaType>(); }
        }

        public ITable<Playlist> Playlist
        {
            get { return GetTable<Playlist>(); }
        }

        public ITable<PlaylistTrack> PlaylistTrack
        {
            get { return GetTable<PlaylistTrack>(); }
        }

        public ITable<Track> Track
        {
            get { return GetTable<Track>(); }
        }
    }
}