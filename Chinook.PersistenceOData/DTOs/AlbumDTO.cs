using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{    
    [MetadataType(typeof(AlbumDTOMetadata))]    
    public partial class AlbumDTO : Chinook.Persistence.Chinook.Data.AlbumDTO, IZDTOBase<AlbumDTO, Album>
    {
        #region Methods

        public AlbumDTO()
        {
        }

        public AlbumDTO(IZDataBase data)
        {
            FromData(data);
        }

        #endregion Methods
        
        #region Methods IZDTOBase
        
        public Func<AlbumDTO, Album> GetDataSelector()
        {
            return x => new Album
            {
                AlbumId = x.AlbumId,
                Title = x.Title,
                ArtistId = x.ArtistId
            };
        }

        public Func<Album, AlbumDTO> GetDTOSelector()
        {
            return x => new AlbumDTO
            {
                AlbumId = x.AlbumId,
                Title = x.Title,
                ArtistId = x.ArtistId,
                ArtistLookupText = x.Artist == null ? "" : x.Artist.LookupText
            };
        }

        public void FromData(IZDataBase data)
        {
            AlbumDTO dto = (new List<Album> { (Album)data })
                .Select(GetDTOSelector())
                .FirstOrDefault();
            LibraryHelper.Clone(dto, this);
        }

        public IZDataBase ToData()
        {
            return (new List<AlbumDTO> { this })
                .Select(GetDataSelector())
                .FirstOrDefault();
        }

        #endregion Methods IZDTOBase
    }

    public partial class AlbumDTOMetadata
    {
        #region Properties

        [Key]
        public virtual int AlbumId { get; set; }

        #endregion Properties
    }    
}
