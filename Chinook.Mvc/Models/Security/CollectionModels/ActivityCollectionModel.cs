using EasyLOB.Library;
using EasyLOB.Security;

namespace EasyLOB.Security.Mvc
{
    public partial class ActivityCollectionModel
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

        public ActivityCollectionModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
