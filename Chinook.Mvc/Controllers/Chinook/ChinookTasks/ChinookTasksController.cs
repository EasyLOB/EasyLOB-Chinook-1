using Chinook.Application;
using EasyLOB.Library.Mvc;

namespace Chinook.Mvc
{
    public partial class ChinookTasksController : TasksController
    {
        #region Properties

        protected override string Domain
        {
            get { return "Chinook"; }
        }

        protected IChinookApplication Application { get; set; }

        #endregion Properties

        #region Methods

        public ChinookTasksController(IChinookApplication application)
        {
            Application = application;
        }

        #endregion Methods
    }
}