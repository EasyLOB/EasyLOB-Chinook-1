using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Security;

namespace Chinook.Mvc
{
    public partial class CustomerDocumentItemModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public CustomerDocumentViewModel CustomerDocument { get; set; }

        public string ControllerAction { get; set; }

        public bool IsMasterDetail
        {
            get
            {
                return MasterCustomerId != null;
            }
            set { }
        }

        public int? MasterCustomerId { get; set; }

        public CustomerDocumentItemModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
