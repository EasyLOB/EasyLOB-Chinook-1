using LinqToDB.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public static partial class ChinookLINQ2DBMap
    {
        public static void PlaylistTrackMap(MappingSchema mappingSchema)
        {
            mappingSchema.GetFluentMappingBuilder().Entity<PlaylistTrack>()
                .HasTableName("PlaylistTrack")

                .Property(x => x.PlaylistId)
                    .IsPrimaryKey(1)
                    .HasColumnName("PlaylistId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.TrackId)
                    .IsPrimaryKey(2)
                    .HasColumnName("TrackId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.Playlist)
                    .IsNotColumn()

                .Property(x => x.Track)
                    .IsNotColumn()

                .Property(x => x.LookupText)
                    .IsNotColumn()

                .Property(x => x.PlaylistTrackId) // !!!
                    .IsNotColumn()
                ;
        }
    }
}