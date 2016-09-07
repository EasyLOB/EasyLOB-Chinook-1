using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Security;

namespace Chinook.Mvc
{
    public partial class InvoiceLineItemModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public InvoiceLineViewModel InvoiceLine { get; set; }

        public string ControllerAction { get; set; }

        public bool IsMasterDetail
        {
            get
            {
                return MasterInvoiceId != null || MasterTrackId != null;
            }
            set { }
        }

        public int? MasterInvoiceId { get; set; }

        public int? MasterTrackId { get; set; }

        public InvoiceLineItemModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
