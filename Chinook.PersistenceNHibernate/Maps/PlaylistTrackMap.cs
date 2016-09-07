using FluentNHibernate.Mapping;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class PlaylistTrackMap : ClassMap<PlaylistTrack>
    {
        public PlaylistTrackMap()
        {
            #region Class

            Table("PlaylistTrack");

            CompositeId()
                .KeyProperty(x => x.PlaylistId, "PlaylistId")
                .KeyProperty(x => x.TrackId, "TrackId");

            #endregion Class

            #region Associations (FK)

            References(x => x.Playlist)
                .Column("PlaylistId");

            References(x => x.Track)
                .Column("TrackId");

            #endregion Associations (FK)
        }
    }
}