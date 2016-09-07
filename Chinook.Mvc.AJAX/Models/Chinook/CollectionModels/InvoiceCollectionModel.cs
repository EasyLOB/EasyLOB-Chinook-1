using EasyLOB.Library;
using EasyLOB.Security;

namespace Chinook.Mvc
{
    public partial class InvoiceCollectionModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public string ControllerAction { get; set; }

        public string MasterControllerAction { get; set; }

        public bool IsMasterDetail
        {
            get
            {
                return MasterCustomerId != null;
            }
            set { }
        }

        public int? MasterCustomerId { get; set; }

        public InvoiceCollectionModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
