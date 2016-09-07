using FluentNHibernate.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class InvoiceLineMap : ClassMap<InvoiceLine>
    {
        public InvoiceLineMap()
        {
            #region Class

            Table("InvoiceLine");

            Id(x => x.InvoiceLineId)
                .Column("InvoiceLineId")
                .CustomSqlType("int")
                .GeneratedBy.Identity()
                .Not.Nullable();

            Not.LazyLoad(); // GetById() EntityProxy => Entity

            #endregion Class

            #region Properties

            Map(x => x.InvoiceId)
                .Column("InvoiceId")
                .CustomSqlType("int")
                .Not.Nullable();

            Map(x => x.TrackId)
                .Column("TrackId")
                .CustomSqlType("int")
                .Not.Nullable();

            Map(x => x.UnitPrice)
                .Column("UnitPrice")
                .CustomSqlType("decimal")
                .Not.Nullable();

            Map(x => x.Quantity)
                .Column("Quantity")
                .CustomSqlType("int")
                .Not.Nullable();

            #endregion Properties

            #region Associations (FK)

            References(x => x.Invoice)
                .Column("InvoiceId");

            References(x => x.Track)
                .Column("TrackId");

            #endregion Associations (FK)
        }
    }
}