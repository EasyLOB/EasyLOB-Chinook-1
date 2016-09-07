using System;
using System.IO;
using System.Web.Mvc;
using Chinook.Application;
using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Library.Mvc;
using Chinook.Persistence;
using System.Web;

/*
HTML Input=“file” Accept Attribute File Type (CSV)
http://stackoverflow.com/questions/11832930/html-input-file-accept-attribute-file-type-csv

<input id = "fileSelect" type="file" accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel" />

For CSV files(.csv), use:
<input type = "file" accept=".csv" />

For Excel Files 2003-2007 (.xls), use:
<input type = "file" accept="application/vnd.ms-excel" />

For Excel Files 2010 (.xlsx), use:
<input type = "file" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" />

For Text Files(.txt) use:
<input type = "file" accept="text/plain" />

For Image Files(.png/.jpg/etc), use:
<input type = "file" accept="image/*" />

For HTML Files(.htm,.html), use:
<input type = "file" accept="text/html" />

For Video Files(.avi, .mpg, .mpeg, .mp4), use:
<input type = "file" accept="video/*" />

For Audio Files(.mp3, .wav, etc), use:
<input type = "file" accept="audio/*" />

For PDF Files, use:
<input type = "file" accept=".pdf" />

If you are trying to display Excel CSV files(.csv), do NOT use:

    text/csv
    application/csv
    text/comma-separated-values(works in Opera only).

If you are trying to display a particular file type(for example, a WAV or PDF), then this will almost always work:

    <input type = "file" accept= ".FILETYPE" />
 */

/*
Web.config

<system.web>
  <!-- 102400 Kb = 100 MB -->
  <httpRuntime maxRequestLength = "102400"/>
</system.web>
 */

namespace Chinook.Mvc
{
    public partial class ChinookTasksController
    {
        // GET: Tasks/ImportGenre
        [HttpGet]
        public ActionResult ImportGenre()
        {
            if (!IsAuthorized("ImportGenre", OperationResult))
            {
                return View("OperationResult", new OperationResultModel(OperationResult));
            }

            TaskImportGenreModel viewModel = new TaskImportGenreModel();

            return View(viewModel);
        }

        // POST: Tasks/ImportGenreAjax
        [HttpPost]
        public virtual ActionResult ImportGenreAjax()
        {
            ZOperationResult operationResult = new ZOperationResult();
            string path = "";
            bool isUploaded = false;

            try
            {
                if (!IsAuthorized("ImportGenre", operationResult))
                {
                    return View("OperationResult", new OperationResultModel(operationResult));
                }
                else
                {
                    HttpPostedFileBase upload = Request.Files["Upload"];
                    if (upload != null && upload.ContentLength != 0)
                    {
                        // Save file

                        string file = Path.GetFileName(upload.FileName);
                        path = Server.MapPath(LibraryHelper.AddDirectorySeparator(LibraryHelper.AppSettings<string>("DirectoryImport")) + file);
                        upload.SaveAs(path);

                        isUploaded = true;

                        // Read file and Create Genre

                        // Application
                        //IChinookGenericApplication<Genre> genreApplication =
                        //    DependencyResolver.Current.GetService<IChinookGenericApplication<Genre>>();
                        //Application.ImportGenreTXTApplication(operationResult, path, genreApplication);

                        // OR

                        // Persistence
                        //IChinookUnitOfWork unitOfWork =
                        //    DependencyResolver.Current.GetService<IChinookUnitOfWork>();
                        //Application.ImportGenreTXTPersistence(operationResult, path, unitOfWork);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }
            finally
            {
                if (!String.IsNullOrEmpty(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            if (operationResult.Ok)
            {
                return JsonResultSuccess(new { IsUploaded = isUploaded, OperationResult = operationResult });
            }
            else
            {
                return JsonResultFailure(new { OperationResult = operationResult });                
            }
        }
    }
}