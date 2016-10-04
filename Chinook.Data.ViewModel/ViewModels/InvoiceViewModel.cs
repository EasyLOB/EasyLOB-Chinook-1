using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Chinook.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    public partial class InvoiceViewModel : ZViewBase<InvoiceViewModel, InvoiceDTO, Invoice>
    {
        #region Properties
        
        [Display(Name = "PropertyInvoiceId", ResourceType = typeof(InvoiceResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Required]
        public virtual int InvoiceId { get; set; }
        
        [Display(Name = "PropertyCustomerId", ResourceType = typeof(InvoiceResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required]
        public virtual int CustomerId { get; set; }
        
        [Display(Name = "PropertyInvoiceDate", ResourceType = typeof(InvoiceResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required]
        public virtual DateTime InvoiceDate { get; set; }
        
        [Display(Name = "PropertyBillingAddress", ResourceType = typeof(InvoiceResources))]
        [StringLength(70)]
        public virtual string BillingAddress { get; set; }
        
        [Display(Name = "PropertyBillingCity", ResourceType = typeof(InvoiceResources))]
        [StringLength(40)]
        public virtual string BillingCity { get; set; }
        
        [Display(Name = "PropertyBillingState", ResourceType = typeof(InvoiceResources))]
        [StringLength(40)]
        public virtual string BillingState { get; set; }
        
        [Display(Name = "PropertyBillingCountry", ResourceType = typeof(InvoiceResources))]
        [StringLength(40)]
        public virtual string BillingCountry { get; set; }
        
        [Display(Name = "PropertyBillingPostalCode", ResourceType = typeof(InvoiceResources))]
        [StringLength(10)]
        public virtual string BillingPostalCode { get; set; }
        
        [Display(Name = "PropertyTotal", ResourceType = typeof(InvoiceResources))]
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        [Required]
        public virtual decimal Total { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string CustomerLookupText { get; set; } // CustomerId
    
        #endregion Associations FK

        #region Methods
        
        public InvoiceViewModel()
        {
            InvoiceId = LibraryDefaults.Default_Int32;
            CustomerId = LibraryDefaults.Default_Int32;
            //InvoiceDate = LibraryDefaults.Default_DateTime;            
            InvoiceDate = DateTime.Today; // !!!
            Total = LibraryDefaults.Default_Decimal;
            BillingAddress = null;
            BillingCity = null;
            BillingState = null;
            BillingCountry = null;
            BillingPostalCode = null;
        }
        
        public InvoiceViewModel(
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

        public InvoiceViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public InvoiceViewModel(IZDTOBase<InvoiceDTO, Invoice> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<InvoiceViewModel, InvoiceDTO> GetDTOSelector()
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
                Total = x.Total
            };
        }

        public override Func<InvoiceDTO, InvoiceViewModel> GetViewSelector()
        {
            return x => new InvoiceViewModel
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
                CustomerLookupText = x.CustomerLookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                InvoiceDTO dto = new InvoiceDTO(data);
                InvoiceViewModel view = (new List<InvoiceDTO> { (InvoiceDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<InvoiceDTO, Invoice> dto)
        {
            if (dto != null)
            {
                InvoiceViewModel view = (new List<InvoiceDTO> { (InvoiceDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<InvoiceDTO, Invoice> ToDTO()
        {
            return (new List<InvoiceViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
