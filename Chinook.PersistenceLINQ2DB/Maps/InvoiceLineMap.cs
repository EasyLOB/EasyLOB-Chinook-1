using LinqToDB.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public static partial class ChinookLINQ2DBMap
    {
        public static void InvoiceLineMap(MappingSchema mappingSchema)
        {
            mappingSchema.GetFluentMappingBuilder().Entity<InvoiceLine>()
                .HasTableName("InvoiceLine")

                .Property(x => x.InvoiceLineId)
                    .IsPrimaryKey()
                    .IsIdentity()
                    .HasColumnName("InvoiceLineId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.InvoiceId)
                    .HasColumnName("InvoiceId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.TrackId)
                    .HasColumnName("TrackId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.UnitPrice)
                    .HasColumnName("UnitPrice")
                    .HasDataType(LinqToDB.DataType.Decimal)
                    .IsNullable(false)

                .Property(x => x.Quantity)
                    .HasColumnName("Quantity")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.Invoice)
                    .IsNotColumn()

                .Property(x => x.Track)
                    .IsNotColumn()

                .Property(x => x.LookupText)
                    .IsNotColumn()
                ;
        }
    }
}