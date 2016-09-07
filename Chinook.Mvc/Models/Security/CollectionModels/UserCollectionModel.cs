using EasyLOB.Library;
using EasyLOB.Security;

namespace EasyLOB.Security.Mvc
{
    public partial class UserCollectionModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public string ControllerAction { get; set; }

        public string MasterControllerAction { get; set; }

        public bool IsMasterDetail
        {
            get
            {
                return false;
            }
            set { }
        }

        public UserCollectionModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
