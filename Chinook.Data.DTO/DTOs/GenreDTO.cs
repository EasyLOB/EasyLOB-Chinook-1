using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    public partial class GenreDTO : ZDTOBase<GenreDTO, Genre>
    {
        #region Properties
               
        public virtual int GenreId { get; set; }
               
        public virtual string Name { get; set; }

        #endregion Properties

        #region Methods
        
        public GenreDTO()
        {
            GenreId = LibraryDefaults.Default_Int32;
            Name = null;
        }
        
        public GenreDTO(
            int genreId,
            string name = null
        )
        {
            GenreId = genreId;
            Name = name;
        }

        public GenreDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<GenreDTO, Genre> GetDataSelector()
        {
            return x => new Genre
            {
                GenreId = x.GenreId,
                Name = x.Name
            };
        }

        public override Func<Genre, GenreDTO> GetDTOSelector()
        {
            return x => new GenreDTO
            {
                GenreId = x.GenreId,
                Name = x.Name,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                GenreDTO dto = (new List<Genre> { (Genre)data })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<GenreDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
