using Newtonsoft.Json;
using Syncfusion.JavaScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Chinook.Application;
using Chinook.Data;
using Chinook.Data.Resources;
using EasyLOB;
using EasyLOB.Data;
using EasyLOB.Library;
using EasyLOB.Library.Mvc;
using EasyLOB.Library.Syncfusion;

namespace Chinook.Mvc
{
    public class AlbumController : BaseControllerSCRUDApplication<AlbumDTO, Album>
    {
        #region Methods

        public AlbumController(IChinookGenericApplicationDTO<AlbumDTO, Album> application)
        {
            Application = application;            
        }
        
        #endregion
        
        #region Methods SCRUD

        // GET: Album
        // GET: Album/Index
        [HttpGet]
        public ActionResult Index(int? masterArtistId = null)
        {
            ClearUrlDictionary();

            AlbumCollectionModel albumCollectionModel = new AlbumCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index",
                MasterArtistId = masterArtistId
            };

            try
            {
                IsSearch(albumCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                albumCollectionModel.OperationResult.ParseException(exception);
            }

            return View(albumCollectionModel);
        }        

        // GET & POST: Album/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterUrl = null, string masterControllerAction = null, int? masterArtistId = null)
        {
            WriteUrlDictionary(masterUrl);

            AlbumCollectionModel albumCollectionModel = new AlbumCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction,
                MasterArtistId = masterArtistId
            };

            try
            {
                IsSearch(albumCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                albumCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView(albumCollectionModel);
        }

        // GET & POST: Album/Lookup
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

            return PartialView("_AlbumLookup", lookupModel);
        }

        // GET: Album/Create
        [HttpGet]
        public ActionResult Create(int? masterArtistId = null)
        {
            AlbumItemModel albumItemModel = new AlbumItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Album = new AlbumViewModel(),
                ControllerAction = "Create",
                MasterArtistId = masterArtistId
            };

            try
            {
                IsCreate(albumItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                albumItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(albumItemModel);
        }

        // POST: Album/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlbumItemModel albumItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Create(albumItemModel.OperationResult, (AlbumDTO)albumItemModel.Album.ToDTO()))
                    {
                        return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                    }
                }
            }
            catch (Exception exception)
            {
                albumItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(albumItemModel.OperationResult);
        }

        // GET: Album/Read/1
        [HttpGet]
        public ActionResult Read(int? albumId, int? masterArtistId = null)
        {
            AlbumItemModel albumItemModel = new AlbumItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Album = new AlbumViewModel(),
                ControllerAction = "Read",
                MasterArtistId = masterArtistId
            };
            
            try
            {
                AlbumDTO albumDTO = Application.GetById(albumItemModel.OperationResult, new object[] { albumId });
                if (albumDTO != null)
                {
                    albumItemModel.Album = new AlbumViewModel(albumDTO);
                }
            }
            catch (Exception exception)
            {
                albumItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(albumItemModel);
        }

        // GET: Album/Update/1
        [HttpGet]
        public ActionResult Update(int? albumId, int? masterArtistId = null)
        {
            AlbumItemModel albumItemModel = new AlbumItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Album = new AlbumViewModel(),
                ControllerAction = "Update",
                MasterArtistId = masterArtistId
            };
            
            try
            {
                AlbumDTO albumDTO = Application.GetById(albumItemModel.OperationResult, new object[] { albumId });
                if (albumDTO != null)
                {
                    albumItemModel.Album = new AlbumViewModel(albumDTO);
                }
            }
            catch (Exception exception)
            {
                albumItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(albumItemModel);
        }

        // POST: Album/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(AlbumItemModel albumItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Update(albumItemModel.OperationResult, (AlbumDTO)albumItemModel.Album.ToDTO()))
                    {
                        return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                    }
                }
            }
            catch (Exception exception)
            {
                albumItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(albumItemModel.OperationResult);
        }

        // GET: Album/Delete/1
        [HttpGet]
        public ActionResult Delete(int? albumId, int? masterArtistId = null)
        {
            AlbumItemModel albumItemModel = new AlbumItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Album = new AlbumViewModel(),
                ControllerAction = "Delete",
                MasterArtistId = masterArtistId
            };
            
            try
            {
                AlbumDTO albumDTO = Application.GetById(albumItemModel.OperationResult, new object[] { albumId });
                if (albumDTO != null)
                {
                    albumItemModel.Album = new AlbumViewModel(albumDTO);
                }
            }
            catch (Exception exception)
            {
                albumItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(albumItemModel);
        }

        // POST: Album/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(AlbumItemModel albumItemModel)
        {
            try
            {
                if (Application.Delete(albumItemModel.OperationResult, (AlbumDTO)albumItemModel.Album.ToDTO()))
                {
                    return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                }
            }
            catch (Exception exception)
            {
                albumItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(albumItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Album/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<AlbumViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Album), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<AlbumViewModel, AlbumDTO, Album>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
                    if (dataManager.RequiresCounts)
                    {
                        countAll = Application.Count(where, args.ToArray());
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

        // POST: Album/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, AlbumResources.EntitySingular + ".xlsx");
        }

        // POST: Album/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, AlbumResources.EntitySingular + ".pdf");
        }

        // POST: Album/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, AlbumResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}