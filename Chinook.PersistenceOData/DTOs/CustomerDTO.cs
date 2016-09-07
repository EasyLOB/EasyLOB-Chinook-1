using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{    
    [MetadataType(typeof(CustomerDTOMetadata))]    
    public partial class CustomerDTO : Chinook.Persistence.Chinook.Data.CustomerDTO, IZDTOBase<CustomerDTO, Customer>
    {
        #region Methods

        public CustomerDTO()
        {
        }

        public CustomerDTO(IZDataBase data)
        {
            FromData(data);
        }

        #endregion Methods
        
        #region Methods IZDTOBase
        
        public Func<CustomerDTO, Customer> GetDataSelector()
        {
            return x => new Customer
            {
                CustomerId = x.CustomerId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Company = x.Company,
                Address = x.Address,
                City = x.City,
                State = x.State,
                Country = x.Country,
                PostalCode = x.PostalCode,
                Phone = x.Phone,
                Fax = x.Fax,
                Email = x.Email,
                SupportRepId = x.SupportRepId
            };
        }

        public Func<Customer, CustomerDTO> GetDTOSelector()
        {
            return x => new CustomerDTO
            {
                CustomerId = x.CustomerId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Company = x.Company,
                Address = x.Address,
                City = x.City,
                State = x.State,
                Country = x.Country,
                PostalCode = x.PostalCode,
                Phone = x.Phone,
                Fax = x.Fax,
                Email = x.Email,
                SupportRepId = x.SupportRepId,
                EmployeeLookupText = x.Employee == null ? "" : x.Employee.LookupText
            };
        }

        public void FromData(IZDataBase data)
        {
            CustomerDTO dto = (new List<Customer> { (Customer)data })
                .Select(GetDTOSelector())
                .FirstOrDefault();
            LibraryHelper.Clone(dto, this);
        }

        public IZDataBase ToData()
        {
            return (new List<CustomerDTO> { this })
                .Select(GetDataSelector())
                .FirstOrDefault();
        }

        #endregion Methods IZDTOBase
    }

    public partial class CustomerDTOMetadata
    {
        #region Properties

        [Key]
        public virtual int CustomerId { get; set; }

        #endregion Properties
    }    
}
