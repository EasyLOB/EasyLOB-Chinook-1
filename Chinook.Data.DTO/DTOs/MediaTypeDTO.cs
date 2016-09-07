using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    public partial class MediaTypeDTO : ZDTOBase<MediaTypeDTO, MediaType>
    {
        #region Properties
               
        public virtual int MediaTypeId { get; set; }
               
        public virtual string Name { get; set; }

        #endregion Properties

        #region Properties ZDTOBase

        public override string LookupText { get; set; }

        #endregion Properties ZDTOBase

        #region Methods
        
        public MediaTypeDTO()
        {
        }
        
        public MediaTypeDTO(
            int mediaTypeId,
            string name = null
        )
        {
            MediaTypeId = mediaTypeId;
            Name = name;
        }

        public MediaTypeDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<MediaTypeDTO, MediaType> GetDataSelector()
        {
            return x => new MediaType
            {
                MediaTypeId = x.MediaTypeId,
                Name = x.Name
            };
        }

        public override Func<MediaType, MediaTypeDTO> GetDTOSelector()
        {
            return x => new MediaTypeDTO
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
                MediaTypeDTO dto = (new List<MediaType> { (MediaType)data })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<MediaTypeDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
