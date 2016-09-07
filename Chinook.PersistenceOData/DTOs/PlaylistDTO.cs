using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{    
    [MetadataType(typeof(PlaylistDTOMetadata))]    
    public partial class PlaylistDTO : Chinook.Persistence.Chinook.Data.PlaylistDTO, IZDTOBase<PlaylistDTO, Playlist>
    {
        #region Methods

        public PlaylistDTO()
        {
        }

        public PlaylistDTO(IZDataBase data)
        {
            FromData(data);
        }

        #endregion Methods
        
        #region Methods IZDTOBase
        
        public Func<PlaylistDTO, Playlist> GetDataSelector()
        {
            return x => new Playlist
            {
                PlaylistId = x.PlaylistId,
                Name = x.Name
            };
        }

        public Func<Playlist, PlaylistDTO> GetDTOSelector()
        {
            return x => new PlaylistDTO
            {
                PlaylistId = x.PlaylistId,
                Name = x.Name
            };
        }

        public void FromData(IZDataBase data)
        {
            PlaylistDTO dto = (new List<Playlist> { (Playlist)data })
                .Select(GetDTOSelector())
                .FirstOrDefault();
            LibraryHelper.Clone(dto, this);
        }

        public IZDataBase ToData()
        {
            return (new List<PlaylistDTO> { this })
                .Select(GetDataSelector())
                .FirstOrDefault();
        }

        #endregion Methods IZDTOBase
    }

    public partial class PlaylistDTOMetadata
    {
        #region Properties

        [Key]
        public virtual int PlaylistId { get; set; }

        #endregion Properties
    }    
}
