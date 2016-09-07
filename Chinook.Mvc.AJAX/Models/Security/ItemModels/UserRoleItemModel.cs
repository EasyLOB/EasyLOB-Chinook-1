using EasyLOB.Security.Data;
using EasyLOB.Library;
using EasyLOB.Security;

namespace EasyLOB.Security.Mvc
{
    public partial class UserRoleItemModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public UserRoleViewModel UserRole { get; set; }

        public string ControllerAction { get; set; }

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

        public UserRoleItemModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
