using System;
using System.Web.Mvc;
using EasyLOB.Library.Mvc;

namespace Chinook.Mvc
{
    public partial class ChinookTasksController
    {
        // GET: Tasks/Simple
        [HttpGet]
        public ActionResult Simple()
        {
            if (!IsAuthorized("Simple", OperationResult))
            {
                return View("OperationResult", new OperationResultModel(OperationResult));
            }

            TaskSimpleModel viewModel = new TaskSimpleModel
            {
                XBoolean = false,
                XDateTime = null,
                XDouble = 0,
                XInteger = null,
                XString = "ABC"
            };

            return View(viewModel);
        }

        // POST: Tasks/Simple
        [HttpPost]
        public ActionResult Simple(TaskSimpleModel viewModel)
        {
            viewModel.OperationResult.Clear();

            try
            {
                if (!IsAuthorized("Simple", viewModel.OperationResult))
                {
                    return View("OperationResult", new OperationResultModel(viewModel.OperationResult));
                }
                else if (ValidateModelState())
                {
                    if (viewModel.XBoolean)
                    {
                        throw new Exception("My exception");
                    }

                    viewModel.OperationResult.StatusCode = "0";
                    viewModel.OperationResult.StatusMessage = "My status message";
                }
            }
            catch (Exception exception)
            {
                viewModel.OperationResult.ErrorCode = "0";
                viewModel.OperationResult.ErrorMessage = "My error message";

                viewModel.OperationResult.ParseException(exception);
            }
            finally
            {
            }

            return View(viewModel);
        }
    }
}