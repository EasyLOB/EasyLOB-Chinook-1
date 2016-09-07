using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{    
    [MetadataType(typeof(MediaTypeDTOMetadata))]    
    public partial class MediaTypeDTO : Chinook.Persistence.Chinook.Data.MediaTypeDTO, IZDTOBase<MediaTypeDTO, MediaType>
    {
        #region Methods

        public MediaTypeDTO()
        {
        }

        public MediaTypeDTO(IZDataBase data)
        {
            FromData(data);
        }

        #endregion Methods
        
        #region Methods IZDTOBase
        
        public Func<MediaTypeDTO, MediaType> GetDataSelector()
        {
            return x => new MediaType
            {
                MediaTypeId = x.MediaTypeId,
                Name = x.Name
            };
        }

        public Func<MediaType, MediaTypeDTO> GetDTOSelector()
        {
            return x => new MediaTypeDTO
            {
                MediaTypeId = x.MediaTypeId,
                Name = x.Name
            };
        }

        public void FromData(IZDataBase data)
        {
            MediaTypeDTO dto = (new List<MediaType> { (MediaType)data })
                .Select(GetDTOSelector())
                .FirstOrDefault();
            LibraryHelper.Clone(dto, this);
        }

        public IZDataBase ToData()
        {
            return (new List<MediaTypeDTO> { this })
                .Select(GetDataSelector())
                .FirstOrDefault();
        }

        #endregion Methods IZDTOBase
    }

    public partial class MediaTypeDTOMetadata
    {
        #region Properties

        [Key]
        public virtual int MediaTypeId { get; set; }

        #endregion Properties
    }    
}
