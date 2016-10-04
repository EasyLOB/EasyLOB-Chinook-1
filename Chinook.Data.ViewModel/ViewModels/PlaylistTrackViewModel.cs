using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Chinook.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    public partial class PlaylistTrackViewModel : ZViewBase<PlaylistTrackViewModel, PlaylistTrackDTO, PlaylistTrack>
    {
        #region Properties
        
        [Display(Name = "PropertyPlaylistId", ResourceType = typeof(PlaylistTrackResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        //[Column(Order=1)]
        [Required]
        public virtual int PlaylistId { get; set; }
        
        [Display(Name = "PropertyTrackId", ResourceType = typeof(PlaylistTrackResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        //[Column(Order=2)]
        [Required]
        public virtual int TrackId { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string PlaylistLookupText { get; set; } // PlaylistId

        public virtual string TrackLookupText { get; set; } // TrackId
    
        #endregion Associations FK

        #region Methods
        
        public PlaylistTrackViewModel()
        {
            PlaylistId = LibraryDefaults.Default_Int32;
            TrackId = LibraryDefaults.Default_Int32;
        }

        public PlaylistTrackViewModel(
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

        public PlaylistTrackViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public PlaylistTrackViewModel(IZDTOBase<PlaylistTrackDTO, PlaylistTrack> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<PlaylistTrackViewModel, PlaylistTrackDTO> GetDTOSelector()
        {
            return x => new PlaylistTrackDTO
            {
                PlaylistId = x.PlaylistId,
                TrackId = x.TrackId
            };
        }

        public override Func<PlaylistTrackDTO, PlaylistTrackViewModel> GetViewSelector()
        {
            return x => new PlaylistTrackViewModel
            {
                PlaylistId = x.PlaylistId,
                TrackId = x.TrackId,
                PlaylistLookupText = x.PlaylistLookupText,
                TrackLookupText = x.TrackLookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                PlaylistTrackDTO dto = new PlaylistTrackDTO(data);
                PlaylistTrackViewModel view = (new List<PlaylistTrackDTO> { (PlaylistTrackDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<PlaylistTrackDTO, PlaylistTrack> dto)
        {
            if (dto != null)
            {
                PlaylistTrackViewModel view = (new List<PlaylistTrackDTO> { (PlaylistTrackDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<PlaylistTrackDTO, PlaylistTrack> ToDTO()
        {
            return (new List<PlaylistTrackViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
