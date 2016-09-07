using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{    
    [MetadataType(typeof(PlaylistTrackDTOMetadata))]    
    public partial class PlaylistTrackDTO : Chinook.Persistence.Chinook.Data.PlaylistTrackDTO, IZDTOBase<PlaylistTrackDTO, PlaylistTrack>
    {
        #region Methods

        public PlaylistTrackDTO()
        {
        }

        public PlaylistTrackDTO(IZDataBase data)
        {
            FromData(data);
        }

        #endregion Methods
        
        #region Methods IZDTOBase
        
        public Func<PlaylistTrackDTO, PlaylistTrack> GetDataSelector()
        {
            return x => new PlaylistTrack
            {
                PlaylistId = x.PlaylistId,
                TrackId = x.TrackId
            };
        }

        public Func<PlaylistTrack, PlaylistTrackDTO> GetDTOSelector()
        {
            return x => new PlaylistTrackDTO
            {
                PlaylistId = x.PlaylistId,
                TrackId = x.TrackId,
                PlaylistLookupText = x.Playlist == null ? "" : x.Playlist.LookupText,
                TrackLookupText = x.Track == null ? "" : x.Track.LookupText
            };
        }

        public void FromData(IZDataBase data)
        {
            PlaylistTrackDTO dto = (new List<PlaylistTrack> { (PlaylistTrack)data })
                .Select(GetDTOSelector())
                .FirstOrDefault();
            LibraryHelper.Clone(dto, this);
        }

        public IZDataBase ToData()
        {
            return (new List<PlaylistTrackDTO> { this })
                .Select(GetDataSelector())
                .FirstOrDefault();
        }

        #endregion Methods IZDTOBase
    }

    public partial class PlaylistTrackDTOMetadata
    {
        #region Properties

        [Key, Column(Order = 0)]
        public virtual int PlaylistId { get; set; }
        [Key, Column(Order = 1)]
        public virtual int TrackId { get; set; }

        #endregion Properties
    }    
}
