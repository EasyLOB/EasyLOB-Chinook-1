using LinqToDB.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public static partial class ChinookLINQ2DBMap
    {
        public static void PlaylistMap(MappingSchema mappingSchema)
        {
            mappingSchema.GetFluentMappingBuilder().Entity<Playlist>()
                .HasTableName("Playlist")

                .Property(x => x.PlaylistId)
                    .IsPrimaryKey()
                    .IsIdentity()
                    .HasColumnName("PlaylistId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.Name)
                    .HasColumnName("Name")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(120)
                    .IsNullable(true)

                .Property(x => x.PlaylistTracks)
                    .IsNotColumn()

                .Property(x => x.LookupText)
                    .IsNotColumn()
                ;
        }
    }
}