using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    public partial class AlbumDTO : ZDTOBase<AlbumDTO, Album>
    {
        #region Properties
               
        public virtual int AlbumId { get; set; }
               
        public virtual string Title { get; set; }
               
        public virtual int ArtistId { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string ArtistLookupText { get; set; } // ArtistId
    
        #endregion Associations FK

        #region Properties ZDTOBase

        public override string LookupText { get; set; }

        #endregion Properties ZDTOBase

        #region Methods
        
        public AlbumDTO()
        {
        }
        
        public AlbumDTO(
            int albumId,
            string title,
            int artistId,
            string artistLookupText = null
        )
        {
            AlbumId = albumId;
            Title = title;
            ArtistId = artistId;
            ArtistLookupText = artistLookupText;
        }

        public AlbumDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<AlbumDTO, Album> GetDataSelector()
        {
            return x => new Album
            {
                AlbumId = x.AlbumId,
                Title = x.Title,
                ArtistId = x.ArtistId
            };
        }

        public override Func<Album, AlbumDTO> GetDTOSelector()
        {
            return x => new AlbumDTO
            {
                AlbumId = x.AlbumId,
                Title = x.Title,
                ArtistId = x.ArtistId,
                ArtistLookupText = x.Artist == null ? "" : x.Artist.LookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                AlbumDTO dto = (new List<Album> { (Album)data })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<AlbumDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
