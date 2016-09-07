using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    public partial class PlaylistTrackDTO : ZDTOBase<PlaylistTrackDTO, PlaylistTrack>
    {
        #region Properties
               
        public virtual int PlaylistId { get; set; }
               
        public virtual int TrackId { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string PlaylistLookupText { get; set; } // PlaylistId

        public virtual string TrackLookupText { get; set; } // TrackId
    
        #endregion Associations FK

        #region Properties ZDTOBase

        public override string LookupText { get; set; }

        #endregion Properties ZDTOBase

        #region Methods
        
        public PlaylistTrackDTO()
        {
        }
        
        public PlaylistTrackDTO(
            int playlistId,
            int trackId,
            string playlistLookupText = null,
            string trackLookupText = null
        )
        {
            PlaylistId = playlistId;
            TrackId = trackId;
            PlaylistLookupText = playlistLookupText;
            TrackLookupText = trackLookupText;
        }

        public PlaylistTrackDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<PlaylistTrackDTO, PlaylistTrack> GetDataSelector()
        {
            return x => new PlaylistTrack
            {
                PlaylistId = x.PlaylistId,
                TrackId = x.TrackId
            };
        }

        public override Func<PlaylistTrack, PlaylistTrackDTO> GetDTOSelector()
        {
            return x => new PlaylistTrackDTO
            {
                PlaylistId = x.PlaylistId,
                TrackId = x.TrackId,
                PlaylistLookupText = x.Playlist == null ? "" : x.Playlist.LookupText,
                TrackLookupText = x.Track == null ? "" : x.Track.LookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                PlaylistTrackDTO dto = (new List<PlaylistTrack> { (PlaylistTrack)data })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<PlaylistTrackDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
