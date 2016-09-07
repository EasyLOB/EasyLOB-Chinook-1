using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Security;

namespace Chinook.Mvc
{
    public partial class CustomerItemModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public CustomerViewModel Customer { get; set; }

        public string ControllerAction { get; set; }

        public bool IsMasterDetail
        {
            get
            {
                return MasterSupportRepId != null;
            }
            set { }
        }

        public int? MasterSupportRepId { get; set; }

        public CustomerItemModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
