using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Chinook.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    public partial class MediaTypeViewModel : ZViewBase<MediaTypeViewModel, MediaTypeDTO, MediaType>
    {
        #region Properties
        
        [Display(Name = "PropertyMediaTypeId", ResourceType = typeof(MediaTypeResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Range(1, Int32.MaxValue)]
        [Required]
        public virtual int MediaTypeId { get; set; }
        
        [Display(Name = "PropertyName", ResourceType = typeof(MediaTypeResources))]
        [StringLength(120)]
        public virtual string Name { get; set; }

        #endregion Properties

        #region Properties ZViewBase

        public override string LookupText { get; set; }

        #endregion Properties ZViewBase

        #region Methods
        
        public MediaTypeViewModel()
        {
            MediaTypeId = 1;
        }
        
        public MediaTypeViewModel(
            int mediaTypeId,
            string name = null
        )
            : this()
        {
            MediaTypeId = mediaTypeId;
            Name = name;
        }

        public MediaTypeViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public MediaTypeViewModel(IZDTOBase<MediaTypeDTO, MediaType> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<MediaTypeViewModel, MediaTypeDTO> GetDTOSelector()
        {
            return x => new MediaTypeDTO
            {
                MediaTypeId = x.MediaTypeId,
                Name = x.Name,
                LookupText = x.LookupText
            };
        }

        public override Func<MediaTypeDTO, MediaTypeViewModel> GetViewSelector()
        {
            return x => new MediaTypeViewModel
            {
                MediaTypeId = x.MediaTypeId,
                Name = x.Name,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                MediaTypeDTO dto = new MediaTypeDTO(data);
                MediaTypeViewModel view = (new List<MediaTypeDTO> { (MediaTypeDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<MediaTypeDTO, MediaType> dto)
        {
            if (dto != null)
            {
                MediaTypeViewModel view = (new List<MediaTypeDTO> { (MediaTypeDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<MediaTypeDTO, MediaType> ToDTO()
        {
            return (new List<MediaTypeViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
