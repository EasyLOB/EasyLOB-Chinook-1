using Chinook.Persistence;
using EasyLOB.AuditTrail;
using EasyLOB.Library;
using EasyLOB.Log;
using System;
using System.IO;

namespace Chinook.Application
{
    public partial class ChinookApplication : IChinookApplication
    {
        #region Properties

        protected IAuditTrailManager _auditTrailManager;

        public IAuditTrailManager AuditTrailManager
        {
            get { return _auditTrailManager; }
        }

        protected ILogManager _logManager;

        public ILogManager LogManager
        {
            get { return _logManager; }
        }

        #endregion Properties

        #region Methods

        public ChinookApplication(IChinookUnitOfWork unitOfWork, IAuditTrailManager auditTrailManager, ILogManager logManager)
        {
            _auditTrailManager = auditTrailManager;
            _logManager = logManager;
        }

        public bool Clean(ZOperationResult operationResult, string directory)
        {
            try
            {
                string[] paths;
                //int index;

                paths = Directory.GetFiles(directory);
                //index = 1;
                foreach (string path in paths)
                {
                    File.Delete(path);
                    //operationResult.AddOperationError(index++.ToString(), path);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return operationResult.Ok;
        }

        public bool Reset(ZOperationResult operationResult, IChinookUnitOfWork unitOfWork)
        {
            try
            {
                unitOfWork.ExecuteSQL("DELETE FROM InvoiceLine WHERE InvoiceLineId > 2240");                
                unitOfWork.ExecuteSQL("DELETE FROM Invoice WHERE InvoiceId > 412");

                unitOfWork.ExecuteSQL("DELETE FROM CustomerDocument");
                unitOfWork.ExecuteSQL("DELETE FROM Customer WHERE CustomerId > 59");
                unitOfWork.ExecuteSQL("DELETE FROM Employee WHERE EmployeeId > 8");

                unitOfWork.ExecuteSQL("DELETE FROM PlaylistTrack WHERE PlaylistTrackId > 18 OR TrackId > 3503");
                unitOfWork.ExecuteSQL("DELETE FROM Track WHERE TrackId > 3503");
                unitOfWork.ExecuteSQL("DELETE FROM Playlist WHERE PlaylistId > 18");

                unitOfWork.ExecuteSQL("DELETE FROM Genre WHERE GenreId > 25");
                unitOfWork.ExecuteSQL("DELETE FROM MediaType WHERE MediaTypeId > 5");
                unitOfWork.ExecuteSQL("DELETE FROM Album WHERE AlbumId > 347");
                unitOfWork.ExecuteSQL("DELETE FROM Artist WHERE ArtistId > 275");
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return operationResult.Ok;
        }

        #endregion Methods
    }
}