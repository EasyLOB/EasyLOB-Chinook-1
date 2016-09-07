using FluentNHibernate.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class MediaTypeMap : ClassMap<MediaType>
    {
        public MediaTypeMap()
        {
            #region Class

            Table("MediaType");

            Id(x => x.MediaTypeId)
                .Column("MediaTypeId")
                .CustomSqlType("int")
                .GeneratedBy.Identity()
                .Not.Nullable();

            Not.LazyLoad(); // GetById() EntityProxy => Entity

            #endregion Class

            #region Properties

            Map(x => x.Name)
                .Column("Name")
                .CustomSqlType("varchar")
                .Length(120);

            #endregion Properties

            #region Collections (PK)

            HasMany(x => x.Tracks)
                .KeyColumn("MediaTypeId");

            #endregion Collections (PK)
        }
    }
}