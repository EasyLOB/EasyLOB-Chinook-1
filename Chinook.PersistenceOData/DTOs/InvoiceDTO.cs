using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{    
    [MetadataType(typeof(InvoiceDTOMetadata))]    
    public partial class InvoiceDTO : Chinook.Persistence.Chinook.Data.InvoiceDTO, IZDTOBase<InvoiceDTO, Invoice>
    {
        #region Methods

        public InvoiceDTO()
        {
        }

        public InvoiceDTO(IZDataBase data)
        {
            FromData(data);
        }

        #endregion Methods
        
        #region Methods IZDTOBase
        
        public Func<InvoiceDTO, Invoice> GetDataSelector()
        {
            return x => new Invoice
            {
                InvoiceId = x.InvoiceId,
                CustomerId = x.CustomerId,
                InvoiceDate = x.InvoiceDate.UtcDateTime,
                BillingAddress = x.BillingAddress,
                BillingCity = x.BillingCity,
                BillingState = x.BillingState,
                BillingCountry = x.BillingCountry,
                BillingPostalCode = x.BillingPostalCode,
                Total = x.Total
            };
        }

        public Func<Invoice, InvoiceDTO> GetDTOSelector()
        {
            return x => new InvoiceDTO
            {
                InvoiceId = x.InvoiceId,
                CustomerId = x.CustomerId,
                InvoiceDate = x.InvoiceDate,
                BillingAddress = x.BillingAddress,
                BillingCity = x.BillingCity,
                BillingState = x.BillingState,
                BillingCountry = x.BillingCountry,
                BillingPostalCode = x.BillingPostalCode,
                Total = x.Total,
                CustomerLookupText = x.Customer == null ? "" : x.Customer.LookupText
            };
        }

        public void FromData(IZDataBase data)
        {
            InvoiceDTO dto = (new List<Invoice> { (Invoice)data })
                .Select(GetDTOSelector())
                .FirstOrDefault();
            LibraryHelper.Clone(dto, this);
        }

        public IZDataBase ToData()
        {
            return (new List<InvoiceDTO> { this })
                .Select(GetDataSelector())
                .FirstOrDefault();
        }

        #endregion Methods IZDTOBase
    }

    public partial class InvoiceDTOMetadata
    {
        #region Properties

        [Key]
        public virtual int InvoiceId { get; set; }

        #endregion Properties
    }    
}
