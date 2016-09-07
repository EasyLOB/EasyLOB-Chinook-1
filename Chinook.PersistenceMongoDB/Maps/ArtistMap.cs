using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public static partial class ChinookMongoDBMap
    {
        public static void ArtistMap()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Artist)))
            {
                BsonClassMap.RegisterClassMap<Artist>(map =>
                {
                    map.MapIdProperty(x => x.ArtistId)
                        .SetIdGenerator(MongoDBInt32IdGenerator.Instance)
                        .SetSerializer(new Int32Serializer(BsonType.String));

                    map.MapProperty(x => x.Name);
                });
            }
        }
    }
}