using EasyLOB.Library;
using EasyLOB.Security;

namespace EasyLOB.Security.Mvc
{
    public partial class ActivityRoleCollectionModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public string ControllerAction { get; set; }

        public string MasterControllerAction { get; set; }

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

        public ActivityRoleCollectionModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
