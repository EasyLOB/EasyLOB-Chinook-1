using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public static partial class ChinookMongoDBMap
    {
        public static void AlbumMap()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Album)))
            {
                BsonClassMap.RegisterClassMap<Album>(map =>
                {
                    map.MapIdProperty(x => x.AlbumId)
                        .SetIdGenerator(MongoDBInt32IdGenerator.Instance)
                        .SetSerializer(new Int32Serializer(BsonType.String));

                    map.MapProperty(x => x.Title);
                    map.MapProperty(x => x.ArtistId);
                });
            }
        }
    }
}