using FluentNHibernate.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class PlaylistMap : ClassMap<Playlist>
    {
        public PlaylistMap()
        {
            #region Class

            Table("Playlist");

            Id(x => x.PlaylistId)
                .Column("PlaylistId")
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

            HasMany(x => x.PlaylistTracks)
                .KeyColumn("PlaylistId");

            #endregion Collections (PK)
        }
    }
}