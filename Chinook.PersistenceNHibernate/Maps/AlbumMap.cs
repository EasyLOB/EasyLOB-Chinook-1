using FluentNHibernate.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class AlbumMap : ClassMap<Album>
    {
        public AlbumMap()
        {
            #region Class

            Table("Album");

            Id(x => x.AlbumId)
                .Column("AlbumId")
                .CustomSqlType("int")
                .GeneratedBy.Identity()
                .Not.Nullable();

            Not.LazyLoad(); // GetById() EntityProxy => Entity

            #endregion Class

            #region Properties

            Map(x => x.Title)
                .Column("Title")
                .CustomSqlType("varchar")
                .Length(160)
                .Not.Nullable();

            Map(x => x.ArtistId)
                .Column("ArtistId")
                .CustomSqlType("int")
                .Not.Nullable();

            #endregion Properties

            #region Associations (FK)

            References(x => x.Artist)
                .Column("ArtistId");

            #endregion Associations (FK)

            #region Collections (PK)

            HasMany(x => x.Tracks)
                .KeyColumn("AlbumId");

            #endregion Collections (PK)
        }
    }
}