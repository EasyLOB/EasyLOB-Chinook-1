using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public static partial class ChinookMongoDBMap
    {
        public static void PlaylistMap()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Playlist)))
            {
                BsonClassMap.RegisterClassMap<Playlist>(map =>
                {
                    map.MapIdProperty(x => x.PlaylistId)
                        .SetIdGenerator(MongoDBInt32IdGenerator.Instance)
                        .SetSerializer(new Int32Serializer(BsonType.String));

                    map.MapProperty(x => x.Name);
                });
            }
        }
    }
}