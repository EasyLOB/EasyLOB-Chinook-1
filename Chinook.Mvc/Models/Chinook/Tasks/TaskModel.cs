using EasyLOB.Library;

namespace Chinook.Mvc
{
    public class TaskModel
    {
        public ZOperationResult OperationResult { get; set; }

        public TaskModel()
        {
            OperationResult = new ZOperationResult();
        }
    }
}