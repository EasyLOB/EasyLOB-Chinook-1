using LinqToDB.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public static partial class ChinookLINQ2DBMap
    {
        public static void AlbumMap(MappingSchema mappingSchema)
        {
            mappingSchema.GetFluentMappingBuilder().Entity<Album>()
                .HasTableName("Album")

                .Property(x => x.AlbumId)
                    .IsPrimaryKey()
                    .IsIdentity()
                    .HasColumnName("AlbumId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.Title)
                    .HasColumnName("Title")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(160)
                    .IsNullable(false)

                .Property(x => x.ArtistId)
                    .HasColumnName("ArtistId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.Artist)
                    .IsNotColumn()

                .Property(x => x.Tracks)
                    .IsNotColumn()

                .Property(x => x.LookupText)
                    .IsNotColumn()
                ;
        }
    }
}