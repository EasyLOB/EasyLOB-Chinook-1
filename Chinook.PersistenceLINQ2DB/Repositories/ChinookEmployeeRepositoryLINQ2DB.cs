using System.Linq;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookEmployeeRepositoryLINQ2DB : ChinookGenericRepositoryLINQ2DB<Employee>
    {
        #region Methods

        public ChinookEmployeeRepositoryLINQ2DB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override IQueryable<Employee> Join(IQueryable<Employee> query)
        {
            return
                from employee in query
                //from employeeReportsTo in UnitOfWork.GetQuery<EmployeeReportsTo>().Where(x => x.EmployeeId == employee.ReportsTo).DefaultIfEmpty() // LEFT JOIN // !!!
                select new Employee
                {
                    EmployeeId = employee.EmployeeId,
                    LastName = employee.LastName,
                    FirstName = employee.FirstName,
                    Title = employee.Title,
                    ReportsTo = employee.ReportsTo,
                    BirthDate = employee.BirthDate,
                    HireDate = employee.HireDate,
                    Address = employee.Address,
                    City = employee.City,
                    State = employee.State,
                    Country = employee.Country,
                    PostalCode = employee.PostalCode,
                    Phone = employee.Phone,
                    Fax = employee.Fax,
                    Email = employee.Email
                    //EmployeeReportsTo = employeeReportsTo // !!!
                };
        }

        #endregion Methods
    }
}

