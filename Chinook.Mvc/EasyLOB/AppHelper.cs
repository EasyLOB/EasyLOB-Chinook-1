using System;

namespace EasyLOB
{
    public static partial class AppHelper
    {
        public static string DocumentTitle(string pageTitle)
        {
            return AppDefaults.AppName +
                (!String.IsNullOrEmpty(AppDefaults.AppVersion) ? " " + AppDefaults.AppVersion : "") +
                (!String.IsNullOrEmpty(pageTitle) ? AppDefaults.TitleSeparator + pageTitle : "");
        }

        public static string PageTitle(string entity, string action)
        {
            return entity +
                AppDefaults.TitleSeparator + action;
        }
    }
}