using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookCustomerDocumentRepositoryMongoDB : ChinookGenericRepositoryMongoDB<CustomerDocument>
    {
        #region Methods

        public ChinookCustomerDocumentRepositoryMongoDB(IUnitOfWork unitOfWork)
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
