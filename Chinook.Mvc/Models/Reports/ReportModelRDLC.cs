using EasyLOB.Library;

namespace EasyLOB.Data
{
    public class ReportModelRDLC
    {
        public ZOperationResult OperationResult { get; set; }

        public string ReportDirectory { get; set; }

        public string ReportName { get; set; }

        //public IEnumerable Data { get; set; }

        public ReportModelRDLC()
        {
            OperationResult = new ZOperationResult();
        }
    }
}