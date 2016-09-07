using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public static partial class ChinookMongoDBMap
    {
        public static void GenreMap()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Genre)))
            {
                BsonClassMap.RegisterClassMap<Genre>(map =>
                {
                    map.MapIdProperty(x => x.GenreId)
                        .SetIdGenerator(MongoDBInt32IdGenerator.Instance)
                        .SetSerializer(new Int32Serializer(BsonType.String));

                    map.MapProperty(x => x.Name);
                });
            }
        }
    }
}