using System.Linq;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookInvoiceLineRepositoryLINQ2DB : ChinookGenericRepositoryLINQ2DB<InvoiceLine>
    {
        #region Methods

        public ChinookInvoiceLineRepositoryLINQ2DB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override IQueryable<InvoiceLine> Join(IQueryable<InvoiceLine> query)
        {
            return
                from invoiceLine in query
                join invoice in UnitOfWork.GetQuery<Invoice>() on invoiceLine.InvoiceId equals invoice.InvoiceId // INNER JOIN
                join track in UnitOfWork.GetQuery<Track>() on invoiceLine.TrackId equals track.TrackId // INNER JOIN
                select new InvoiceLine
                {
                    InvoiceLineId = invoiceLine.InvoiceLineId,
                    InvoiceId = invoiceLine.InvoiceId,
                    TrackId = invoiceLine.TrackId,
                    UnitPrice = invoiceLine.UnitPrice,
                    Quantity = invoiceLine.Quantity,
                    Invoice = invoice,
                    Track = track
                };
        }

        #endregion Methods
    }
}

