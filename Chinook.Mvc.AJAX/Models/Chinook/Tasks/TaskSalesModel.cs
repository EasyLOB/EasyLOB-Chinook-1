using Chinook.Data;
using EasyLOB.Library;

namespace Chinook.Mvc
{
    public class TaskSalesModel
    {
        public ZOperationResult OperationResult { get; set; }

        public InvoiceViewModel Invoice { get; set; }

        public InvoiceLineViewModel InvoiceLine { get; set; }

        public TaskSalesModel()
        {
            OperationResult = new ZOperationResult();
            Invoice = new InvoiceViewModel();
            InvoiceLine = new InvoiceLineViewModel();
        }
    }
}