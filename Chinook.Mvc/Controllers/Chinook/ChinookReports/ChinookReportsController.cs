using EasyLOB.Library.Mvc;

namespace Chinook.Mvc
{
    public partial class ChinookReportsController : ReportsController
    {
        #region Properties

        protected override string Domain
        {
            get { return "Chinook"; }
        }

        #endregion Properties
    }
}