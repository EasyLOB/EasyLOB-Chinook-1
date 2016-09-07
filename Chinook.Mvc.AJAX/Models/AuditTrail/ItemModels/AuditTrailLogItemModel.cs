using EasyLOB.AuditTrail.Data;
using EasyLOB.Library;
using EasyLOB.Security;

namespace EasyLOB.AuditTrail.Mvc
{
    public partial class AuditTrailLogItemModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public AuditTrailLogViewModel AuditTrailLog { get; set; }

        public string ControllerAction { get; set; }

        public bool IsMasterDetail
        {
            get
            {
                return false;
            }
            set { }
        }

        public AuditTrailLogItemModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
