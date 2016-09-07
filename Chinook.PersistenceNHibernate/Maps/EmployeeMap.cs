using FluentNHibernate.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            #region Class

            Table("Employee");

            Id(x => x.EmployeeId)
                .Column("EmployeeId")
                .CustomSqlType("int")
                .GeneratedBy.Identity()
                .Not.Nullable();

            Not.LazyLoad(); // GetById() EntityProxy => Entity

            #endregion Class

            #region Properties

            Map(x => x.LastName)
                .Column("LastName")
                .CustomSqlType("varchar")
                .Length(20)
                .Not.Nullable();

            Map(x => x.FirstName)
                .Column("FirstName")
                .CustomSqlType("varchar")
                .Length(20)
                .Not.Nullable();

            Map(x => x.Title)
                .Column("Title")
                .CustomSqlType("varchar")
                .Length(30);

            Map(x => x.ReportsTo)
                .Column("ReportsTo")
                .CustomSqlType("int");

            Map(x => x.BirthDate)
                .Column("BirthDate")
                .CustomSqlType("datetime");

            Map(x => x.HireDate)
                .Column("HireDate")
                .CustomSqlType("datetime");

            Map(x => x.Address)
                .Column("Address")
                .CustomSqlType("varchar")
                .Length(70);

            Map(x => x.City)
                .Column("City")
                .CustomSqlType("varchar")
                .Length(40);

            Map(x => x.State)
                .Column("State")
                .CustomSqlType("varchar")
                .Length(40);

            Map(x => x.Country)
                .Column("Country")
                .CustomSqlType("varchar")
                .Length(40);

            Map(x => x.PostalCode)
                .Column("PostalCode")
                .CustomSqlType("varchar")
                .Length(10);

            Map(x => x.Phone)
                .Column("Phone")
                .CustomSqlType("varchar")
                .Length(24);

            Map(x => x.Fax)
                .Column("Fax")
                .CustomSqlType("varchar")
                .Length(24);

            Map(x => x.Email)
                .Column("Email")
                .CustomSqlType("varchar")
                .Length(60);

            #endregion Properties

            #region Associations (FK)

            References(x => x.EmployeeReportsTo)
                .Column("ReportsTo");

            #endregion Associations (FK)

            #region Collections (PK)

            HasMany(x => x.Customers)
                .KeyColumn("SupportRepId");

            HasMany(x => x.Employees)
                .KeyColumn("ReportsTo");

            #endregion Collections (PK)
        }
    }
}