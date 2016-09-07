using System.Linq;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookPlaylistTrackRepositoryLINQ2DB : ChinookGenericRepositoryLINQ2DB<PlaylistTrack>
    {
        #region Methods

        public ChinookPlaylistTrackRepositoryLINQ2DB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override IQueryable<PlaylistTrack> Join(IQueryable<PlaylistTrack> query)
        {
            return
                from playlistTrack in query
                join playlist in UnitOfWork.GetQuery<Playlist>() on playlistTrack.PlaylistId equals playlist.PlaylistId // INNER JOIN
                join track in UnitOfWork.GetQuery<Track>() on playlistTrack.TrackId equals track.TrackId // INNER JOIN
                select new PlaylistTrack
                {
                    PlaylistId = playlistTrack.PlaylistId,
                    TrackId = playlistTrack.TrackId,
                    Playlist = playlist,
                    Track = track
                };
        }

        #endregion Methods
    }
}

