using EasyLOB.Library;
using EasyLOB.Security;

namespace Chinook.Mvc
{
    public partial class CustomerCollectionModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public string ControllerAction { get; set; }

        public string MasterControllerAction { get; set; }

        public bool IsMasterDetail
        {
            get
            {
                return MasterSupportRepId != null;
            }
            set { }
        }

        public int? MasterSupportRepId { get; set; }

        public CustomerCollectionModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
