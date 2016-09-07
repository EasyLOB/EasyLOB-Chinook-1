using System;
using System.Collections.Generic;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    [ZDataDictionaryAttribute(
        associations: new string[] { "Album", "Genre", "MediaType" },
        collections: new string[] { "InvoiceLines", "PlaylistTracks" },
        isIdentity: true,
        keys: new string[] { "TrackId" },
        linqOrderBy: "Name",
        linqWhere: "TrackId == @0",
        lookup: "Name"
    )]
    public partial class Track : ZDataBase
    {
        #region Properties

        public virtual int TrackId { get; set; }

        public virtual string Name { get; set; }

        private int? _albumId;

        public virtual int? AlbumId
        {
            get { return this.Album == null ? _albumId : this.Album.AlbumId; }
            set
            {
                _albumId = value;
                Album = null;
            }
        }

        private int _mediaTypeId;

        public virtual int MediaTypeId
        {
            get { return this.MediaType == null ? _mediaTypeId : this.MediaType.MediaTypeId; }
            set
            {
                _mediaTypeId = value;
                MediaType = null;
            }
        }

        private int? _genreId;

        public virtual int? GenreId
        {
            get { return this.Genre == null ? _genreId : this.Genre.GenreId; }
            set
            {
                _genreId = value;
                Genre = null;
            }
        }

        public virtual string Composer { get; set; }

        public virtual int Milliseconds { get; set; }

        public virtual int? Bytes { get; set; }

        public virtual decimal UnitPrice { get; set; }

        public override string LookupText
        {
            get { return base.LookupText; }
            set { }
        }

        #endregion Properties

        #region Associations (FK)

        public virtual Album Album { get; set; } // AlbumId

        public virtual Genre Genre { get; set; } // GenreId

        public virtual MediaType MediaType { get; set; } // MediaTypeId

        #endregion Associations (FK)

        #region Collections (PK)

        public virtual IList<InvoiceLine> InvoiceLines { get; set; }

        public virtual IList<PlaylistTrack> PlaylistTracks { get; set; }

        #endregion Collections (PK)

        #region Methods

        public Track()
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

            //Album = new Album();
            //Genre = new Genre();
            //MediaType = new MediaType();

            InvoiceLines = new List<InvoiceLine>();
            PlaylistTracks = new List<PlaylistTrack>();
        }

        public Track(int trackId)
            : this()
        {
            TrackId = trackId;
        }

        public Track(
            int trackId,
            string name,
            int mediaTypeId,
            int milliseconds,
            decimal unitPrice,
            int? albumId = null,
            int? genreId = null,
            string composer = null,
            int? bytes = null
        )
            : this()
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
        }

        public override object[] GetId()
        {
            return new object[] { TrackId };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                TrackId = DataHelper.IdToInt32(ids[0]);
            }
        }

        #endregion Methods
    }
}