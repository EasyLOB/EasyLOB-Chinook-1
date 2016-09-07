using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookCustomerDocumentRepositoryRedis : ChinookGenericRepositoryRedis<CustomerDocument>
    {
        #region Methods

        public ChinookCustomerDocumentRepositoryRedis(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override void Join(CustomerDocument customerDocument)
        {
            if (customerDocument != null)
            {
                customerDocument.Customer = UnitOfWork.GetRepository<Customer>().GetById(customerDocument.CustomerId);
            }
        }

        #endregion Methods
    }
}
