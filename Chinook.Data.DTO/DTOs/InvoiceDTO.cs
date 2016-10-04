using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    public partial class InvoiceDTO : ZDTOBase<InvoiceDTO, Invoice>
    {
        #region Properties
               
        public virtual int InvoiceId { get; set; }
               
        public virtual int CustomerId { get; set; }
               
        public virtual DateTime InvoiceDate { get; set; }
               
        public virtual string BillingAddress { get; set; }
               
        public virtual string BillingCity { get; set; }
               
        public virtual string BillingState { get; set; }
               
        public virtual string BillingCountry { get; set; }
               
        public virtual string BillingPostalCode { get; set; }
               
        public virtual decimal Total { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string CustomerLookupText { get; set; } // CustomerId
    
        #endregion Associations FK

        #region Methods
        
        public InvoiceDTO()
        {
            InvoiceId = LibraryDefaults.Default_Int32;
            CustomerId = LibraryDefaults.Default_Int32;
            InvoiceDate = LibraryDefaults.Default_DateTime;
            Total = LibraryDefaults.Default_Decimal;
            BillingAddress = null;
            BillingCity = null;
            BillingState = null;
            BillingCountry = null;
            BillingPostalCode = null;
        }
        
        public InvoiceDTO(
            int invoiceId,
            int customerId,
            DateTime invoiceDate,
            decimal total,
            string billingAddress = null,
            string billingCity = null,
            string billingState = null,
            string billingCountry = null,
            string billingPostalCode = null,
            string customerLookupText = null
        )
        {
            InvoiceId = invoiceId;
            CustomerId = customerId;
            InvoiceDate = invoiceDate;
            BillingAddress = billingAddress;
            BillingCity = billingCity;
            BillingState = billingState;
            BillingCountry = billingCountry;
            BillingPostalCode = billingPostalCode;
            Total = total;
            CustomerLookupText = customerLookupText;
        }

        public InvoiceDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<InvoiceDTO, Invoice> GetDataSelector()
        {
            return x => new Invoice
            {
                InvoiceId = x.InvoiceId,
                CustomerId = x.CustomerId,
                InvoiceDate = x.InvoiceDate,
                BillingAddress = x.BillingAddress,
                BillingCity = x.BillingCity,
                BillingState = x.BillingState,
                BillingCountry = x.BillingCountry,
                BillingPostalCode = x.BillingPostalCode,
                Total = x.Total
            };
        }

        public override Func<Invoice, InvoiceDTO> GetDTOSelector()
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
                CustomerLookupText = x.Customer == null ? "" : x.Customer.LookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                InvoiceDTO dto = (new List<Invoice> { (Invoice)data })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<InvoiceDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
