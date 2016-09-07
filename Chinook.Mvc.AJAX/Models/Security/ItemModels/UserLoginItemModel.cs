using EasyLOB.Security.Data;
using EasyLOB.Library;
using EasyLOB.Security;

namespace EasyLOB.Security.Mvc
{
    public partial class UserLoginItemModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public UserLoginViewModel UserLogin { get; set; }

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

        public UserLoginItemModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
