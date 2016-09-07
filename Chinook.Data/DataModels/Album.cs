using System;
using System.Collections.Generic;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    [ZDataDictionaryAttribute(
        associations: new string[] { "Artist" },
        collections: new string[] { "Tracks" },
        isIdentity: true,
        keys: new string[] { "AlbumId" },
        linqOrderBy: "Title",
        linqWhere: "AlbumId == @0",
        lookup: "Title"
    )]
    public partial class Album : ZDataBase
    {
        #region Properties

        public virtual int AlbumId { get; set; }

        public virtual string Title { get; set; }

        private int _artistId;

        public virtual int ArtistId
        {
            get { return this.Artist == null ? _artistId : this.Artist.ArtistId; }
            set
            {
                _artistId = value;
                Artist = null;
            }
        }

        public override string LookupText
        {
            get { return base.LookupText; }
            set { }
        }

        #endregion Properties

        #region Associations (FK)

        public virtual Artist Artist { get; set; } // ArtistId

        #endregion Associations (FK)

        #region Collections (PK)

        public virtual IList<Track> Tracks { get; set; }

        #endregion Collections (PK)

        #region Methods

        public Album()
        {
            AlbumId = LibraryDefaults.Default_Int32;
            Title = LibraryDefaults.Default_String;
            ArtistId = LibraryDefaults.Default_Int32;

            //Artist = new Artist();

            Tracks = new List<Track>();
        }

        public Album(int albumId)
            : this()
        {
            AlbumId = albumId;
        }

        public Album(
            int albumId,
            string title,
            int artistId
        )
            : this()
        {
            AlbumId = albumId;
            Title = title;
            ArtistId = artistId;
        }

        public override object[] GetId()
        {
            return new object[] { AlbumId };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                AlbumId = DataHelper.IdToInt32(ids[0]);
            }
        }

        #endregion Methods
    }
}