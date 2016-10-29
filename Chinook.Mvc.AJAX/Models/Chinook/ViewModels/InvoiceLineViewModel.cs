using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Chinook.Data;
using Chinook.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Mvc
{
    public partial class InvoiceLineViewModel : ZViewBase<InvoiceLineViewModel, InvoiceLineDTO, InvoiceLine>
    {
        #region Properties
        
        [Display(Name = "PropertyInvoiceLineId", ResourceType = typeof(InvoiceLineResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Required]
        public virtual int InvoiceLineId { get; set; }
        
        [Display(Name = "PropertyInvoiceId", ResourceType = typeof(InvoiceLineResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required]
        public virtual int InvoiceId { get; set; }
        
        [Display(Name = "PropertyTrackId", ResourceType = typeof(InvoiceLineResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required]
        public virtual int TrackId { get; set; }
        
        [Display(Name = "PropertyUnitPrice", ResourceType = typeof(InvoiceLineResources))]
        [DisplayFormat(DataFormatString = "{0:f2}", ApplyFormatInEditMode = true)]
        [Required]
        public virtual decimal UnitPrice { get; set; }
        
        [Display(Name = "PropertyQuantity", ResourceType = typeof(InvoiceLineResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required]
        public virtual int Quantity { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string InvoiceLookupText { get; set; } // InvoiceId

        public virtual string TrackLookupText { get; set; } // TrackId
    
        #endregion Associations FK

        #region Methods
        
        public InvoiceLineViewModel()
        {
            InvoiceLineId = LibraryDefaults.Default_Int32;
            InvoiceId = LibraryDefaults.Default_Int32;
            TrackId = LibraryDefaults.Default_Int32;
            UnitPrice = LibraryDefaults.Default_Decimal;
            Quantity = LibraryDefaults.Default_Int32;
        }
        
        public InvoiceLineViewModel(
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

        public InvoiceLineViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public InvoiceLineViewModel(IZDTOBase<InvoiceLineDTO, InvoiceLine> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<InvoiceLineViewModel, InvoiceLineDTO> GetDTOSelector()
        {
            return x => new InvoiceLineDTO
            {
                InvoiceLineId = x.InvoiceLineId,
                InvoiceId = x.InvoiceId,
                TrackId = x.TrackId,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity
            };
        }

        public override Func<InvoiceLineDTO, InvoiceLineViewModel> GetViewSelector()
        {
            return x => new InvoiceLineViewModel
            {
                InvoiceLineId = x.InvoiceLineId,
                InvoiceId = x.InvoiceId,
                TrackId = x.TrackId,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
                InvoiceLookupText = x.InvoiceLookupText,
                TrackLookupText = x.TrackLookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                InvoiceLineDTO dto = new InvoiceLineDTO(data);
                InvoiceLineViewModel view = (new List<InvoiceLineDTO> { (InvoiceLineDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<InvoiceLineDTO, InvoiceLine> dto)
        {
            if (dto != null)
            {
                InvoiceLineViewModel view = (new List<InvoiceLineDTO> { (InvoiceLineDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<InvoiceLineDTO, InvoiceLine> ToDTO()
        {
            return (new List<InvoiceLineViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
