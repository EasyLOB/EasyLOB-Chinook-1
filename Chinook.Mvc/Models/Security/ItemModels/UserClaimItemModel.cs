using EasyLOB.Security.Data;
using EasyLOB.Library;
using EasyLOB.Security;

namespace EasyLOB.Security.Mvc
{
    public partial class UserClaimItemModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public UserClaimViewModel UserClaim { get; set; }

        public string ControllerAction { get; set; }

        public bool IsMasterDetail
        {
            get
            {
                return MasterUserId != null;
            }
            set { }
        }

        public string MasterUserId { get; set; }

        public UserClaimItemModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
