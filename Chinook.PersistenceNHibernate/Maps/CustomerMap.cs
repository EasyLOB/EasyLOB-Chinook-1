using FluentNHibernate.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            #region Class

            Table("Customer");

            Id(x => x.CustomerId)
                .Column("CustomerId")
                .CustomSqlType("int")
                .GeneratedBy.Identity()
                .Not.Nullable();

            Not.LazyLoad(); // GetById() EntityProxy => Entity

            #endregion Class

            #region Properties

            Map(x => x.FirstName)
                .Column("FirstName")
                .CustomSqlType("varchar")
                .Length(40)
                .Not.Nullable();

            Map(x => x.LastName)
                .Column("LastName")
                .CustomSqlType("varchar")
                .Length(20)
                .Not.Nullable();

            Map(x => x.Company)
                .Column("Company")
                .CustomSqlType("varchar")
                .Length(80);

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
                .Length(60)
                .Not.Nullable();

            Map(x => x.SupportRepId)
                .Column("SupportRepId")
                .CustomSqlType("int");

            #endregion Properties

            #region Associations (FK)

            References(x => x.Employee)
                .Column("SupportRepId");

            #endregion Associations (FK)

            #region Collections (PK)

            HasMany(x => x.Invoices)
                .KeyColumn("CustomerId");

            #endregion Collections (PK)
        }
    }
}