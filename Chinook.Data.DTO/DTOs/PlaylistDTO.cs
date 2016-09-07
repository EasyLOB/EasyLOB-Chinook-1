using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    public partial class PlaylistDTO : ZDTOBase<PlaylistDTO, Playlist>
    {
        #region Properties
               
        public virtual int PlaylistId { get; set; }
               
        public virtual string Name { get; set; }

        #endregion Properties

        #region Properties ZDTOBase

        public override string LookupText { get; set; }

        #endregion Properties ZDTOBase

        #region Methods
        
        public PlaylistDTO()
        {
        }
        
        public PlaylistDTO(
            int playlistId,
            string name = null
        )
        {
            PlaylistId = playlistId;
            Name = name;
        }

        public PlaylistDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<PlaylistDTO, Playlist> GetDataSelector()
        {
            return x => new Playlist
            {
                PlaylistId = x.PlaylistId,
                Name = x.Name
            };
        }

        public override Func<Playlist, PlaylistDTO> GetDTOSelector()
        {
            return x => new PlaylistDTO
            {
                PlaylistId = x.PlaylistId,
                Name = x.Name,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                PlaylistDTO dto = (new List<Playlist> { (Playlist)data })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<PlaylistDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
