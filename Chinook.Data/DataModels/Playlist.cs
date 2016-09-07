using System;
using System.Collections.Generic;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    [ZDataDictionaryAttribute(
        associations: new string[] { },
        collections: new string[] { "PlaylistTracks" },
        isIdentity: true,
        keys: new string[] { "PlaylistId" },
        linqOrderBy: "Name",
        linqWhere: "PlaylistId == @0",
        lookup: "Name"
    )]
    public partial class Playlist : ZDataBase
    {
        #region Properties

        public virtual int PlaylistId { get; set; }

        public virtual string Name { get; set; }

        public override string LookupText
        {
            get { return base.LookupText; }
            set { }
        }

        #endregion Properties

        #region Collections (PK)

        public virtual IList<PlaylistTrack> PlaylistTracks { get; set; }

        #endregion Collections (PK)

        #region Methods

        public Playlist()
        {
            PlaylistId = LibraryDefaults.Default_Int32;
            Name = null;

            PlaylistTracks = new List<PlaylistTrack>();
        }

        public Playlist(int playlistId)
            : this()
        {
            PlaylistId = playlistId;
        }

        public Playlist(
            int playlistId,
            string name = null
        )
            : this()
        {
            PlaylistId = playlistId;
            Name = name;
        }

        public override object[] GetId()
        {
            return new object[] { PlaylistId };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                PlaylistId = DataHelper.IdToInt32(ids[0]);
            }
        }

        #endregion Methods
    }
}