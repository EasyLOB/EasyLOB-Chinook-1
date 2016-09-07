using System;
using System.Collections.Generic;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    [ZDataDictionaryAttribute(
        associations: new string[] { "Playlist", "Track" },
        collections: new string[] { },
        isIdentity: false,
        keys: new string[] { "PlaylistId", "TrackId" },
        linqOrderBy: "TrackId",
        linqWhere: "PlaylistId == @0 && TrackId == @1",
        lookup: "TrackId"
    )]
    public partial class PlaylistTrack : ZDataBase
    {
        #region Properties

        private int _playlistId;

        public virtual int PlaylistId
        {
            get { return this.Playlist == null ? _playlistId : this.Playlist.PlaylistId; }
            set
            {
                _playlistId = value;
                Playlist = null;
            }
        }

        private int _trackId;

        public virtual int TrackId
        {
            get { return this.Track == null ? _trackId : this.Track.TrackId; }
            set
            {
                _trackId = value;
                Track = null;
            }
        }

        public override string LookupText
        {
            get { return base.LookupText; }
            set { }
        }

        #endregion Properties

        #region Associations (FK)

        public virtual Playlist Playlist { get; set; } // PlaylistId

        public virtual Track Track { get; set; } // TrackId

        #endregion Associations (FK)

        #region Methods

        public PlaylistTrack()
        {
            PlaylistId = LibraryDefaults.Default_Int32;
            TrackId = LibraryDefaults.Default_Int32;

            //Playlist = new Playlist();
            //Track = new Track();
        }

        public PlaylistTrack(
            int playlistId,
            int trackId
        )
            : this()
        {
            PlaylistId = playlistId;
            TrackId = trackId;
        }

        public override object[] GetId()
        {
            return new object[] { PlaylistId, TrackId };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                PlaylistId = DataHelper.IdToInt32(ids[0]);
            }
            if (ids != null && ids[1] != null)
            {
                TrackId = DataHelper.IdToInt32(ids[1]);
            }
        }

        #endregion Methods

        #region Methods NHibernate

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is PlaylistTrack)
            {
                var playlistTrack = (PlaylistTrack)obj;
                if (playlistTrack == null)
                {
                    return false;
                }

                if (PlaylistId == playlistTrack.PlaylistId && TrackId == playlistTrack.TrackId)
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (PlaylistId.ToString() + "|" + TrackId.ToString()).GetHashCode();
        }

        #endregion Methods NHibernate
    }
}