using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{    
    [MetadataType(typeof(GenreDTOMetadata))]    
    public partial class GenreDTO : Chinook.Persistence.Chinook.Data.GenreDTO, IZDTOBase<GenreDTO, Genre>
    {
        #region Methods

        public GenreDTO()
        {
        }

        public GenreDTO(IZDataBase data)
        {
            FromData(data);
        }

        #endregion Methods
        
        #region Methods IZDTOBase
        
        public Func<GenreDTO, Genre> GetDataSelector()
        {
            return x => new Genre
            {
                GenreId = x.GenreId,
                Name = x.Name
            };
        }

        public Func<Genre, GenreDTO> GetDTOSelector()
        {
            return x => new GenreDTO
            {
                GenreId = x.GenreId,
                Name = x.Name
            };
        }

        public void FromData(IZDataBase data)
        {
            GenreDTO dto = (new List<Genre> { (Genre)data })
                .Select(GetDTOSelector())
                .FirstOrDefault();
            LibraryHelper.Clone(dto, this);
        }

        public IZDataBase ToData()
        {
            return (new List<GenreDTO> { this })
                .Select(GetDataSelector())
                .FirstOrDefault();
        }

        #endregion Methods IZDTOBase
    }

    public partial class GenreDTOMetadata
    {
        #region Properties

        [Key]
        public virtual int GenreId { get; set; }

        #endregion Properties
    }    
}
