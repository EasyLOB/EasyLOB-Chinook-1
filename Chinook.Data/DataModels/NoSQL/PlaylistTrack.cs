using System.ComponentModel.DataAnnotations.Schema;

namespace Chinook.Data
{
    public partial class PlaylistTrack
    {
        #region Properties NoSQL

        [NotMapped]
        public virtual int PlaylistTrackId
        {
            get { return PlaylistId * 1000000 + TrackId; } // 000 000000
            set { }
        }

        #endregion Properties NoSQL

        #region Methods MongoDB

        public PlaylistTrack(
            int playlistTrackId
        )
            : this()
        {
            PlaylistId = playlistTrackId / 1000000;
            TrackId = playlistTrackId % 1000000;
        }

        #endregion Methods MongoDB
    }
}