using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{    
    [MetadataType(typeof(EmployeeDTOMetadata))]    
    public partial class EmployeeDTO : Chinook.Persistence.Chinook.Data.EmployeeDTO, IZDTOBase<EmployeeDTO, Employee>
    {
        #region Methods

        public EmployeeDTO()
        {
        }

        public EmployeeDTO(IZDataBase data)
        {
            FromData(data);
        }

        #endregion Methods
        
        #region Methods IZDTOBase
        
        public Func<EmployeeDTO, Employee> GetDataSelector()
        {
            return x => new Employee
            {
                EmployeeId = x.EmployeeId,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Title = x.Title,
                ReportsTo = x.ReportsTo,
                BirthDate = x.BirthDate.GetValueOrDefault().UtcDateTime,
                HireDate = x.HireDate.GetValueOrDefault().UtcDateTime,
                Address = x.Address,
                City = x.City,
                State = x.State,
                Country = x.Country,
                PostalCode = x.PostalCode,
                Phone = x.Phone,
                Fax = x.Fax,
                Email = x.Email
            };
        }

        public Func<Employee, EmployeeDTO> GetDTOSelector()
        {
            return x => new EmployeeDTO
            {
                EmployeeId = x.EmployeeId,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Title = x.Title,
                ReportsTo = x.ReportsTo,
                BirthDate = x.BirthDate,
                HireDate = x.HireDate,
                Address = x.Address,
                City = x.City,
                State = x.State,
                Country = x.Country,
                PostalCode = x.PostalCode,
                Phone = x.Phone,
                Fax = x.Fax,
                Email = x.Email,
                EmployeeLookupText = x.EmployeeReportsTo == null ? "" : x.EmployeeReportsTo.LookupText
            };
        }

        public void FromData(IZDataBase data)
        {
            EmployeeDTO dto = (new List<Employee> { (Employee)data })
                .Select(GetDTOSelector())
                .FirstOrDefault();
            LibraryHelper.Clone(dto, this);
        }

        public IZDataBase ToData()
        {
            return (new List<EmployeeDTO> { this })
                .Select(GetDataSelector())
                .FirstOrDefault();
        }

        #endregion Methods IZDTOBase
    }

    public partial class EmployeeDTOMetadata
    {
        #region Properties

        [Key]
        public virtual int EmployeeId { get; set; }

        #endregion Properties
    }    
}
