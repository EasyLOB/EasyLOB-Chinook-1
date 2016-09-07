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
    public class PlaylistTrackController : BaseControllerSCRUDApplication<PlaylistTrackDTO, PlaylistTrack>
    {
        #region Methods

        public PlaylistTrackController(IChinookGenericApplicationDTO<PlaylistTrackDTO, PlaylistTrack> application)
        {
            Application = application;
        }
        
        #endregion
        
        #region Methods SCRUD

        // GET: PlaylistTrack
        // GET: PlaylistTrack/Index
        [HttpGet]
        public ActionResult Index(int? masterPlaylistId = null, int? masterTrackId = null)
        {
            ClearUrlDictionary();

            PlaylistTrackCollectionModel playlistTrackCollectionModel = new PlaylistTrackCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index",
                MasterPlaylistId = masterPlaylistId, MasterTrackId = masterTrackId
            };

            try
            {
                IsSearch(playlistTrackCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                playlistTrackCollectionModel.OperationResult.ParseException(exception);
            }

            return View(playlistTrackCollectionModel);
        }        

        // GET & POST: PlaylistTrack/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null, int? masterPlaylistId = null, int? masterTrackId = null)
        {
            WriteUrlDictionary();

            PlaylistTrackCollectionModel playlistTrackCollectionModel = new PlaylistTrackCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction,
                MasterPlaylistId = masterPlaylistId, MasterTrackId = masterTrackId
            };

            try
            {
                IsSearch(playlistTrackCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                playlistTrackCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView("_PlaylistTrackCollection", playlistTrackCollectionModel);
        }

        // GET & POST: PlaylistTrack/Lookup
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

            return PartialView("_PlaylistTrackLookup", lookupModel);
        }

        // GET: PlaylistTrack/Create
        [HttpGet]
        public ActionResult Create(int? masterPlaylistId = null, int? masterTrackId = null)
        {
            PlaylistTrackItemModel playlistTrackItemModel = new PlaylistTrackItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                PlaylistTrack = new PlaylistTrackViewModel(),
                ControllerAction = "Create",
                MasterPlaylistId = masterPlaylistId, MasterTrackId = masterTrackId
            };

            try
            {
                IsCreate(playlistTrackItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                playlistTrackItemModel.OperationResult.ParseException(exception);
            }

            return View(playlistTrackItemModel);
        }

        // POST: PlaylistTrack/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlaylistTrackItemModel playlistTrackItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Create(playlistTrackItemModel.OperationResult, (PlaylistTrackDTO)playlistTrackItemModel.PlaylistTrack.ToDTO()))
                    {
                        return RedirectToUrlDictionary();
                    }
                }
            }
            catch (Exception exception)
            {
                playlistTrackItemModel.OperationResult.ParseException(exception);
            }

            playlistTrackItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(playlistTrackItemModel);
        }

        // GET: PlaylistTrack/Read/1
        [HttpGet]
        public ActionResult Read(int? playlistId, int? trackId, int? masterPlaylistId = null, int? masterTrackId = null)
        {
            PlaylistTrackItemModel playlistTrackItemModel = new PlaylistTrackItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                PlaylistTrack = new PlaylistTrackViewModel(),
                ControllerAction = "Read",
                MasterPlaylistId = masterPlaylistId, MasterTrackId = masterTrackId
            };
            
            try
            {
                PlaylistTrackDTO playlistTrackDTO = Application.GetById(playlistTrackItemModel.OperationResult, new object[] { playlistId, trackId });
                if (playlistTrackDTO != null)
                {
                    playlistTrackItemModel.PlaylistTrack = new PlaylistTrackViewModel(playlistTrackDTO);
                }
            }
            catch (Exception exception)
            {
                playlistTrackItemModel.OperationResult.ParseException(exception);
            }

            return View(playlistTrackItemModel);
        }

        // GET: PlaylistTrack/Update/1
        [HttpGet]
        public ActionResult Update(int? playlistId, int? trackId, int? masterPlaylistId = null, int? masterTrackId = null)
        {
            PlaylistTrackItemModel playlistTrackItemModel = new PlaylistTrackItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                PlaylistTrack = new PlaylistTrackViewModel(),
                ControllerAction = "Update",
                MasterPlaylistId = masterPlaylistId, MasterTrackId = masterTrackId
            };
            
            try
            {
                PlaylistTrackDTO playlistTrackDTO = Application.GetById(playlistTrackItemModel.OperationResult, new object[] { playlistId, trackId });
                if (playlistTrackDTO != null)
                {
                    playlistTrackItemModel.PlaylistTrack = new PlaylistTrackViewModel(playlistTrackDTO);
                }
            }
            catch (Exception exception)
            {
                playlistTrackItemModel.OperationResult.ParseException(exception);
            }

            return View(playlistTrackItemModel);
        }

        // POST: PlaylistTrack/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(PlaylistTrackItemModel playlistTrackItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Update(playlistTrackItemModel.OperationResult, (PlaylistTrackDTO)playlistTrackItemModel.PlaylistTrack.ToDTO()))
                    {
                        return RedirectToUrlDictionary();
                    }
                }
            }
            catch (Exception exception)
            {
                playlistTrackItemModel.OperationResult.ParseException(exception);
            }

            playlistTrackItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(playlistTrackItemModel);
        }

        // GET: PlaylistTrack/Delete/1
        [HttpGet]
        public ActionResult Delete(int? playlistId, int? trackId, int? masterPlaylistId = null, int? masterTrackId = null)
        {
            PlaylistTrackItemModel playlistTrackItemModel = new PlaylistTrackItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                PlaylistTrack = new PlaylistTrackViewModel(),
                ControllerAction = "Delete",
                MasterPlaylistId = masterPlaylistId, MasterTrackId = masterTrackId
            };
            
            try
            {
                PlaylistTrackDTO playlistTrackDTO = Application.GetById(playlistTrackItemModel.OperationResult, new object[] { playlistId, trackId });
                if (playlistTrackDTO != null)
                {
                    playlistTrackItemModel.PlaylistTrack = new PlaylistTrackViewModel(playlistTrackDTO);
                }
            }
            catch (Exception exception)
            {
                playlistTrackItemModel.OperationResult.ParseException(exception);
            }

            return View(playlistTrackItemModel);
        }

        // POST: PlaylistTrack/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PlaylistTrackItemModel playlistTrackItemModel)
        {
            try
            {
                if (Application.Delete(playlistTrackItemModel.OperationResult, (PlaylistTrackDTO)playlistTrackItemModel.PlaylistTrack.ToDTO()))
                {
                    return RedirectToUrlDictionary();
                }
            }
            catch (Exception exception)
            {
                playlistTrackItemModel.OperationResult.ParseException(exception);
            }

            playlistTrackItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(playlistTrackItemModel);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: PlaylistTrack/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<PlaylistTrackViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(PlaylistTrack), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<PlaylistTrackViewModel, PlaylistTrackDTO, PlaylistTrack>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: PlaylistTrack/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, PlaylistTrackResources.EntitySingular + ".xlsx");
        }

        // POST: PlaylistTrack/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, PlaylistTrackResources.EntitySingular + ".pdf");
        }

        // POST: PlaylistTrack/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, PlaylistTrackResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}