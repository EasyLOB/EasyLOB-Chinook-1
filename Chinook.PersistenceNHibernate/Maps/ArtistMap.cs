using FluentNHibernate.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class ArtistMap : ClassMap<Artist>
    {
        public ArtistMap()
        {
            #region Class

            Table("Artist");

            Id(x => x.ArtistId)
                .Column("ArtistId")
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

            HasMany(x => x.Albums)
                .KeyColumn("ArtistId");

            #endregion Collections (PK)
        }
    }
}