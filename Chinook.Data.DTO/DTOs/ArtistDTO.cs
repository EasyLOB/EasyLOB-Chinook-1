using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    public partial class ArtistDTO : ZDTOBase<ArtistDTO, Artist>
    {
        #region Properties
               
        public virtual int ArtistId { get; set; }
               
        public virtual string Name { get; set; }

        #endregion Properties

        #region Properties ZDTOBase

        public override string LookupText { get; set; }

        #endregion Properties ZDTOBase

        #region Methods
        
        public ArtistDTO()
        {
        }
        
        public ArtistDTO(
            int artistId,
            string name = null
        )
        {
            ArtistId = artistId;
            Name = name;
        }

        public ArtistDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<ArtistDTO, Artist> GetDataSelector()
        {
            return x => new Artist
            {
                ArtistId = x.ArtistId,
                Name = x.Name
            };
        }

        public override Func<Artist, ArtistDTO> GetDTOSelector()
        {
            return x => new ArtistDTO
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
                ArtistDTO dto = (new List<Artist> { (Artist)data })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<ArtistDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
