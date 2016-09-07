using LinqToDB.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public static partial class ChinookLINQ2DBMap
    {
        public static void TrackMap(MappingSchema mappingSchema)
        {
            mappingSchema.GetFluentMappingBuilder().Entity<Track>()
                .HasTableName("Track")

                .Property(x => x.TrackId)
                    .IsPrimaryKey()
                    .IsIdentity()
                    .HasColumnName("TrackId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.Name)
                    .HasColumnName("Name")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(200)
                    .IsNullable(false)

                .Property(x => x.AlbumId)
                    .HasColumnName("AlbumId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(true)

                .Property(x => x.MediaTypeId)
                    .HasColumnName("MediaTypeId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.GenreId)
                    .HasColumnName("GenreId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(true)

                .Property(x => x.Composer)
                    .HasColumnName("Composer")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(220)
                    .IsNullable(true)

                .Property(x => x.Milliseconds)
                    .HasColumnName("Milliseconds")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.Bytes)
                    .HasColumnName("Bytes")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(true)

                .Property(x => x.UnitPrice)
                    .HasColumnName("UnitPrice")
                    .HasDataType(LinqToDB.DataType.Decimal)
                    .IsNullable(false)

                .Property(x => x.Album)
                    .IsNotColumn()

                .Property(x => x.Genre)
                    .IsNotColumn()

                .Property(x => x.MediaType)
                    .IsNotColumn()

                .Property(x => x.InvoiceLines)
                    .IsNotColumn()

                .Property(x => x.PlaylistTracks)
                    .IsNotColumn()

                .Property(x => x.LookupText)
                    .IsNotColumn()
                ;
        }
    }
}