using FluentNHibernate.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class TrackMap : ClassMap<Track>
    {
        public TrackMap()
        {
            #region Class

            Table("Track");

            Id(x => x.TrackId)
                .Column("TrackId")
                .CustomSqlType("int")
                .GeneratedBy.Identity()
                .Not.Nullable();

            Not.LazyLoad(); // GetById() EntityProxy => Entity

            #endregion Class

            #region Properties

            Map(x => x.Name)
                .Column("Name")
                .CustomSqlType("varchar")
                .Length(200)
                .Not.Nullable();

            Map(x => x.AlbumId)
                .Column("AlbumId")
                .CustomSqlType("int");

            Map(x => x.MediaTypeId)
                .Column("MediaTypeId")
                .CustomSqlType("int")
                .Not.Nullable();

            Map(x => x.GenreId)
                .Column("GenreId")
                .CustomSqlType("int");

            Map(x => x.Composer)
                .Column("Composer")
                .CustomSqlType("varchar")
                .Length(220);

            Map(x => x.Milliseconds)
                .Column("Milliseconds")
                .CustomSqlType("int")
                .Not.Nullable();

            Map(x => x.Bytes)
                .Column("Bytes")
                .CustomSqlType("int");

            Map(x => x.UnitPrice)
                .Column("UnitPrice")
                .CustomSqlType("decimal")
                .Not.Nullable();

            #endregion Properties

            #region Associations (FK)

            References(x => x.Album)
                .Column("AlbumId");

            References(x => x.Genre)
                .Column("GenreId");

            References(x => x.MediaType)
                .Column("MediaTypeId");

            #endregion Associations (FK)

            #region Collections (PK)

            HasMany(x => x.InvoiceLines)
                .KeyColumn("TrackId");

            HasMany(x => x.PlaylistTracks)
                .KeyColumn("TrackId");

            #endregion Collections (PK)
        }
    }
}