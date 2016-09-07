using System.Linq;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookCustomerDocumentRepositoryLINQ2DB : ChinookGenericRepositoryLINQ2DB<CustomerDocument>
    {
        #region Methods

        public ChinookCustomerDocumentRepositoryLINQ2DB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override IQueryable<CustomerDocument> Join(IQueryable<CustomerDocument> query)
        {
            return
                from customerDocument in query
                join customer in UnitOfWork.GetQuery<Customer>() on customerDocument.CustomerId equals customer.CustomerId // INNER JOIN
                select new CustomerDocument
                {
                    CustomerDocumentId = customerDocument.CustomerDocumentId,
                    CustomerId = customerDocument.CustomerId,
                    Description = customerDocument.Description,
                    FileAcronym = customerDocument.FileAcronym,
                    Customer = customer
                };
        }

        #endregion Methods
    }
}

