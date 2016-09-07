using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookEmployeeRepositoryMongoDB : ChinookGenericRepositoryMongoDB<Employee>
    {
        #region Methods

        public ChinookEmployeeRepositoryMongoDB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override void Join(Employee employee)
        {
            if (employee != null)
            {
                employee.EmployeeReportsTo = UnitOfWork.GetRepository<Employee>().GetById(employee.ReportsTo);
            }
        }

        #endregion Methods
    }
}
