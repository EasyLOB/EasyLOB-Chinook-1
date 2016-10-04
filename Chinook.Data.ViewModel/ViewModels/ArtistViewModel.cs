using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Chinook.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    public partial class ArtistViewModel : ZViewBase<ArtistViewModel, ArtistDTO, Artist>
    {
        #region Properties
        
        [Display(Name = "PropertyArtistId", ResourceType = typeof(ArtistResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Required]
        public virtual int ArtistId { get; set; }
        
        [Display(Name = "PropertyName", ResourceType = typeof(ArtistResources))]
        [StringLength(120)]
        public virtual string Name { get; set; }

        #endregion Properties

        #region Methods
        
        public ArtistViewModel()
        {
            ArtistId = LibraryDefaults.Default_Int32;
            Name = null;
        }
        
        public ArtistViewModel(
            int artistId,
            string name = null
        )
        {
            ArtistId = artistId;
            Name = name;
        }

        public ArtistViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public ArtistViewModel(IZDTOBase<ArtistDTO, Artist> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<ArtistViewModel, ArtistDTO> GetDTOSelector()
        {
            return x => new ArtistDTO
            {
                ArtistId = x.ArtistId,
                Name = x.Name
            };
        }

        public override Func<ArtistDTO, ArtistViewModel> GetViewSelector()
        {
            return x => new ArtistViewModel
            {
                ArtistId = x.ArtistId,
                Name = x.Name,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                ArtistDTO dto = new ArtistDTO(data);
                ArtistViewModel view = (new List<ArtistDTO> { (ArtistDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<ArtistDTO, Artist> dto)
        {
            if (dto != null)
            {
                ArtistViewModel view = (new List<ArtistDTO> { (ArtistDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<ArtistDTO, Artist> ToDTO()
        {
            return (new List<ArtistViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
