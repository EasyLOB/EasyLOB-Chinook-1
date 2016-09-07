using LinqToDB.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public static partial class ChinookLINQ2DBMap
    {
        public static void InvoiceMap(MappingSchema mappingSchema)
        {
            mappingSchema.GetFluentMappingBuilder().Entity<Invoice>()
                .HasTableName("Invoice")

                .Property(x => x.InvoiceId)
                    .IsPrimaryKey()
                    .IsIdentity()
                    .HasColumnName("InvoiceId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.CustomerId)
                    .HasColumnName("CustomerId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.InvoiceDate)
                    .HasColumnName("InvoiceDate")
                    .HasDataType(LinqToDB.DataType.DateTime)
                    .IsNullable(false)

                .Property(x => x.BillingAddress)
                    .HasColumnName("BillingAddress")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(70)
                    .IsNullable(true)

                .Property(x => x.BillingCity)
                    .HasColumnName("BillingCity")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(40)
                    .IsNullable(true)

                .Property(x => x.BillingState)
                    .HasColumnName("BillingState")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(40)
                    .IsNullable(true)

                .Property(x => x.BillingCountry)
                    .HasColumnName("BillingCountry")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(40)
                    .IsNullable(true)

                .Property(x => x.BillingPostalCode)
                    .HasColumnName("BillingPostalCode")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(10)
                    .IsNullable(true)

                .Property(x => x.Total)
                    .HasColumnName("Total")
                    .HasDataType(LinqToDB.DataType.Decimal)
                    .IsNullable(false)

                .Property(x => x.Customer)
                    .IsNotColumn()

                .Property(x => x.InvoiceLines)
                    .IsNotColumn()

                .Property(x => x.LookupText)
                    .IsNotColumn()
                ;
        }
    }
}