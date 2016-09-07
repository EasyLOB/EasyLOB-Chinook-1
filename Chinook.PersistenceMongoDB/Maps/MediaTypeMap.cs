using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public static partial class ChinookMongoDBMap
    {
        public static void MediaTypeMap()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(MediaType)))
            {
                BsonClassMap.RegisterClassMap<MediaType>(map =>
                {
                    map.MapIdProperty(x => x.MediaTypeId)
                        .SetIdGenerator(MongoDBInt32IdGenerator.Instance)
                        .SetSerializer(new Int32Serializer(BsonType.String));

                    map.MapProperty(x => x.Name);
                });
            }
        }
    }
}