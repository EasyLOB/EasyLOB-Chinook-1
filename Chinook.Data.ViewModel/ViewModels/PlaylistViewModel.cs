using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Chinook.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    public partial class PlaylistViewModel : ZViewBase<PlaylistViewModel, PlaylistDTO, Playlist>
    {
        #region Properties
        
        [Display(Name = "PropertyPlaylistId", ResourceType = typeof(PlaylistResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Required]
        public virtual int PlaylistId { get; set; }
        
        [Display(Name = "PropertyName", ResourceType = typeof(PlaylistResources))]
        [StringLength(120)]
        public virtual string Name { get; set; }

        #endregion Properties

        #region Methods
        
        public PlaylistViewModel()
        {
            PlaylistId = LibraryDefaults.Default_Int32;
            Name = null;
        }
        
        public PlaylistViewModel(
            int playlistId,
            string name = null
        )
        {
            PlaylistId = playlistId;
            Name = name;
        }

        public PlaylistViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public PlaylistViewModel(IZDTOBase<PlaylistDTO, Playlist> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<PlaylistViewModel, PlaylistDTO> GetDTOSelector()
        {
            return x => new PlaylistDTO
            {
                PlaylistId = x.PlaylistId,
                Name = x.Name
            };
        }

        public override Func<PlaylistDTO, PlaylistViewModel> GetViewSelector()
        {
            return x => new PlaylistViewModel
            {
                PlaylistId = x.PlaylistId,
                Name = x.Name,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                PlaylistDTO dto = new PlaylistDTO(data);
                PlaylistViewModel view = (new List<PlaylistDTO> { (PlaylistDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<PlaylistDTO, Playlist> dto)
        {
            if (dto != null)
            {
                PlaylistViewModel view = (new List<PlaylistDTO> { (PlaylistDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<PlaylistDTO, Playlist> ToDTO()
        {
            return (new List<PlaylistViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
