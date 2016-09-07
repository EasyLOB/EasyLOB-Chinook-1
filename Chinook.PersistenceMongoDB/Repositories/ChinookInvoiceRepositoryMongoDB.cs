using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookInvoiceRepositoryMongoDB : ChinookGenericRepositoryMongoDB<Invoice>
    {
        #region Methods

        public ChinookInvoiceRepositoryMongoDB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override void Join(Invoice invoice)
        {
            if (invoice != null)
            {
                invoice.Customer = UnitOfWork.GetRepository<Customer>().GetById(invoice.CustomerId);
            }
        }

        #endregion Methods
    }
}
