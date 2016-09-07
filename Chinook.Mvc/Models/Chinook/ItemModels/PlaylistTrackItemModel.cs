using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Security;

namespace Chinook.Mvc
{
    public partial class PlaylistTrackItemModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public PlaylistTrackViewModel PlaylistTrack { get; set; }

        public string ControllerAction { get; set; }

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

        public PlaylistTrackItemModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
