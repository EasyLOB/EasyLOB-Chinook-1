using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public static partial class ChinookMongoDBMap
    {
        public static void InvoiceLineMap()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(InvoiceLine)))
            {
                BsonClassMap.RegisterClassMap<InvoiceLine>(map =>
                {
                    map.MapIdProperty(x => x.InvoiceLineId)
                        .SetIdGenerator(MongoDBInt32IdGenerator.Instance)
                        .SetSerializer(new Int32Serializer(BsonType.String));
                    map.MapIdProperty(x => x.InvoiceLineId);

                    map.MapProperty(x => x.InvoiceId);
                    map.MapProperty(x => x.TrackId);
                    map.MapProperty(x => x.UnitPrice);
                    map.MapProperty(x => x.Quantity);
                });
            }
        }
    }
}