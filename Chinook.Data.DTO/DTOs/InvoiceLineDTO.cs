using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    public partial class InvoiceLineDTO : ZDTOBase<InvoiceLineDTO, InvoiceLine>
    {
        #region Properties
               
        public virtual int InvoiceLineId { get; set; }
               
        public virtual int InvoiceId { get; set; }
               
        public virtual int TrackId { get; set; }
               
        public virtual decimal UnitPrice { get; set; }
               
        public virtual int Quantity { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string InvoiceLookupText { get; set; } // InvoiceId

        public virtual string TrackLookupText { get; set; } // TrackId
    
        #endregion Associations FK

        #region Methods
        
        public InvoiceLineDTO()
        {
            InvoiceLineId = LibraryDefaults.Default_Int32;
            InvoiceId = LibraryDefaults.Default_Int32;
            TrackId = LibraryDefaults.Default_Int32;
            UnitPrice = LibraryDefaults.Default_Decimal;
            Quantity = LibraryDefaults.Default_Int32;
        }
        
        public InvoiceLineDTO(
            int invoiceLineId,
            int invoiceId,
            int trackId,
            decimal unitPrice,
            int quantity,
            string invoiceLookupText = null,
            string trackLookupText = null
        )
        {
            InvoiceLineId = invoiceLineId;
            InvoiceId = invoiceId;
            TrackId = trackId;
            UnitPrice = unitPrice;
            Quantity = quantity;
            InvoiceLookupText = invoiceLookupText;
            TrackLookupText = trackLookupText;
        }

        public InvoiceLineDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<InvoiceLineDTO, InvoiceLine> GetDataSelector()
        {
            return x => new InvoiceLine
            {
                InvoiceLineId = x.InvoiceLineId,
                InvoiceId = x.InvoiceId,
                TrackId = x.TrackId,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity
            };
        }

        public override Func<InvoiceLine, InvoiceLineDTO> GetDTOSelector()
        {
            return x => new InvoiceLineDTO
            {
                InvoiceLineId = x.InvoiceLineId,
                InvoiceId = x.InvoiceId,
                TrackId = x.TrackId,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
                InvoiceLookupText = x.Invoice == null ? "" : x.Invoice.LookupText,
                TrackLookupText = x.Track == null ? "" : x.Track.LookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                InvoiceLineDTO dto = (new List<InvoiceLine> { (InvoiceLine)data })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<InvoiceLineDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
