using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public static partial class ChinookMongoDBMap
    {
        public static void EmployeeMap()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Employee)))
            {
                BsonClassMap.RegisterClassMap<Employee>(map =>
                {
                    map.MapIdProperty(x => x.EmployeeId)
                        .SetIdGenerator(MongoDBInt32IdGenerator.Instance)
                        .SetSerializer(new Int32Serializer(BsonType.String));

                    map.MapProperty(x => x.LastName);
                    map.MapProperty(x => x.FirstName);
                    map.MapProperty(x => x.Title);
                    map.MapProperty(x => x.ReportsTo);
                    map.MapProperty(x => x.BirthDate);
                    map.MapProperty(x => x.HireDate);
                    map.MapProperty(x => x.Address);
                    map.MapProperty(x => x.City);
                    map.MapProperty(x => x.State);
                    map.MapProperty(x => x.Country);
                    map.MapProperty(x => x.PostalCode);
                    map.MapProperty(x => x.Phone);
                    map.MapProperty(x => x.Fax);
                    map.MapProperty(x => x.Email);
                });
            }
        }
    }
}