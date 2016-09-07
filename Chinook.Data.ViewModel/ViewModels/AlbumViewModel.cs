using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Chinook.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    public partial class AlbumViewModel : ZViewBase<AlbumViewModel, AlbumDTO, Album>
    {
        #region Properties
        
        [Display(Name = "PropertyAlbumId", ResourceType = typeof(AlbumResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Range(1, Int32.MaxValue)]
        [Required]
        public virtual int AlbumId { get; set; }
        
        [Display(Name = "PropertyTitle", ResourceType = typeof(AlbumResources))]
        [Required]
        [StringLength(160)]
        public virtual string Title { get; set; }
        
        [Display(Name = "PropertyArtistId", ResourceType = typeof(AlbumResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Range(1, Int32.MaxValue)]
        [Required]
        public virtual int ArtistId { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string ArtistLookupText { get; set; } // ArtistId
    
        #endregion Associations FK

        #region Properties ZViewBase

        public override string LookupText { get; set; }

        #endregion Properties ZViewBase

        #region Methods
        
        public AlbumViewModel()
        {
            AlbumId = 1;
        }
        
        public AlbumViewModel(
            int albumId,
            string title,
            int artistId,
            string artistLookupText = null
        )
            : this()
        {
            AlbumId = albumId;
            Title = title;
            ArtistId = artistId;
            ArtistLookupText = artistLookupText;
        }

        public AlbumViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public AlbumViewModel(IZDTOBase<AlbumDTO, Album> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<AlbumViewModel, AlbumDTO> GetDTOSelector()
        {
            return x => new AlbumDTO
            {
                AlbumId = x.AlbumId,
                Title = x.Title,
                ArtistId = x.ArtistId,
                ArtistLookupText = x.ArtistLookupText,
                LookupText = x.LookupText
            };
        }

        public override Func<AlbumDTO, AlbumViewModel> GetViewSelector()
        {
            return x => new AlbumViewModel
            {
                AlbumId = x.AlbumId,
                Title = x.Title,
                ArtistId = x.ArtistId,
                ArtistLookupText = x.ArtistLookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                AlbumDTO dto = new AlbumDTO(data);
                AlbumViewModel view = (new List<AlbumDTO> { (AlbumDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<AlbumDTO, Album> dto)
        {
            if (dto != null)
            {
                AlbumViewModel view = (new List<AlbumDTO> { (AlbumDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<AlbumDTO, Album> ToDTO()
        {
            return (new List<AlbumViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
