using EasyLOB.Security.Data;
using EasyLOB.Library;
using EasyLOB.Security;

namespace EasyLOB.Security.Mvc
{
    public partial class RoleItemModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public RoleViewModel Role { get; set; }

        public string ControllerAction { get; set; }

        public bool IsMasterDetail
        {
            get
            {
                return false;
            }
            set { }
        }

        public RoleItemModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
