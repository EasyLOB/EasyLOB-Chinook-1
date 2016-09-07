using EasyLOB.Library;
using EasyLOB.Security;

namespace Chinook.Mvc
{
    public partial class AlbumCollectionModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public string ControllerAction { get; set; }

        public string MasterControllerAction { get; set; }

        public bool IsMasterDetail
        {
            get
            {
                return MasterArtistId != null;
            }
            set { }
        }

        public int? MasterArtistId { get; set; }

        public AlbumCollectionModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
