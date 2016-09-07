using System;
using System.Web.Mvc;
using EasyLOB.Library.Mvc;
using Chinook.Mvc.Resources.Tasks;
using EasyLOB.Library;

namespace Chinook.Mvc
{
    public partial class ChinookTasksController
    {
        // GET: Tasks/Clean
        [HttpGet]
        public ActionResult Clean()
        {
            if (!IsAuthorized("Clean", OperationResult))
            {
                return View("OperationResult", new OperationResultModel(OperationResult));
            }

            TaskModel viewModel = new TaskModel();

            return View(viewModel);
        }

        // POST: Tasks/Clean
        [HttpPost]
        public ActionResult Clean(TaskModel viewModel)
        {
            viewModel.OperationResult.Clear();

            try
            {
                if (!IsAuthorized("Clean", viewModel.OperationResult))
                {
                    return View("OperationResult", new OperationResultModel(viewModel.OperationResult));
                }
                else if (ValidateModelState())
                {
                    // Z-Export

                    if (viewModel.OperationResult.Ok)
                    {
                        Application.Clean(viewModel.OperationResult, Server.MapPath(LibraryHelper.AppSettings<string>("DirectoryExport")));
                    }

                    // Z-Import

                    if (viewModel.OperationResult.Ok)
                    {
                        Application.Clean(viewModel.OperationResult, Server.MapPath(LibraryHelper.AppSettings<string>("DirectoryImport")));
                    }

                    viewModel.OperationResult.StatusMessage = TaskCleanResources.Task + " Ok";
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