using Chinook.Data;
using EasyLOB.Persistence;

namespace Chinook.Persistence
{
    public class ChinookPlaylistTrackRepositoryNH : ChinookGenericRepositoryNH<PlaylistTrack> // !!!
    {
        #region Methods

        public ChinookPlaylistTrackRepositoryNH(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override PlaylistTrack GetById(object[] ids)
        {
            return Session.Load<PlaylistTrack>(new PlaylistTrack((int)ids[0], (int)ids[1]));
        }

        #endregion Methods
    }
}