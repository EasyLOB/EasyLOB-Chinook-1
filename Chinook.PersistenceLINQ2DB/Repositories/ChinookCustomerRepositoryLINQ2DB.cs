using System.Linq;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookCustomerRepositoryLINQ2DB : ChinookGenericRepositoryLINQ2DB<Customer>
    {
        #region Methods

        public ChinookCustomerRepositoryLINQ2DB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override IQueryable<Customer> Join(IQueryable<Customer> query)
        {
            return
                from customer in query
                from employee in UnitOfWork.GetQuery<Employee>().Where(x => x.EmployeeId == customer.SupportRepId).DefaultIfEmpty() // LEFT JOIN
                select new Customer
                {
                    CustomerId = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Company = customer.Company,
                    Address = customer.Address,
                    City = customer.City,
                    State = customer.State,
                    Country = customer.Country,
                    PostalCode = customer.PostalCode,
                    Phone = customer.Phone,
                    Fax = customer.Fax,
                    Email = customer.Email,
                    SupportRepId = customer.SupportRepId,
                    Employee = employee
                };
        }

        #endregion Methods
    }
}

