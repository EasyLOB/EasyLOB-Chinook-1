using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookInvoiceRepositoryRedis : ChinookGenericRepositoryRedis<Invoice>
    {
        #region Methods

        public ChinookInvoiceRepositoryRedis(IUnitOfWork unitOfWork)
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
