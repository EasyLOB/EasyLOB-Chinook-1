using FluentNHibernate.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class InvoiceMap : ClassMap<Invoice>
    {
        public InvoiceMap()
        {
            #region Class

            Table("Invoice");

            Id(x => x.InvoiceId)
                .Column("InvoiceId")
                .CustomSqlType("int")
                .GeneratedBy.Identity()
                .Not.Nullable();

            Not.LazyLoad(); // GetById() EntityProxy => Entity

            #endregion Class

            #region Properties

            Map(x => x.CustomerId)
                .Column("CustomerId")
                .CustomSqlType("int")
                .Not.Nullable();

            Map(x => x.InvoiceDate)
                .Column("InvoiceDate")
                .CustomSqlType("datetime")
                .Not.Nullable();

            Map(x => x.BillingAddress)
                .Column("BillingAddress")
                .CustomSqlType("varchar")
                .Length(70);

            Map(x => x.BillingCity)
                .Column("BillingCity")
                .CustomSqlType("varchar")
                .Length(40);

            Map(x => x.BillingState)
                .Column("BillingState")
                .CustomSqlType("varchar")
                .Length(40);

            Map(x => x.BillingCountry)
                .Column("BillingCountry")
                .CustomSqlType("varchar")
                .Length(40);

            Map(x => x.BillingPostalCode)
                .Column("BillingPostalCode")
                .CustomSqlType("varchar")
                .Length(10);

            Map(x => x.Total)
                .Column("Total")
                .CustomSqlType("decimal")
                .Not.Nullable();

            #endregion Properties

            #region Associations (FK)

            References(x => x.Customer)
                .Column("CustomerId");

            #endregion Associations (FK)

            #region Collections (PK)

            HasMany(x => x.InvoiceLines)
                .KeyColumn("InvoiceId");

            #endregion Collections (PK)
        }
    }
}