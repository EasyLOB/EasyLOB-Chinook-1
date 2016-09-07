using Chinook.Mvc.Resources.Tasks;
using Chinook.Persistence;
using EasyLOB.Library.Mvc;
using System;
using System.Web.Mvc;

namespace Chinook.Mvc
{
    public partial class ChinookTasksController
    {
        // GET: Tasks/Reset
        [HttpGet]
        public ActionResult Reset()
        {
            if (!IsAuthorized("Reset", OperationResult))
            {
                return View("OperationResult", new OperationResultModel(OperationResult));
            }

            TaskModel viewModel = new TaskModel();

            return View(viewModel);
        }

        // POST: Tasks/Reset
        [HttpPost]
        public ActionResult Reset(TaskModel viewModel)
        {
            viewModel.OperationResult.Clear();

            try
            {
                if (!IsAuthorized("Reset", viewModel.OperationResult))
                {
                    return View("OperationResult", new OperationResultModel(viewModel.OperationResult));
                }
                else if (ValidateModelState())
                {
                    IChinookUnitOfWork unitOfWork = DependencyResolver.Current.GetService<IChinookUnitOfWork>();
                    Application.Reset(viewModel.OperationResult, unitOfWork);

                    viewModel.OperationResult.StatusMessage = TaskResetResources.Task + " Ok";
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