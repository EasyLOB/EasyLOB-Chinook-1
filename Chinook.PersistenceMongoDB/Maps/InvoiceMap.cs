using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public static partial class ChinookMongoDBMap
    {
        public static void InvoiceMap()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Invoice)))
            {
                BsonClassMap.RegisterClassMap<Invoice>(map =>
                {
                    map.MapIdProperty(x => x.InvoiceId)
                        .SetIdGenerator(MongoDBInt32IdGenerator.Instance)
                        .SetSerializer(new Int32Serializer(BsonType.String));

                    map.MapProperty(x => x.CustomerId);
                    map.MapProperty(x => x.InvoiceDate);
                    map.MapProperty(x => x.BillingAddress);
                    map.MapProperty(x => x.BillingCity);
                    map.MapProperty(x => x.BillingState);
                    map.MapProperty(x => x.BillingCountry);
                    map.MapProperty(x => x.BillingPostalCode);
                    map.MapProperty(x => x.Total);
                });
            }
        }
    }
}