using System;
using System.Collections.Generic;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    [ZDataDictionaryAttribute(
        associations: new string[] { "Invoice", "Track" },
        collections: new string[] { },
        isIdentity: true,
        keys: new string[] { "InvoiceLineId" },
        linqOrderBy: "InvoiceId",
        linqWhere: "InvoiceLineId == @0",
        lookup: "InvoiceId"
    )]
    public partial class InvoiceLine : ZDataBase
    {
        #region Properties

        public virtual int InvoiceLineId { get; set; }

        private int _invoiceId;

        public virtual int InvoiceId
        {
            get { return this.Invoice == null ? _invoiceId : this.Invoice.InvoiceId; }
            set
            {
                _invoiceId = value;
                Invoice = null;
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

        public virtual decimal UnitPrice { get; set; }

        public virtual int Quantity { get; set; }

        public override string LookupText
        {
            get { return base.LookupText; }
            set { }
        }

        #endregion Properties

        #region Associations (FK)

        public virtual Invoice Invoice { get; set; } // InvoiceId

        public virtual Track Track { get; set; } // TrackId

        #endregion Associations (FK)

        #region Methods

        public InvoiceLine()
        {
            InvoiceLineId = LibraryDefaults.Default_Int32;
            InvoiceId = LibraryDefaults.Default_Int32;
            TrackId = LibraryDefaults.Default_Int32;
            UnitPrice = LibraryDefaults.Default_Decimal;
            Quantity = LibraryDefaults.Default_Int32;

            //Invoice = new Invoice();
            //Track = new Track();

            // !!!
            Quantity = 1;
        }

        public InvoiceLine(int invoiceLineId)
            : this()
        {
            InvoiceLineId = invoiceLineId;
        }

        public InvoiceLine(
            int invoiceLineId,
            int invoiceId,
            int trackId,
            decimal unitPrice,
            int quantity
        )
            : this()
        {
            InvoiceLineId = invoiceLineId;
            InvoiceId = invoiceId;
            TrackId = trackId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public override object[] GetId()
        {
            return new object[] { InvoiceLineId };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                InvoiceLineId = DataHelper.IdToInt32(ids[0]);
            }
        }

        #endregion Methods
    }
}