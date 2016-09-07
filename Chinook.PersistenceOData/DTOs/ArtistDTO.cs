using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{    
    [MetadataType(typeof(ArtistDTOMetadata))]    
    public partial class ArtistDTO : Chinook.Persistence.Chinook.Data.ArtistDTO, IZDTOBase<ArtistDTO, Artist>
    {
        #region Methods

        public ArtistDTO()
        {
        }

        public ArtistDTO(IZDataBase data)
        {
            FromData(data);
        }

        #endregion Methods
        
        #region Methods IZDTOBase
        
        public Func<ArtistDTO, Artist> GetDataSelector()
        {
            return x => new Artist
            {
                ArtistId = x.ArtistId,
                Name = x.Name
            };
        }

        public Func<Artist, ArtistDTO> GetDTOSelector()
        {
            return x => new ArtistDTO
            {
                ArtistId = x.ArtistId,
                Name = x.Name
            };
        }

        public void FromData(IZDataBase data)
        {
            ArtistDTO dto = (new List<Artist> { (Artist)data })
                .Select(GetDTOSelector())
                .FirstOrDefault();
            LibraryHelper.Clone(dto, this);
        }

        public IZDataBase ToData()
        {
            return (new List<ArtistDTO> { this })
                .Select(GetDataSelector())
                .FirstOrDefault();
        }

        #endregion Methods IZDTOBase
    }

    public partial class ArtistDTOMetadata
    {
        #region Properties

        [Key]
        public virtual int ArtistId { get; set; }

        #endregion Properties
    }    
}
