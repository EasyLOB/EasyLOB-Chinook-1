using System;
using System.Collections.Generic;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    [ZDataDictionaryAttribute(
        associations: new string[] { },
        collections: new string[] { "Albums" },
        isIdentity: true,
        keys: new string[] { "ArtistId" },
        linqOrderBy: "Name",
        linqWhere: "ArtistId == @0",
        lookup: "Name"
    )]
    public partial class Artist : ZDataBase
    {
        #region Properties

        public virtual int ArtistId { get; set; }

        public virtual string Name { get; set; }

        public override string LookupText
        {
            get { return base.LookupText; }
            set { }
        }

        #endregion Properties

        #region Collections (PK)

        public virtual IList<Album> Albums { get; }

        #endregion Collections (PK)

        #region Methods

        public Artist()
        {
            ArtistId = LibraryDefaults.Default_Int32;
            Name = null;

            Albums = new List<Album>();
        }

        public Artist(int artistId)
            : this()
        {
            ArtistId = artistId;
        }

        public Artist(
            int artistId,
            string name = null
        )
            : this()
        {
            ArtistId = artistId;
            Name = name;
        }

        public override object[] GetId()
        {
            return new object[] { ArtistId };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                ArtistId = DataHelper.IdToInt32(ids[0]);
            }
        }

        #endregion Methods
    }
}