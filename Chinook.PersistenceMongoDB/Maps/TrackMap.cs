using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public static partial class ChinookMongoDBMap
    {
        public static void TrackMap()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Track)))
            {
                BsonClassMap.RegisterClassMap<Track>(map =>
                {
                    map.MapIdProperty(x => x.TrackId)
                        .SetIdGenerator(MongoDBInt32IdGenerator.Instance)
                        .SetSerializer(new Int32Serializer(BsonType.String));

                    map.MapProperty(x => x.TrackId);
                    map.MapProperty(x => x.Name);
                    map.MapProperty(x => x.AlbumId);
                    map.MapProperty(x => x.MediaTypeId);
                    map.MapProperty(x => x.GenreId);
                    map.MapProperty(x => x.Composer);
                    map.MapProperty(x => x.Milliseconds);
                    map.MapProperty(x => x.Bytes);
                    map.MapProperty(x => x.UnitPrice);
                });
            }
        }
    }
}