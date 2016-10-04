using Newtonsoft.Json;
using Syncfusion.JavaScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Chinook.Data;
using Chinook.Data.Resources;
using Chinook.Persistence;
using EasyLOB;
using EasyLOB.Data;
using EasyLOB.Library;
using EasyLOB.Library.Mvc;
using EasyLOB.Library.Syncfusion;

namespace Chinook.Mvc
{
    public class GenreController : BaseControllerSCRUDPersistence<Genre>
    {
        #region Methods

        public GenreController(IChinookUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        
        #endregion
        
        #region Methods SCRUD

        // GET: Genre
        // GET: Genre/Index
        [HttpGet]
        public ActionResult Index()
        {
            ClearUrlDictionary();

            GenreCollectionModel genreCollectionModel = new GenreCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index"
            };

            try
            {
                IsSearch(genreCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                genreCollectionModel.OperationResult.ParseException(exception);
            }

            return View(genreCollectionModel);
        }

        // GET & POST: Genre/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterUrl = null, string masterControllerAction = null)
        {
            WriteUrlDictionary(masterUrl);

            GenreCollectionModel genreCollectionModel = new GenreCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction
            };

            try
            {
                IsSearch(genreCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                genreCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView(genreCollectionModel);
        }

        // GET & POST: Genre/Lookup
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Lookup(string text, string valueId, List<LookupModelElement> elements = null)
        {
            LookupModel lookupModel = new LookupModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Text = text,
                ValueId = valueId,
                Elements = elements
            };

            try
            {
                IsSearch(lookupModel.OperationResult);
            }
            catch (Exception exception)
            {
                lookupModel.OperationResult.ParseException(exception);
            }

            return PartialView("_GenreLookup", lookupModel);
        }

        // GET: Genre/Create
        [HttpGet]
        public ActionResult Create()
        {
            GenreItemModel genreItemModel = new GenreItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Genre = new GenreViewModel(),
                ControllerAction = "Create"
            };

            try
            {
                IsCreate(genreItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                genreItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(genreItemModel);
        }

        // POST: Genre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GenreItemModel genreItemModel)
        {
            try
            {
                if (IsCreate(genreItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Create(genreItemModel.OperationResult, (Genre)genreItemModel.Genre.ToData()))
                        {
                            UnitOfWork.Save(genreItemModel.OperationResult);
                            {
                                return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                genreItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(genreItemModel.OperationResult);
        }

        // GET: Genre/Read/1
        [HttpGet]
        public ActionResult Read(int? genreId)
        {
            GenreItemModel genreItemModel = new GenreItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Genre = new GenreViewModel(),
                ControllerAction = "Read"
            };
            
            try
            {
                if (IsRead(genreItemModel.OperationResult))
                {
                    Genre genre = Repository.GetById(new object[] { genreId });
                    if (genre != null)
                    {
                        genreItemModel.Genre = new GenreViewModel(genre);
                    }
                }
            }
            catch (Exception exception)
            {
                genreItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(genreItemModel);
        }

        // GET: Genre/Update/1
        [HttpGet]
        public ActionResult Update(int? genreId)
        {
            GenreItemModel genreItemModel = new GenreItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Genre = new GenreViewModel(),
                ControllerAction = "Update"
            };
            
            try
            {
                if (IsUpdate(genreItemModel.OperationResult))
                {
                    Genre genre = Repository.GetById(new object[] { genreId });
                    if (genre != null)
                    {
                        genreItemModel.Genre = new GenreViewModel(genre);
                    }
                }
            }
            catch (Exception exception)
            {
                genreItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(genreItemModel);
        }

        // POST: Genre/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GenreItemModel genreItemModel)
        {
            try
            {
                if (IsUpdate(genreItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Update(genreItemModel.OperationResult, (Genre)genreItemModel.Genre.ToData()))
                        {
                            if (UnitOfWork.Save(genreItemModel.OperationResult))
                            {
                                return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                genreItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(genreItemModel.OperationResult);
        }

        // GET: Genre/Delete/1
        [HttpGet]
        public ActionResult Delete(int? genreId)
        {
            GenreItemModel genreItemModel = new GenreItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Genre = new GenreViewModel(),
                ControllerAction = "Delete"
            };
            
            try
            {
                if (IsDelete(genreItemModel.OperationResult))
                {
                    Genre genre = Repository.GetById(new object[] { genreId });
                    if (genre != null)
                    {
                        genreItemModel.Genre = new GenreViewModel(genre);
                    }
                }
            }
            catch (Exception exception)
            {
                genreItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(genreItemModel);
        }

        // POST: Genre/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(GenreItemModel genreItemModel)
        {
            try
            {
                if (IsDelete(genreItemModel.OperationResult))
                {
                    if (Repository.Delete(genreItemModel.OperationResult, (Genre)genreItemModel.Genre.ToData()))
                    {
                        if (UnitOfWork.Save(genreItemModel.OperationResult))
                        {
                            return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                genreItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(genreItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Genre/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<GenreViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();
            
            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Genre), UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<GenreViewModel, GenreDTO, Genre>.ToViewList(Repository.Select(where, args.ToArray(), orderBy, dataManager.Skip, take));
        
                    if (dataManager.RequiresCounts)
                    {
                        countAll = Repository.Count(where, args.ToArray());
                    }
                }
                catch (Exception exception)
                {
                    operationResult.ParseException(exception);
                }

                if (!operationResult.Ok)
                {
                    throw new InvalidOperationException(operationResult.Text);
                }
            }

            return Json(JsonConvert.SerializeObject(new { result = data, count = countAll }), JsonRequestBehavior.AllowGet);
        } 

        // POST: Genre/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, GenreResources.EntitySingular + ".xlsx");
        }

        // POST: Genre/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, GenreResources.EntitySingular + ".pdf");
        }

        // POST: Genre/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, GenreResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}