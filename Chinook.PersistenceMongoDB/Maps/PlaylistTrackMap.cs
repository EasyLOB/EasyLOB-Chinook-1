using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Chinook.Data;
using EasyLOB.Data;

namespace Chinook.Persistence
{
    public static partial class ChinookMongoDBMap
    {
        public static void PlaylistTrackMap()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(PlaylistTrack)))
            {
                BsonClassMap.RegisterClassMap<PlaylistTrack>(map =>
                {
                    map.MapIdProperty(x => x.PlaylistTrackId); // !!!

                    map.MapProperty(x => x.PlaylistId);
                    map.MapProperty(x => x.TrackId);
                });
            }
        }
    }
}