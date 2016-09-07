using EasyLOB;
using EasyLOB.Library.Resources;
using EasyLOB.Log;
using Microsoft.Practices.Unity;
using System;

namespace Chinook.Service
{
    public partial class ChinookServiceHelper
    {
        #region Properties

        private static ILogManager LogManager;

        private static UnityContainer Container = new UnityContainer();

        #endregion Properties

        #region Methods

        static ChinookServiceHelper()
        {
            // Unity

            UnityHelper.RegisterMappings(Container);

            LogManager = (ILogManager)Container.Resolve<ILogManager>();
        }

        private static string GetLog(string activity, string description)
        {
            return
                String.Format(PatternResources.DataFormat_DateTime, DateTime.Now.ToString()) +
                " - " + activity +
                (String.IsNullOrEmpty(description) ? "" : ": " + description);
        }

        #endregion Methods
    }
}