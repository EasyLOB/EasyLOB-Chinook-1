using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Chinook.Data;
using Chinook.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Mvc
{
    public partial class TrackViewModel : ZViewBase<TrackViewModel, TrackDTO, Track>
    {
        #region Properties
        
        [Display(Name = "PropertyTrackId", ResourceType = typeof(TrackResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Required]
        public virtual int TrackId { get; set; }
        
        [Display(Name = "PropertyName", ResourceType = typeof(TrackResources))]
        [Required]
        [StringLength(200)]
        public virtual string Name { get; set; }
        
        [Display(Name = "PropertyAlbumId", ResourceType = typeof(TrackResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public virtual int? AlbumId { get; set; }
        
        [Display(Name = "PropertyMediaTypeId", ResourceType = typeof(TrackResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required]
        public virtual int MediaTypeId { get; set; }
        
        [Display(Name = "PropertyGenreId", ResourceType = typeof(TrackResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public virtual int? GenreId { get; set; }
        
        [Display(Name = "PropertyComposer", ResourceType = typeof(TrackResources))]
        [StringLength(220)]
        public virtual string Composer { get; set; }
        
        [Display(Name = "PropertyMilliseconds", ResourceType = typeof(TrackResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required]
        public virtual int Milliseconds { get; set; }
        
        [Display(Name = "PropertyBytes", ResourceType = typeof(TrackResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public virtual int? Bytes { get; set; }
        
        [Display(Name = "PropertyUnitPrice", ResourceType = typeof(TrackResources))]
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        [Required]
        public virtual decimal UnitPrice { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string AlbumLookupText { get; set; } // AlbumId

        public virtual string GenreLookupText { get; set; } // GenreId

        public virtual string MediaTypeLookupText { get; set; } // MediaTypeId
    
        #endregion Associations FK

        #region Methods
        
        public TrackViewModel()
        {
            TrackId = LibraryDefaults.Default_Int32;
            Name = LibraryDefaults.Default_String;
            MediaTypeId = LibraryDefaults.Default_Int32;
            Milliseconds = LibraryDefaults.Default_Int32;
            UnitPrice = LibraryDefaults.Default_Decimal;
            AlbumId = null;
            GenreId = null;
            Composer = null;
            Bytes = null;
        }

        public TrackViewModel(
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

        public TrackViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public TrackViewModel(IZDTOBase<TrackDTO, Track> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<TrackViewModel, TrackDTO> GetDTOSelector()
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
                UnitPrice = x.UnitPrice
            };
        }

        public override Func<TrackDTO, TrackViewModel> GetViewSelector()
        {
            return x => new TrackViewModel
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
                AlbumLookupText = x.AlbumLookupText,
                GenreLookupText = x.GenreLookupText,
                MediaTypeLookupText = x.MediaTypeLookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                TrackDTO dto = new TrackDTO(data);
                TrackViewModel view = (new List<TrackDTO> { (TrackDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<TrackDTO, Track> dto)
        {
            if (dto != null)
            {
                TrackViewModel view = (new List<TrackDTO> { (TrackDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<TrackDTO, Track> ToDTO()
        {
            return (new List<TrackViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
