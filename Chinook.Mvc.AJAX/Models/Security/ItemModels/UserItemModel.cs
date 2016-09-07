using EasyLOB.Security.Data;
using EasyLOB.Library;
using EasyLOB.Security;

namespace EasyLOB.Security.Mvc
{
    public partial class UserItemModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public UserViewModel User { get; set; }

        public string ControllerAction { get; set; }

        public bool IsMasterDetail
        {
            get
            {
                return false;
            }
            set { }
        }

        public UserItemModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
