using System.IO;
using System.Web.Mvc;

namespace EasyLOB.Library.Mvc
{
    public partial class TasksController : BaseControllerTask
    {
        #region Methods

        [HttpGet]
        public virtual ActionResult Download(string filePath)
        {
            string extension = System.IO.Path.GetExtension(filePath);
            ZFileTypes fileType = LibraryHelper.GetFileType(extension);

            return File(filePath, LibraryHelper.GetContentType(fileType), Path.GetFileName(filePath));
        }

        #endregion Methods
    }
}