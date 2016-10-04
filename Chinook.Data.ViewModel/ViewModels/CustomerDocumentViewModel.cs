using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Chinook.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    public partial class CustomerDocumentViewModel : ZViewBase<CustomerDocumentViewModel, CustomerDocumentDTO, CustomerDocument>
    {
        #region Properties
        
        [Display(Name = "PropertyCustomerDocumentId", ResourceType = typeof(CustomerDocumentResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Required]
        public virtual int CustomerDocumentId { get; set; }
        
        [Display(Name = "PropertyCustomerId", ResourceType = typeof(CustomerDocumentResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required]
        public virtual int CustomerId { get; set; }
        
        [Display(Name = "PropertyDescription", ResourceType = typeof(CustomerDocumentResources))]
        [Required]
        [StringLength(100)]
        public virtual string Description { get; set; }
        
        [Display(Name = "PropertyFileAcronym", ResourceType = typeof(CustomerDocumentResources))]
        [Required]
        [StringLength(10)]
        public virtual string FileAcronym { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string CustomerLookupText { get; set; } // CustomerId
    
        #endregion Associations FK

        #region Methods
        
        public CustomerDocumentViewModel()
        {
            CustomerDocumentId = LibraryDefaults.Default_Int32;
            CustomerId = LibraryDefaults.Default_Int32;
            Description = LibraryDefaults.Default_String;
            FileAcronym = LibraryDefaults.Default_String;
        }
        
        public CustomerDocumentViewModel(
            int customerDocumentId,
            int customerId,
            string description,
            string fileAcronym,
            string customerLookupText = null
        )
        {
            CustomerDocumentId = customerDocumentId;
            CustomerId = customerId;
            Description = description;
            FileAcronym = fileAcronym;
            CustomerLookupText = customerLookupText;
        }

        public CustomerDocumentViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public CustomerDocumentViewModel(IZDTOBase<CustomerDocumentDTO, CustomerDocument> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<CustomerDocumentViewModel, CustomerDocumentDTO> GetDTOSelector()
        {
            return x => new CustomerDocumentDTO
            {
                CustomerDocumentId = x.CustomerDocumentId,
                CustomerId = x.CustomerId,
                Description = x.Description,
                FileAcronym = x.FileAcronym
            };
        }

        public override Func<CustomerDocumentDTO, CustomerDocumentViewModel> GetViewSelector()
        {
            return x => new CustomerDocumentViewModel
            {
                CustomerDocumentId = x.CustomerDocumentId,
                CustomerId = x.CustomerId,
                Description = x.Description,
                FileAcronym = x.FileAcronym,
                CustomerLookupText = x.CustomerLookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                CustomerDocumentDTO dto = new CustomerDocumentDTO(data);
                CustomerDocumentViewModel view = (new List<CustomerDocumentDTO> { (CustomerDocumentDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<CustomerDocumentDTO, CustomerDocument> dto)
        {
            if (dto != null)
            {
                CustomerDocumentViewModel view = (new List<CustomerDocumentDTO> { (CustomerDocumentDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<CustomerDocumentDTO, CustomerDocument> ToDTO()
        {
            return (new List<CustomerDocumentViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}
