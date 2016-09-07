using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Security;

namespace Chinook.Mvc
{
    public partial class AlbumItemModel
    {
        public ZOperationResult OperationResult { get; set; }

        public ZIsSecurityOperations IsSecurityOperations { get; set; }

        public AlbumViewModel Album { get; set; }

        public string ControllerAction { get; set; }

        public bool IsMasterDetail
        {
            get
            {
                return MasterArtistId != null;
            }
            set { }
        }

        public int? MasterArtistId { get; set; }

        public AlbumItemModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}
