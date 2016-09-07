using System;
using System.IO;
using System.Web.Mvc;
using Chinook.Application;
using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Library.Mvc;
using Chinook.Persistence;

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

        // POST: Tasks/ImportGenre
        [HttpPost]
        public ActionResult ImportGenre(TaskImportGenreModel viewModel)
        {
            viewModel.OperationResult.Clear();

            string path = "";

            try
            {
                if (!IsAuthorized("ImportGenre", viewModel.OperationResult))
                {
                    return View("OperationResult", new OperationResultModel(viewModel.OperationResult));
                }
                else if (ValidateModelState())
                {
                    if (viewModel.Upload != null && viewModel.Upload.ContentLength > 0)
                    {
                        // Save file

                        string file = Path.GetFileName(viewModel.Upload.FileName);
                        path = Path.Combine(Server.MapPath(LibraryHelper.AppSettings<string>("DirectoryImport")), file);
                        viewModel.Upload.SaveAs(path);

                        // Read file and Create Genre

                        // Application
                        IChinookGenericApplication<Genre> genreApplication =
                            DependencyResolver.Current.GetService<IChinookGenericApplication<Genre>>();
                        Application.ImportGenreTXTApplication(viewModel.OperationResult, path, genreApplication);

                        // Persistence
                        IChinookUnitOfWork unitOfWork =
                            DependencyResolver.Current.GetService<IChinookUnitOfWork>();
                        Application.ImportGenreTXTPersistence(viewModel.OperationResult, path, unitOfWork);
                    }
                }
            }
            catch (Exception exception)
            {
                viewModel.OperationResult.ParseException(exception);
            }
            finally
            {
                if (!String.IsNullOrEmpty(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            return View(viewModel);
        }

        /*
        // TaskModel

        // GET: Tasks/ImportGenre
        [HttpGet]
        public ActionResult ImportGenre()
        {
            if (!IsAuthorized("ImportGenre", OperationResult))
            {
                return View("OperationResult", new OperationResultModel(OperationResult));
            }

            TaskModel viewModel = new TaskModel();

            return View(viewModel);
        }

        // <input type="file" name="upload" id="upload" accept="text/plain" />

        // POST: Tasks/ImportGenre
        [HttpPost]
        public ActionResult ImportGenre(TaskModel viewModel)
        {
            viewModel.OperationResult.Clear();

            string path = "";

            try
            {
                if (!IsAuthorized("ImportGenre", viewModel.OperationResult))
                {
                    return View("OperationResult", new OperationResultModel(viewModel.OperationResult));
                }
                else if (ValidateModelState())
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase upload = Request.Files[0];

                        if (upload != null && upload.ContentLength > 0)
                        {
                            // Save file

                            string file = Path.GetFileName(upload.FileName);
                            path = Path.Combine(Server.MapPath(LibraryHelper.AppSettings<string>("DirectoryImport")), file);
                            upload.SaveAs(path);

                            // Read file and Create Genre

                            // Application
                            IChinookGenericApplication<Genre> genreApplication =
                                DependencyResolver.Current.GetService<IChinookGenericApplication<Genre>>();
                            Application.ImportGenreTXTApplication(viewModel.OperationResult, path, genreApplication);

                            // Persistence
                            IChinookUnitOfWork unitOfWork =
                                DependencyResolver.Current.GetService<IChinookUnitOfWork>();
                            Application.ImportGenreTXTPersistence(viewModel.OperationResult, path, unitOfWork);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                viewModel.OperationResult.ParseException(exception);
            }
            finally
            {
                if (!String.IsNullOrEmpty(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            return View(viewModel);
        }
        */
    }
}