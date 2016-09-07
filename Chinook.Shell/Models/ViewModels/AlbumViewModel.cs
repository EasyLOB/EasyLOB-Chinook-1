using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Chinook.Data;
using Chinook.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Mvc
{
    public partial class AlbumViewModel : ZViewBase<AlbumViewModel, AlbumDTO, Album>
    {
        #region Properties
        
        [Display(Name = "PropertyAlbumId", ResourceType = typeof(AlbumResources))]
        //[Key]
        [Required]
        public virtual int AlbumId { get; set; }
        
        [Display(Name = "PropertyTitle", ResourceType = typeof(AlbumResources))]
        [Required]
        [StringLength(160)]
        public virtual string Title { get; set; }
        
        [Display(Name = "PropertyArtistId", ResourceType = typeof(AlbumResources))]
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
        }
        
        public AlbumViewModel(
            int albumId,
            string title,
            int artistId,
            string artistLookupText = null
        )
        {
            AlbumId = albumId;
            Title = title;
            ArtistId = artistId;
            ArtistLookupText = artistLookupText;
        }

        public AlbumViewModel(IZDataBase data)
        {
            AlbumDTO dto = new AlbumDTO(data);
            AlbumViewModel view = (new List<AlbumDTO> { (AlbumDTO)dto })
                .Select(GetViewSelector())
                .SingleOrDefault();
            LibraryHelper.Clone(view, this);            
        }

        public AlbumViewModel(IZDTOBase<AlbumDTO, Album> dto)
        {
            AlbumViewModel view = (new List<AlbumDTO> { (AlbumDTO)dto })
                .Select(GetViewSelector())
                .SingleOrDefault();
            LibraryHelper.Clone(view, this);
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
