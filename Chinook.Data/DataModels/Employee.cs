using System;
using System.Collections.Generic;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    [ZDataDictionaryAttribute(
        associations: new string[] { "EmployeeReportsTo" },
        collections: new string[] { "Customers", "Employees" },
        isIdentity: true,
        keys: new string[] { "EmployeeId" },
        linqOrderBy: "LastName",
        linqWhere: "EmployeeId == @0",
        lookup: "LastName"
    )]
    public partial class Employee : ZDataBase
    {
        #region Properties

        public virtual int EmployeeId { get; set; }

        public virtual string LastName { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string Title { get; set; }

        private int? _reportsTo;

        public virtual int? ReportsTo
        {
            get { return this.EmployeeReportsTo == null ? _reportsTo : this.EmployeeReportsTo.EmployeeId; }
            set
            {
                _reportsTo = value;
                EmployeeReportsTo = null;
            }
        }

        public virtual DateTime? BirthDate { get; set; }

        public virtual DateTime? HireDate { get; set; }

        public virtual string Address { get; set; }

        public virtual string City { get; set; }

        public virtual string State { get; set; }

        public virtual string Country { get; set; }

        public virtual string PostalCode { get; set; }

        public virtual string Phone { get; set; }

        public virtual string Fax { get; set; }

        public virtual string Email { get; set; }

        public override string LookupText
        {
            get { return base.LookupText; }
            set { }
        }

        #endregion Properties

        #region Associations (FK)

        public virtual Employee EmployeeReportsTo { get; set; } // ReportsTo

        #endregion Associations (FK)

        #region Collections (PK)

        public virtual IList<Customer> Customers { get; set; }

        public virtual IList<Employee> Employees { get; set; }

        #endregion Collections (PK)

        #region Methods

        public Employee()
        {
            EmployeeId = LibraryDefaults.Default_Int32;
            LastName = LibraryDefaults.Default_String;
            FirstName = LibraryDefaults.Default_String;
            Title = null;
            ReportsTo = null;
            BirthDate = null;
            HireDate = null;
            Address = null;
            City = null;
            State = null;
            Country = null;
            PostalCode = null;
            Phone = null;
            Fax = null;
            Email = null;

            //EmployeeReportsTo = new Employee();

            Customers = new List<Customer>();
            Employees = new List<Employee>();
        }

        public Employee(int employeeId)
            : this()
        {
            EmployeeId = employeeId;
        }

        public Employee(
            int employeeId,
            string lastName,
            string firstName,
            string title = null,
            int? reportsTo = null,
            DateTime? birthDate = null,
            DateTime? hireDate = null,
            string address = null,
            string city = null,
            string state = null,
            string country = null,
            string postalCode = null,
            string phone = null,
            string fax = null,
            string email = null
        )
            : this()
        {
            EmployeeId = employeeId;
            LastName = lastName;
            FirstName = firstName;
            Title = title;
            ReportsTo = reportsTo;
            BirthDate = birthDate;
            HireDate = hireDate;
            Address = address;
            City = city;
            State = state;
            Country = country;
            PostalCode = postalCode;
            Phone = phone;
            Fax = fax;
            Email = email;
        }

        public override object[] GetId()
        {
            return new object[] { EmployeeId };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                EmployeeId = DataHelper.IdToInt32(ids[0]);
            }
        }

        #endregion Methods
    }
}