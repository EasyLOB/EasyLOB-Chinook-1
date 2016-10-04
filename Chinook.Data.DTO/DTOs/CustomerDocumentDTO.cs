using System;
using System.Collections.Generic;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    public partial class CustomerDocumentDTO : ZDTOBase<CustomerDocumentDTO, CustomerDocument>
    {
        #region Properties
               
        public virtual int CustomerDocumentId { get; set; }
               
        public virtual int CustomerId { get; set; }
               
        public virtual string Description { get; set; }
               
        public virtual string FileAcronym { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string CustomerLookupText { get; set; } // CustomerId
    
        #endregion Associations FK

        #region Methods
        
        public CustomerDocumentDTO()
        {
            CustomerDocumentId = LibraryDefaults.Default_Int32;
            CustomerId = LibraryDefaults.Default_Int32;
            Description = LibraryDefaults.Default_String;
            FileAcronym = LibraryDefaults.Default_String;
        }
        
        public CustomerDocumentDTO(
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

        public CustomerDocumentDTO(IZDataBase data)
        {
            FromData(data);
        }
        
        #endregion Methods

        #region Methods ZDTOBase

        public override Func<CustomerDocumentDTO, CustomerDocument> GetDataSelector()
        {
            return x => new CustomerDocument
            {
                CustomerDocumentId = x.CustomerDocumentId,
                CustomerId = x.CustomerId,
                Description = x.Description,
                FileAcronym = x.FileAcronym
            };
        }

        public override Func<CustomerDocument, CustomerDocumentDTO> GetDTOSelector()
        {
            return x => new CustomerDocumentDTO
            {
                CustomerDocumentId = x.CustomerDocumentId,
                CustomerId = x.CustomerId,
                Description = x.Description,
                FileAcronym = x.FileAcronym,
                CustomerLookupText = x.Customer == null ? "" : x.Customer.LookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                CustomerDocumentDTO dto = (new List<CustomerDocument> { (CustomerDocument)data })
                    .Select(GetDTOSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(dto, this);
            }
        }

        public override IZDataBase ToData()
        {
            return (new List<CustomerDocumentDTO> { this })
                .Select(GetDataSelector())
                .SingleOrDefault();
        }

        #endregion Methods ZDTOBase
    }
}
