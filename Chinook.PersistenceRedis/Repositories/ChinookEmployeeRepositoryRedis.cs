using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookEmployeeRepositoryRedis : ChinookGenericRepositoryRedis<Employee>
    {
        #region Methods

        public ChinookEmployeeRepositoryRedis(IUnitOfWork unitOfWork)
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
