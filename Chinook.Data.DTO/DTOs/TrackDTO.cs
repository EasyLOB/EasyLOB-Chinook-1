using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    public partial class TrackDTO : ZDTOBase<TrackDTO, Track>
    {
        #region Properties
               
        public virtual int TrackId { get; set; }
               
        public virtual string Name { get; set; }
               
        public virtual int? AlbumId { get; set; }
               
        public virtual int MediaTypeId { get; set; }
               
        public virtual int? GenreId { get; set; }
               
        public virtual string Composer { get; set; }
               
        public virtual int Milliseconds { get; set; }
               
        public virtual int? Bytes { get; set; }
               
        public virtual decimal UnitPrice { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string AlbumLookupText { get; set; } // AlbumId

        public virtual string GenreLookupText { get; set; } // GenreId

        public virtual string MediaTypeLookupText { get; set; } // MediaTypeId
    
        #endregion Associations FK

        #region Properties ZDTOBase

        public override string LookupText { get; set; }

        #endregion Properties ZDTOBase

        #region Methods
        
        public TrackDTO()
        {
        }
        
        public TrackDTO(
            int trackId,
            string name,
            int mediaTypeId,
            int milliseconds,
            decimal unitPrice,
            int? albumId = null,
            int? genreId = null,
            string composer = null,
            int? bytes = null,
            string albumLookupText = null,
            string genreLookupText = null,
            string mediaTypeLookupText = null
        )
        {
            TrackId = trackId;
            Name = name;
            AlbumId = albumId;
            MediaTypeId = mediaTypeId;
            GenreId = genreId;
            Composer = composer;
            Milliseconds = milliseconds;
            Bytes = bytes;
            UnitPrice = unitPrice;
            AlbumLookupText = albumLookupText;
            GenreLookupText = genreLookupText;
            MediaTypeLookupText = mediaTypeLookupText;
        }

        public TrackDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<TrackDTO, Track> GetDataSelector()
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

        public override Func<Track, TrackDTO> GetDTOSelector()
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
                MediaTypeLookupText = x.MediaType == null ? "" : x.MediaType.LookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                TrackDTO dto = (new List<Track> { (Track)data })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<TrackDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
