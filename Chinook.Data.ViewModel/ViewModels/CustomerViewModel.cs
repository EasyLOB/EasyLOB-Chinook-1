using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Chinook.Data.Resources;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    public partial class CustomerViewModel : ZViewBase<CustomerViewModel, CustomerDTO, Customer>
    {
        #region Properties
        
        [Display(Name = "PropertyCustomerId", ResourceType = typeof(CustomerResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[Key]
        [Range(1, Int32.MaxValue)]
        [Required]
        public virtual int CustomerId { get; set; }
        
        [Display(Name = "PropertyFirstName", ResourceType = typeof(CustomerResources))]
        [Required]
        [StringLength(40)]
        public virtual string FirstName { get; set; }
        
        [Display(Name = "PropertyLastName", ResourceType = typeof(CustomerResources))]
        [Required]
        [StringLength(20)]
        public virtual string LastName { get; set; }
        
        [Display(Name = "PropertyCompany", ResourceType = typeof(CustomerResources))]
        [StringLength(80)]
        public virtual string Company { get; set; }
        
        [Display(Name = "PropertyAddress", ResourceType = typeof(CustomerResources))]
        [StringLength(70)]
        public virtual string Address { get; set; }
        
        [Display(Name = "PropertyCity", ResourceType = typeof(CustomerResources))]
        [StringLength(40)]
        public virtual string City { get; set; }
        
        [Display(Name = "PropertyState", ResourceType = typeof(CustomerResources))]
        [StringLength(40)]
        public virtual string State { get; set; }
        
        [Display(Name = "PropertyCountry", ResourceType = typeof(CustomerResources))]
        [StringLength(40)]
        public virtual string Country { get; set; }
        
        [Display(Name = "PropertyPostalCode", ResourceType = typeof(CustomerResources))]
        [StringLength(10)]
        public virtual string PostalCode { get; set; }
        
        [Display(Name = "PropertyPhone", ResourceType = typeof(CustomerResources))]
        [StringLength(24)]
        public virtual string Phone { get; set; }
        
        [Display(Name = "PropertyFax", ResourceType = typeof(CustomerResources))]
        [StringLength(24)]
        public virtual string Fax { get; set; }
        
        [Display(Name = "PropertyEmail", ResourceType = typeof(CustomerResources))]
        [Required]
        [StringLength(60)]
        public virtual string Email { get; set; }
        
        [Display(Name = "PropertySupportRepId", ResourceType = typeof(CustomerResources))]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public virtual int? SupportRepId { get; set; }

        #endregion Properties

        #region Associations (FK)

        public virtual string EmployeeLookupText { get; set; } // SupportRepId
    
        #endregion Associations FK

        #region Properties ZViewBase

        public override string LookupText { get; set; }

        #endregion Properties ZViewBase

        #region Methods
        
        public CustomerViewModel()
        {
            CustomerId = 1;
        }
        
        public CustomerViewModel(
            int customerId,
            string firstName,
            string lastName,
            string email,
            string company = null,
            string address = null,
            string city = null,
            string state = null,
            string country = null,
            string postalCode = null,
            string phone = null,
            string fax = null,
            int? supportRepId = null,
            string employeeLookupText = null
        )
            : this()
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            Company = company;
            Address = address;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
            Phone = phone;
            Fax = fax;
            Email = email;
            SupportRepId = supportRepId;
            EmployeeLookupText = employeeLookupText;
        }

        public CustomerViewModel(IZDataBase data)
        {
            FromData(data);
        }

        public CustomerViewModel(IZDTOBase<CustomerDTO, Customer> dto)
        {
            FromDTO(dto);
        }

        #endregion Methods

        #region Methods ZViewBase

        public override Func<CustomerViewModel, CustomerDTO> GetDTOSelector()
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
                EmployeeLookupText = x.EmployeeLookupText,
                LookupText = x.LookupText
            };
        }

        public override Func<CustomerDTO, CustomerViewModel> GetViewSelector()
        {
            return x => new CustomerViewModel
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
                EmployeeLookupText = x.EmployeeLookupText,
                LookupText = x.LookupText
            };
        }

        public override void FromData(IZDataBase data)
        {
            if (data != null)
            {
                CustomerDTO dto = new CustomerDTO(data);
                CustomerViewModel view = (new List<CustomerDTO> { (CustomerDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);            
            }
        }

        public override void FromDTO(IZDTOBase<CustomerDTO, Customer> dto)
        {
            if (dto != null)
            {
                CustomerViewModel view = (new List<CustomerDTO> { (CustomerDTO)dto })
                    .Select(GetViewSelector())
                    .SingleOrDefault();
                LibraryHelper.Clone(view, this);
            }
        }

        public override IZDataBase ToData()
        {
            return ToDTO().ToData();
        }
        
        public override IZDTOBase<CustomerDTO, Customer> ToDTO()
        {
            return (new List<CustomerViewModel> { this })
                .Select(GetDTOSelector())
                .SingleOrDefault();   
        }
        
        #endregion Methods ZViewBase
    }
}