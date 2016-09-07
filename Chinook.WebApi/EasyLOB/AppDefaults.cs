using Newtonsoft.Json;

namespace EasyLOB
{
    public static partial class AppDefaults // !!!
    {
        #region Properties

        public static string AppName {  get { return "Chinook";  } }

        public static string[] AppPath { get { return new string[] { "Chinook", "Account" }; } }

        public static string AppVersion { get { return "1.0"; } }

        public static string Domain { get { return "Chinook"; } }

        public static JsonSerializerSettings JsonSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    Formatting = Formatting.None,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
            }
        }

        public static string TitleSeparator { get { return " : "; } }

        #endregion Properties

        #region Properties CSS Class

        // Group
        //     Label
        //     Editor
        //         Lookup
        //     Validator

        public static string CssClassGroup { get { return "form-group z-group"; } }

        public static string CssClassLabel { get { return "control-label z-label"; } }

        public static string CssClassLabelRequired { get { return "control-label z-label-required"; } }

        public static string CssClassEditor { get { return "form-control input-sm z-editor"; } }

        public static string CssClassValidator { get { return "z-validator"; } }

        #endregion Properties CSS

        #region Properties Syncfusion

        public static int SyncfusionRecordsByPage { get { return 10; } }

        public static int SyncfusionRecordsByLookup { get { return 5; } }

        public static int SyncfusionRecordsBySearch { get { return 100; } }

        public static string SyncfusionTheme { get { return "bootstrap-theme"; } }

        #endregion Properties Syncfusion
    }
}