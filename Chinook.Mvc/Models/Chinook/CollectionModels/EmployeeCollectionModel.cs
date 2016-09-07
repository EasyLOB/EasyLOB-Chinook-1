using EasyLOB.Library;
using EasyLOB.Security;

namespace Chinook.Mvc
{
    public partial class EmployeeCollectionModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public string ControllerAction { get; set; }

        public string MasterControllerAction { get; set; }

        public bool IsMasterDetail
        {
            get
            {
                return MasterReportsTo != null;
            }
            set { }
        }

        public int? MasterReportsTo { get; set; }

        public EmployeeCollectionModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
