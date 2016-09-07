using EasyLOB.Library;

namespace EasyLOB.Data
{
    public class ReportModelRDL
    {
        public ZOperationResult OperationResult { get; set; }

        public string ReportDirectory { get; set; }

        public string ReportName { get; set; }

        public ReportModelRDL()
        {
            OperationResult = new ZOperationResult();
        }
    }
}