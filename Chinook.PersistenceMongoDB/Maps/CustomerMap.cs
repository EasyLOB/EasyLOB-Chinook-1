using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public static partial class ChinookMongoDBMap
    {
        public static void CustomerMap()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Customer)))
            {
                BsonClassMap.RegisterClassMap<Customer>(map =>
                {
                    map.MapIdProperty(x => x.CustomerId)
                        .SetIdGenerator(MongoDBInt32IdGenerator.Instance)
                        .SetSerializer(new Int32Serializer(BsonType.String));

                    map.MapProperty(x => x.FirstName);
                    map.MapProperty(x => x.LastName);
                    map.MapProperty(x => x.Company);
                    map.MapProperty(x => x.Address);
                    map.MapProperty(x => x.City);
                    map.MapProperty(x => x.State);
                    map.MapProperty(x => x.Country);
                    map.MapProperty(x => x.PostalCode);
                    map.MapProperty(x => x.Phone);
                    map.MapProperty(x => x.Fax);
                    map.MapProperty(x => x.Email);
                    map.MapProperty(x => x.SupportRepId);
                });
            }
        }
    }
}