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
    public class TrackController : BaseControllerSCRUDApplication<TrackDTO, Track>
    {
        #region Methods

        public TrackController(IChinookGenericApplicationDTO<TrackDTO, Track> application)
        {
            Application = application;            
        }
        
        #endregion
        
        #region Methods SCRUD

        // GET: Track
        // GET: Track/Index
        [HttpGet]
        public ActionResult Index(int? masterAlbumId = null, int? masterGenreId = null, int? masterMediaTypeId = null)
        {
            ClearUrlDictionary();

            TrackCollectionModel trackCollectionModel = new TrackCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index",
                MasterAlbumId = masterAlbumId, MasterGenreId = masterGenreId, MasterMediaTypeId = masterMediaTypeId
            };

            try
            {
                IsSearch(trackCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                trackCollectionModel.OperationResult.ParseException(exception);
            }

            return View(trackCollectionModel);
        }        

        // GET & POST: Track/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterUrl = null, string masterControllerAction = null, int? masterAlbumId = null, int? masterGenreId = null, int? masterMediaTypeId = null)
        {
            WriteUrlDictionary(masterUrl);

            TrackCollectionModel trackCollectionModel = new TrackCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction,
                MasterAlbumId = masterAlbumId, MasterGenreId = masterGenreId, MasterMediaTypeId = masterMediaTypeId
            };

            try
            {
                IsSearch(trackCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                trackCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView("_TrackCollection", trackCollectionModel);
        }

        // GET & POST: Track/Lookup
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

            return PartialView("_TrackLookup", lookupModel);
        }

        // GET: Track/Create
        [HttpGet]
        public ActionResult Create(int? masterAlbumId = null, int? masterGenreId = null, int? masterMediaTypeId = null)
        {
            TrackItemModel trackItemModel = new TrackItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Track = new TrackViewModel(),
                ControllerAction = "Create",
                MasterAlbumId = masterAlbumId, MasterGenreId = masterGenreId, MasterMediaTypeId = masterMediaTypeId
            };

            try
            {
                IsCreate(trackItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                trackItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(trackItemModel);
        }

        // POST: Track/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrackItemModel trackItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Create(trackItemModel.OperationResult, (TrackDTO)trackItemModel.Track.ToDTO()))
                    {
                        return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                    }
                }
            }
            catch (Exception exception)
            {
                trackItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(trackItemModel.OperationResult);
        }

        // GET: Track/Read/1
        [HttpGet]
        public ActionResult Read(int? trackId, int? masterAlbumId = null, int? masterGenreId = null, int? masterMediaTypeId = null)
        {
            TrackItemModel trackItemModel = new TrackItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Track = new TrackViewModel(),
                ControllerAction = "Read",
                MasterAlbumId = masterAlbumId, MasterGenreId = masterGenreId, MasterMediaTypeId = masterMediaTypeId
            };
            
            try
            {
                TrackDTO trackDTO = Application.GetById(trackItemModel.OperationResult, new object[] { trackId });
                if (trackDTO != null)
                {
                    trackItemModel.Track = new TrackViewModel(trackDTO);
                }
            }
            catch (Exception exception)
            {
                trackItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(trackItemModel);
        }

        // GET: Track/Update/1
        [HttpGet]
        public ActionResult Update(int? trackId, int? masterAlbumId = null, int? masterGenreId = null, int? masterMediaTypeId = null)
        {
            TrackItemModel trackItemModel = new TrackItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Track = new TrackViewModel(),
                ControllerAction = "Update",
                MasterAlbumId = masterAlbumId, MasterGenreId = masterGenreId, MasterMediaTypeId = masterMediaTypeId
            };
            
            try
            {
                TrackDTO trackDTO = Application.GetById(trackItemModel.OperationResult, new object[] { trackId });
                if (trackDTO != null)
                {
                    trackItemModel.Track = new TrackViewModel(trackDTO);
                }
            }
            catch (Exception exception)
            {
                trackItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(trackItemModel);
        }

        // POST: Track/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(TrackItemModel trackItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Update(trackItemModel.OperationResult, (TrackDTO)trackItemModel.Track.ToDTO()))
                    {
                        return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                    }
                }
            }
            catch (Exception exception)
            {
                trackItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(trackItemModel.OperationResult);
        }

        // GET: Track/Delete/1
        [HttpGet]
        public ActionResult Delete(int? trackId, int? masterAlbumId = null, int? masterGenreId = null, int? masterMediaTypeId = null)
        {
            TrackItemModel trackItemModel = new TrackItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Track = new TrackViewModel(),
                ControllerAction = "Delete",
                MasterAlbumId = masterAlbumId, MasterGenreId = masterGenreId, MasterMediaTypeId = masterMediaTypeId
            };
            
            try
            {
                TrackDTO trackDTO = Application.GetById(trackItemModel.OperationResult, new object[] { trackId });
                if (trackDTO != null)
                {
                    trackItemModel.Track = new TrackViewModel(trackDTO);
                }
            }
            catch (Exception exception)
            {
                trackItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(trackItemModel);
        }

        // POST: Track/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TrackItemModel trackItemModel)
        {
            try
            {
                if (Application.Delete(trackItemModel.OperationResult, (TrackDTO)trackItemModel.Track.ToDTO()))
                {
                    return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                }
            }
            catch (Exception exception)
            {
                trackItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(trackItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Track/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<TrackViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Track), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<TrackViewModel, TrackDTO, Track>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: Track/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, TrackResources.EntitySingular + ".xlsx");
        }

        // POST: Track/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, TrackResources.EntitySingular + ".pdf");
        }

        // POST: Track/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, TrackResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}