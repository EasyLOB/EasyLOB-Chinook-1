using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public static partial class ChinookMongoDBMap
    {
        public static void CustomerDocumentMap()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(CustomerDocument)))
            {
                BsonClassMap.RegisterClassMap<CustomerDocument>(map =>
                {
                    map.MapIdProperty(x => x.CustomerDocumentId)
                        .SetIdGenerator(MongoDBInt32IdGenerator.Instance)
                        .SetSerializer(new Int32Serializer(BsonType.String));

                    map.MapProperty(x => x.CustomerId);
                    map.MapProperty(x => x.Description);
                    map.MapProperty(x => x.FileAcronym);
                });
            }
        }
    }
}