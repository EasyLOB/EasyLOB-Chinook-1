using EasyLOB.Library;
using EasyLOB.Security;

namespace Chinook.Mvc
{
    public partial class PlaylistTrackCollectionModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public string ControllerAction { get; set; }

        public string MasterControllerAction { get; set; }

        public bool IsMasterDetail
        {
            get
            {
                return MasterPlaylistId != null || MasterTrackId != null;
            }
            set { }
        }

        public int? MasterPlaylistId { get; set; }

        public int? MasterTrackId { get; set; }

        public PlaylistTrackCollectionModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
