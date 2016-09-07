using EasyLOB.Library;
using EasyLOB.Library.Mvc;
using System;
using System.Web.Mvc;

namespace Chinook.Mvc
{
    public partial class ChinookTasksController
    {
        // GET: Tasks/ExportGenre
        [HttpGet]
        public ActionResult ExportGenre()
        {
            if (!IsAuthorized("ExportGenre", OperationResult))
            {
                return View("OperationResult", new OperationResultModel(OperationResult));
            }

            TaskModel viewModel = new TaskModel();

            return View(viewModel);
        }

        // POST: Tasks/ExportGenre
        [HttpPost]
        public ActionResult ExportGenre(TaskModel viewModel)
        {
            viewModel.OperationResult.Clear();

            try
            {
                if (!IsAuthorized("ExportGenre", viewModel.OperationResult))
                {
                    return View("OperationResult", new OperationResultModel(viewModel.OperationResult));
                }
                else if (ValidateModelState())
                {
                    string fileDirectory = Server.MapPath(LibraryHelper.AppSettings<string>("DirectoryExport"));
                    string filePath;

                    if (Application.ExportGenreXLSX(viewModel.OperationResult, fileDirectory, out filePath))
                    {
                        return JsonResultSuccess(filePath);
                    }
                }
            }
            catch (Exception exception)
            {
                viewModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(viewModel.OperationResult);
        }
    }
}