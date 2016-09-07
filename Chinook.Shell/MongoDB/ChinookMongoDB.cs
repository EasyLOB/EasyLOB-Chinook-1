using Chinook.Persistence;
using EasyLOB.Library;
using MongoDB.Driver;

namespace Chinook.Shell
{
    public class ChinookMongoDB
    {
        private IMongoDatabase _database;

        public IMongoDatabase Database { get { return _database; } }

        public ChinookMongoDB()
        {
            _database = new MongoClient(LibraryHelper.AppSettings<string>("MongoDB.Chinook.Url"))
                .GetDatabase(LibraryHelper.AppSettings<string>("MongoDB.Chinook.Database"));

            ChinookMongoDBMap.AlbumMap();
            ChinookMongoDBMap.ArtistMap();
            ChinookMongoDBMap.CustomerMap();
            ChinookMongoDBMap.EmployeeMap();
            ChinookMongoDBMap.GenreMap();
            ChinookMongoDBMap.InvoiceMap();
            ChinookMongoDBMap.InvoiceLineMap();
            ChinookMongoDBMap.MediaTypeMap();
            ChinookMongoDBMap.PlaylistMap();
            ChinookMongoDBMap.PlaylistTrackMap();
            ChinookMongoDBMap.TrackMap();

            ChinookMongoDBMap.CustomerDocumentMap();
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }
    }
}