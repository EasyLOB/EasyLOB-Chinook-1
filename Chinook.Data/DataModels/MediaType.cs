using System;
using System.Collections.Generic;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    [ZDataDictionaryAttribute(
        associations: new string[] { },
        collections: new string[] { "Tracks" },
        isIdentity: true,
        keys: new string[] { "MediaTypeId" },
        linqOrderBy: "Name",
        linqWhere: "MediaTypeId == @0",
        lookup: "Name"
    )]
    public partial class MediaType : ZDataBase
    {
        #region Properties

        public virtual int MediaTypeId { get; set; }

        public virtual string Name { get; set; }

        public override string LookupText
        {
            get { return base.LookupText; }
            set { }
        }

        #endregion Properties

        #region Collections (PK)

        public virtual IList<Track> Tracks { get; }

        #endregion Collections (PK)

        #region Methods

        public MediaType()
        {
            MediaTypeId = LibraryDefaults.Default_Int32;
            Name = null;

            Tracks = new List<Track>();
        }

        public MediaType(int mediaTypeId)
            : this()
        {
            MediaTypeId = mediaTypeId;
        }

        public MediaType(
            int mediaTypeId,
            string name = null
        )
            : this()
        {
            MediaTypeId = mediaTypeId;
            Name = name;
        }

        public override object[] GetId()
        {
            return new object[] { MediaTypeId };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                MediaTypeId = DataHelper.IdToInt32(ids[0]);
            }
        }

        #endregion Methods
    }
}