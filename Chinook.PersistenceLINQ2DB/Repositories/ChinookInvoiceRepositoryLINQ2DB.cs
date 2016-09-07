using System.Linq;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookInvoiceRepositoryLINQ2DB : ChinookGenericRepositoryLINQ2DB<Invoice>
    {
        #region Methods

        public ChinookInvoiceRepositoryLINQ2DB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override IQueryable<Invoice> Join(IQueryable<Invoice> query)
        {
            return
                from invoice in query
                join customer in UnitOfWork.GetQuery<Customer>() on invoice.CustomerId equals customer.CustomerId // INNER JOIN
                select new Invoice
                {
                    InvoiceId = invoice.InvoiceId,
                    CustomerId = invoice.CustomerId,
                    InvoiceDate = invoice.InvoiceDate,
                    BillingAddress = invoice.BillingAddress,
                    BillingCity = invoice.BillingCity,
                    BillingState = invoice.BillingState,
                    BillingCountry = invoice.BillingCountry,
                    BillingPostalCode = invoice.BillingPostalCode,
                    Total = invoice.Total,
                    Customer = customer
                };
        }

        #endregion Methods
    }
}

