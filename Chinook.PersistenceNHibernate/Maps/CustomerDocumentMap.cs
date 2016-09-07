using FluentNHibernate.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class CustomerDocumentMap : ClassMap<CustomerDocument>
    {
        public CustomerDocumentMap()
        {
            #region Class

            Table("CustomerDocument");

            Id(x => x.CustomerDocumentId)
                .Column("CustomerDocumentId")
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

            Map(x => x.Description)
                .Column("Description")
                .CustomSqlType("varchar")
                .Length(100);

            Map(x => x.FileAcronym)
                .Column("FileAcronym")
                .CustomSqlType("varchar")
                .Length(10);

            #endregion Properties

            #region Associations (FK)

            References(x => x.Customer)
                .Column("CustomerId");

            #endregion Associations (FK)
        }
    }
}