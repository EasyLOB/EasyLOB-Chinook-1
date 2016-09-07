using EasyLOB.Library;
using EasyLOB.Security;

namespace Chinook.Mvc
{
    public partial class InvoiceLineCollectionModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public string ControllerAction { get; set; }

        public string MasterControllerAction { get; set; }

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

        public InvoiceLineCollectionModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
