using EasyLOB.Library;
using EasyLOB.Security;

namespace Chinook.Mvc
{
    public partial class ArtistCollectionModel
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

        public ArtistCollectionModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
