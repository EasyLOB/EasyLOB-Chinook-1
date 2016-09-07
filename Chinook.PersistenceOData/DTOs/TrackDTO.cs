using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{    
    [MetadataType(typeof(TrackDTOMetadata))]    
    public partial class TrackDTO : Chinook.Persistence.Chinook.Data.TrackDTO, IZDTOBase<TrackDTO, Track>
    {
        #region Methods

        public TrackDTO()
        {
        }

        public TrackDTO(IZDataBase data)
        {
            FromData(data);
        }

        #endregion Methods
        
        #region Methods IZDTOBase
        
        public Func<TrackDTO, Track> GetDataSelector()
        {
            return x => new Track
            {
                TrackId = x.TrackId,
                Name = x.Name,
                AlbumId = x.AlbumId,
                MediaTypeId = x.MediaTypeId,
                GenreId = x.GenreId,
                Composer = x.Composer,
                Milliseconds = x.Milliseconds,
                Bytes = x.Bytes,
                UnitPrice = x.UnitPrice
            };
        }

        public Func<Track, TrackDTO> GetDTOSelector()
        {
            return x => new TrackDTO
            {
                TrackId = x.TrackId,
                Name = x.Name,
                AlbumId = x.AlbumId,
                MediaTypeId = x.MediaTypeId,
                GenreId = x.GenreId,
                Composer = x.Composer,
                Milliseconds = x.Milliseconds,
                Bytes = x.Bytes,
                UnitPrice = x.UnitPrice,
                AlbumLookupText = x.Album == null ? "" : x.Album.LookupText,
                GenreLookupText = x.Genre == null ? "" : x.Genre.LookupText,
                MediaTypeLookupText = x.MediaType == null ? "" : x.MediaType.LookupText
            };
        }

        public void FromData(IZDataBase data)
        {
            TrackDTO dto = (new List<Track> { (Track)data })
                .Select(GetDTOSelector())
                .FirstOrDefault();
            LibraryHelper.Clone(dto, this);
        }

        public IZDataBase ToData()
        {
            return (new List<TrackDTO> { this })
                .Select(GetDataSelector())
                .FirstOrDefault();
        }

        #endregion Methods IZDTOBase
    }

    public partial class TrackDTOMetadata
    {
        #region Properties

        [Key]
        public virtual int TrackId { get; set; }

        #endregion Properties
    }    
}
