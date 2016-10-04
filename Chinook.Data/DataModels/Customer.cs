using System;
using System.Collections.Generic;
using EasyLOB.Data;
using EasyLOB.Library;

namespace Chinook.Data
{
    [ZDataDictionaryAttribute(
        associations: new string[] { "Employee" },
        collections: new string[] { "CustomerDocuments", "Invoices" },
        isIdentity: true,
        keys: new string[] { "CustomerId" },
        linqOrderBy: "FirstName",
        linqWhere: "CustomerId == @0",
        lookup: "FirstName"
    )]
    public partial class Customer : ZDataBase
    {
        #region Properties

        public virtual int CustomerId { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Company { get; set; }

        public virtual string Address { get; set; }

        public virtual string City { get; set; }

        public virtual string State { get; set; }

        public virtual string Country { get; set; }

        public virtual string PostalCode { get; set; }

        public virtual string Phone { get; set; }

        public virtual string Fax { get; set; }

        public virtual string Email { get; set; }

        private int? _supportRepId;

        public virtual int? SupportRepId
        {
            get { return this.Employee == null ? _supportRepId : this.Employee.EmployeeId; }
            set
            {
                _supportRepId = value;
                Employee = null;
            }
        }

        public override string LookupText
        {
            get { return base.LookupText; }
            set { }
        }

        #endregion Properties

        #region Associations (FK)

        public virtual Employee Employee { get; set; } // SupportRepId

        #endregion Associations (FK)

        #region Collections (PK)

        public virtual IList<CustomerDocument> CustomerDocuments { get; }

        public virtual IList<Invoice> Invoices { get; }

        #endregion Collections (PK)

        #region Methods

        public Customer()
        {
            CustomerId = LibraryDefaults.Default_Int32;
            FirstName = LibraryDefaults.Default_String;
            LastName = LibraryDefaults.Default_String;
            Email = LibraryDefaults.Default_String;
            Company = null;
            Address = null;
            City = null;
            State = null;
            Country = null;
            PostalCode = null;
            Phone = null;
            Fax = null;
            SupportRepId = null;

            //Employee = new Employee();

            CustomerDocuments = new List<CustomerDocument>();
            Invoices = new List<Invoice>();
        }

        public Customer(int customerId)
            : this()
        {
            CustomerId = customerId;
        }

        public Customer(
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
            int? supportRepId = null
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
        }

        public override object[] GetId()
        {
            return new object[] { CustomerId };
        }

        public override void SetId(object[] ids)
        {
            if (ids != null && ids[0] != null)
            {
                CustomerId = DataHelper.IdToInt32(ids[0]);
            }
        }

        #endregion Methods
    }
}