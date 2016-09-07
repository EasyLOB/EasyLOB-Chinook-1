using System.Web;
using EasyLOB.Library;

namespace Chinook.Mvc
{
    public class TaskImportGenreModel
    {
        public ZOperationResult OperationResult { get; set; }

        public HttpPostedFileBase Upload { get; set; }

        public TaskImportGenreModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}