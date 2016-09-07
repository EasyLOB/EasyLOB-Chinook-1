using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookCustomerRepositoryRedis : ChinookGenericRepositoryRedis<Customer>
    {
        #region Methods

        public ChinookCustomerRepositoryRedis(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override void Join(Customer customer)
        {
            if (customer != null)
            {
                customer.Employee = UnitOfWork.GetRepository<Employee>().GetById(customer.SupportRepId);
            }
        }

        #endregion Methods
    }
}
