using LinqToDB.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public static partial class ChinookLINQ2DBMap
    {
        public static void GenreMap(MappingSchema mappingSchema)
        {
            mappingSchema.GetFluentMappingBuilder().Entity<Genre>()
                .HasTableName("Genre")

                .Property(x => x.GenreId)
                    .IsPrimaryKey()
                    .IsIdentity()
                    .HasColumnName("GenreId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.Name)
                    .HasColumnName("Name")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(120)
                    .IsNullable(true)

                .Property(x => x.Tracks)
                    .IsNotColumn()

                .Property(x => x.LookupText)
                    .IsNotColumn()
                ;
        }
    }
}