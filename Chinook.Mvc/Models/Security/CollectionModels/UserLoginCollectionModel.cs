using EasyLOB.Library;
using EasyLOB.Security;

namespace EasyLOB.Security.Mvc
{
    public partial class UserLoginCollectionModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public string ControllerAction { get; set; }

        public string MasterControllerAction { get; set; }

        public bool IsMasterDetail
        {
            get
            {
                return MasterUserId != null;
            }
            set { }
        }

        public string MasterUserId { get; set; }

        public UserLoginCollectionModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
