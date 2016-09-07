using Chinook.Data;
using EasyLOB.Library;
using Chinook.Persistence;

namespace Chinook.Application
{
    public interface IChinookApplication
    {
        #region Methods

        bool Clean(ZOperationResult operationResult, string directory);

        bool Reset(ZOperationResult operationResult, IChinookUnitOfWork unitOfWork);

        // TXT

        bool ExportGenreTXT(ZOperationResult operationResult, string fileDirectory, IChinookGenericApplication<Genre> genreApplication,
            out string filePath);

        bool ImportGenreTXTApplication(ZOperationResult operationResult, string filePath, IChinookGenericApplication<Genre> genreApplication);

        bool ImportGenreTXTPersistence(ZOperationResult operationResult, string filePath, IChinookUnitOfWork unitOfWork);

        // XLSX

        bool ExportAlbumByArtistXLSX(ZOperationResult operationResult, string templateDirectory, string fileDirectory,
            out string filePath);

        bool ExportGenreXLSX(ZOperationResult operationResult, string fileDirectory,
            out string filePath);

        bool ImportGenreXLSX(ZOperationResult operationResult, string filePath, IChinookGenericApplication<Genre> genreApplication);
        
        #endregion Methods
    }
}