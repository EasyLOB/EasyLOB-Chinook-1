using FluentNHibernate.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class GenreMap : ClassMap<Genre>
    {
        public GenreMap()
        {
            #region Class

            Table("Genre");

            Id(x => x.GenreId)
                .Column("GenreId")
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
                .KeyColumn("GenreId");

            #endregion Collections (PK)
        }
    }
}