using System.Linq;
using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookPlaylistRepositoryLINQ2DB : ChinookGenericRepositoryLINQ2DB<Playlist>
    {
        #region Methods

        public ChinookPlaylistRepositoryLINQ2DB(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override IQueryable<Playlist> Join(IQueryable<Playlist> query)
        {
            return
                from playlist in query
                select new Playlist
                {
                    PlaylistId = playlist.PlaylistId,
                    Name = playlist.Name
                };
        }

        #endregion Methods
    }
}

