using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookCustomerRepositoryMongoDB : ChinookGenericRepositoryMongoDB<Customer>
    {
        #region Methods

        public ChinookCustomerRepositoryMongoDB(IUnitOfWork unitOfWork)
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
