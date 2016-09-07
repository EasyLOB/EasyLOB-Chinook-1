using LinqToDB.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public static partial class ChinookLINQ2DBMap
    {
        public static void ArtistMap(MappingSchema mappingSchema)
        {
            mappingSchema.GetFluentMappingBuilder().Entity<Artist>()
                .HasTableName("Artist")

                .Property(x => x.ArtistId)
                    .IsPrimaryKey()
                    .IsIdentity()
                    .HasColumnName("ArtistId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.Name)
                    .HasColumnName("Name")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(120)
                    .IsNullable(true)

                .Property(x => x.Albums)
                    .IsNotColumn()

                .Property(x => x.LookupText)
                    .IsNotColumn()
                ;
        }
    }
}