using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{    
    [MetadataType(typeof(InvoiceLineDTOMetadata))]    
    public partial class InvoiceLineDTO : Chinook.Persistence.Chinook.Data.InvoiceLineDTO, IZDTOBase<InvoiceLineDTO, InvoiceLine>
    {
        #region Methods

        public InvoiceLineDTO()
        {
        }

        public InvoiceLineDTO(IZDataBase data)
        {
            FromData(data);
        }

        #endregion Methods
        
        #region Methods IZDTOBase
        
        public Func<InvoiceLineDTO, InvoiceLine> GetDataSelector()
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

        public Func<InvoiceLine, InvoiceLineDTO> GetDTOSelector()
        {
            return x => new InvoiceLineDTO
            {
                InvoiceLineId = x.InvoiceLineId,
                InvoiceId = x.InvoiceId,
                TrackId = x.TrackId,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
                InvoiceLookupText = x.Invoice == null ? "" : x.Invoice.LookupText,
                TrackLookupText = x.Track == null ? "" : x.Track.LookupText
            };
        }

        public void FromData(IZDataBase data)
        {
            InvoiceLineDTO dto = (new List<InvoiceLine> { (InvoiceLine)data })
                .Select(GetDTOSelector())
                .FirstOrDefault();
            LibraryHelper.Clone(dto, this);
        }

        public IZDataBase ToData()
        {
            return (new List<InvoiceLineDTO> { this })
                .Select(GetDataSelector())
                .FirstOrDefault();
        }

        #endregion Methods IZDTOBase
    }

    public partial class InvoiceLineDTOMetadata
    {
        #region Properties

        [Key]
        public virtual int InvoiceLineId { get; set; }

        #endregion Properties
    }    
}
