using System;
using System.IO;
using System.Web.Mvc;
using EasyLOB.Library;
using EasyLOB.Library.Mvc;

namespace Chinook.Mvc
{
    public partial class ChinookTasksController
    {
        // GET: Tasks/ExportAlbumByArtist
        [HttpGet]
        public ActionResult ExportAlbumByArtist()
        {
            if (!IsAuthorized("ExportAlbumByArtist", OperationResult))
            {
                return View("OperationResult", new OperationResultModel(OperationResult));
            }

            TaskModel viewModel = new TaskModel();

            return View(viewModel);
        }

        // POST: Tasks/ExportAlbumByArtist
        [HttpPost]
        public ActionResult ExportAlbumByArtist(TaskModel viewModel)
        {
            viewModel.OperationResult.Clear();

            try
            {
                if (!IsAuthorized("ExportAlbumByArtist", viewModel.OperationResult))
                {
                    return View("OperationResult", new OperationResultModel(viewModel.OperationResult));
                }
                else if (ValidateModelState())
                {
                    string templateDirectory = Server.MapPath(LibraryHelper.AppSettings<string>("DirectoryTemplate"));
                    string fileDirectory = Server.MapPath(LibraryHelper.AppSettings<string>("DirectoryExport"));
                    string filePath;

                    if (Application.ExportAlbumByArtistXLSX(viewModel.OperationResult, templateDirectory, fileDirectory, out filePath))
                    {
                        byte[] file = System.IO.File.ReadAllBytes(filePath);
                        return File(file, LibraryHelper.GetContentType(ZFileTypes.ftXLSX), Path.GetFileName(filePath));
                    }
                }
            }
            catch (Exception exception)
            {
                viewModel.OperationResult.ParseException(exception);
            }

            return View(viewModel);
        }
    }
}