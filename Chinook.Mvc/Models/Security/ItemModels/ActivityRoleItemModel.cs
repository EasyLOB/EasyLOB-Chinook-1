using EasyLOB.Security.Data;
using EasyLOB.Library;
using EasyLOB.Security;

namespace EasyLOB.Security.Mvc
{
    public partial class ActivityRoleItemModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public ActivityRoleViewModel ActivityRole { get; set; }

        public string ControllerAction { get; set; }

        public bool IsMasterDetail
        {
            get
            {
                return MasterActivityId != null || MasterRoleId != null;
            }
            set { }
        }

        public string MasterActivityId { get; set; }

        public string MasterRoleId { get; set; }

        public ActivityRoleItemModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
