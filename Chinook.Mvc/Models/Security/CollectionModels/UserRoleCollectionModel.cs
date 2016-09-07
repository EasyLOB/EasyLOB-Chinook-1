using EasyLOB.Library;
using EasyLOB.Security;

namespace EasyLOB.Security.Mvc
{
    public partial class UserRoleCollectionModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public string ControllerAction { get; set; }

        public string MasterControllerAction { get; set; }

        public bool IsMasterDetail
        {
            get
            {
                return MasterRoleId != null || MasterUserId != null;
            }
            set { }
        }

        public string MasterRoleId { get; set; }

        public string MasterUserId { get; set; }

        public UserRoleCollectionModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
