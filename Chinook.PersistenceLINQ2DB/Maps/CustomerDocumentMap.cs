using LinqToDB.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public static partial class ChinookLINQ2DBMap
    {
        public static void CustomerDocumentMap(MappingSchema mappingSchema)
        {
            mappingSchema.GetFluentMappingBuilder().Entity<CustomerDocument>()
                .HasTableName("CustomerDocument")

                .Property(x => x.CustomerDocumentId)
                    .IsPrimaryKey()
                    .IsIdentity()
                    .HasColumnName("CustomerDocumentId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.CustomerId)
                    .HasColumnName("CustomerId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.Description)
                    .HasColumnName("Description")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(100)
                    .IsNullable(false)

                .Property(x => x.FileAcronym)
                    .HasColumnName("FileAcronym")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(10)
                    .IsNullable(false)

                .Property(x => x.Customer)
                    .IsNotColumn()

                .Property(x => x.LookupText)
                    .IsNotColumn()
                ;
        }
    }
}