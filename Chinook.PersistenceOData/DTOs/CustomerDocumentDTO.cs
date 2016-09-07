using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{    
    [MetadataType(typeof(CustomerDocumentDTOMetadata))]    
    public partial class CustomerDocumentDTO : Chinook.Persistence.Chinook.Data.CustomerDocumentDTO, IZDTOBase<CustomerDocumentDTO, CustomerDocument>
    {
        #region Methods

        public CustomerDocumentDTO()
        {
        }

        public CustomerDocumentDTO(IZDataBase data)
        {
            FromData(data);
        }

        #endregion Methods
        
        #region Methods IZDTOBase
        
        public Func<CustomerDocumentDTO, CustomerDocument> GetDataSelector()
        {
            return x => new CustomerDocument
            {
                CustomerDocumentId = x.CustomerDocumentId,
                CustomerId = x.CustomerId,
                Description = x.Description,
                FileAcronym = x.FileAcronym
            };
        }

        public Func<CustomerDocument, CustomerDocumentDTO> GetDTOSelector()
        {
            return x => new CustomerDocumentDTO
            {
                CustomerDocumentId = x.CustomerDocumentId,
                CustomerId = x.CustomerId,
                Description = x.Description,
                FileAcronym = x.FileAcronym,
                CustomerLookupText = x.Customer == null ? "" : x.Customer.LookupText
            };
        }

        public void FromData(IZDataBase data)
        {
            CustomerDocumentDTO dto = (new List<CustomerDocument> { (CustomerDocument)data })
                .Select(GetDTOSelector())
                .FirstOrDefault();
            LibraryHelper.Clone(dto, this);
        }

        public IZDataBase ToData()
        {
            return (new List<CustomerDocumentDTO> { this })
                .Select(GetDataSelector())
                .FirstOrDefault();
        }

        #endregion Methods IZDTOBase
    }

    public partial class CustomerDocumentDTOMetadata
    {
        #region Properties

        [Key]
        public virtual int CustomerDocumentId { get; set; }

        #endregion Properties
    }    
}
