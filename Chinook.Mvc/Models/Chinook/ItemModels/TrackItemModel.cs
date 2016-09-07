using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Security;

namespace Chinook.Mvc
{
    public partial class TrackItemModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public TrackViewModel Track { get; set; }

        public string ControllerAction { get; set; }

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

        public TrackItemModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
