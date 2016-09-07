using LinqToDB.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public static partial class ChinookLINQ2DBMap
    {
        public static void CustomerMap(MappingSchema mappingSchema)
        {
            mappingSchema.GetFluentMappingBuilder().Entity<Customer>()
                .HasTableName("Customer")

                .Property(x => x.CustomerId)
                    .IsPrimaryKey()
                    .IsIdentity()
                    .HasColumnName("CustomerId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.FirstName)
                    .HasColumnName("FirstName")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(40)
                    .IsNullable(false)

                .Property(x => x.LastName)
                    .HasColumnName("LastName")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(20)
                    .IsNullable(false)

                .Property(x => x.Company)
                    .HasColumnName("Company")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(80)
                    .IsNullable(true)

                .Property(x => x.Address)
                    .HasColumnName("Address")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(70)
                    .IsNullable(true)

                .Property(x => x.City)
                    .HasColumnName("City")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(40)
                    .IsNullable(true)

                .Property(x => x.State)
                    .HasColumnName("State")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(40)
                    .IsNullable(true)

                .Property(x => x.Country)
                    .HasColumnName("Country")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(40)
                    .IsNullable(true)

                .Property(x => x.PostalCode)
                    .HasColumnName("PostalCode")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(10)
                    .IsNullable(true)

                .Property(x => x.Phone)
                    .HasColumnName("Phone")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(24)
                    .IsNullable(true)

                .Property(x => x.Fax)
                    .HasColumnName("Fax")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(24)
                    .IsNullable(true)

                .Property(x => x.Email)
                    .HasColumnName("Email")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(60)
                    .IsNullable(false)

                .Property(x => x.SupportRepId)
                    .HasColumnName("SupportRepId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(true)

                .Property(x => x.Employee)
                    .IsNotColumn()

                .Property(x => x.CustomerDocuments)
                    .IsNotColumn()

                .Property(x => x.Invoices)
                    .IsNotColumn()

                .Property(x => x.LookupText)
                    .IsNotColumn()
                ;
        }
    }
}