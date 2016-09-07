using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookPlaylistTrackRepositoryMongoDB : ChinookGenericRepositoryMongoDB<PlaylistTrack>
    {
        #region Methods

        public ChinookPlaylistTrackRepositoryMongoDB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override void Join(PlaylistTrack playlistTrack)
        {
            if (playlistTrack != null)
            {
                playlistTrack.Playlist = UnitOfWork.GetRepository<Playlist>().GetById(playlistTrack.PlaylistId);
                playlistTrack.Track = UnitOfWork.GetRepository<Track>().GetById(playlistTrack.TrackId);
            }
        }

        #endregion Methods
    }
}
