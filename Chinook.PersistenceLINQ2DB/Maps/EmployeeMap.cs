using LinqToDB.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public static partial class ChinookLINQ2DBMap
    {
        public static void EmployeeMap(MappingSchema mappingSchema)
        {
            mappingSchema.GetFluentMappingBuilder().Entity<Employee>()
                .HasTableName("Employee")

                .Property(x => x.EmployeeId)
                    .IsPrimaryKey()
                    .IsIdentity()
                    .HasColumnName("EmployeeId")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(false)

                .Property(x => x.LastName)
                    .HasColumnName("LastName")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(20)
                    .IsNullable(false)

                .Property(x => x.FirstName)
                    .HasColumnName("FirstName")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(20)
                    .IsNullable(false)

                .Property(x => x.Title)
                    .HasColumnName("Title")
                    .HasDataType(LinqToDB.DataType.VarChar)
                    .HasLength(30)
                    .IsNullable(true)

                .Property(x => x.ReportsTo)
                    .HasColumnName("ReportsTo")
                    .HasDataType(LinqToDB.DataType.Int32)
                    .IsNullable(true)

                .Property(x => x.BirthDate)
                    .HasColumnName("BirthDate")
                    .HasDataType(LinqToDB.DataType.DateTime)
                    .IsNullable(true)

                .Property(x => x.HireDate)
                    .HasColumnName("HireDate")
                    .HasDataType(LinqToDB.DataType.DateTime)
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
                    .IsNullable(true)

                .Property(x => x.EmployeeReportsTo)
                    .IsNotColumn()

                .Property(x => x.Customers)
                    .IsNotColumn()

                .Property(x => x.Employees)
                    .IsNotColumn()

                .Property(x => x.LookupText)
                    .IsNotColumn()
                ;
        }
    }
}