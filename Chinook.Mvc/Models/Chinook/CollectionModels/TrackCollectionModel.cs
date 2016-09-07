using EasyLOB.Library;
using EasyLOB.Security;

namespace Chinook.Mvc
{
    public partial class TrackCollectionModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public string ControllerAction { get; set; }

        public string MasterControllerAction { get; set; }

        public bool IsMasterDetail
        {
            get
            {
                return MasterAlbumId != null || MasterGenreId != null || MasterMediaTypeId != null;
            }
            set { }
        }

        public int? MasterAlbumId { get; set; }

        public int? MasterGenreId { get; set; }

        public int? MasterMediaTypeId { get; set; }

        public TrackCollectionModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
