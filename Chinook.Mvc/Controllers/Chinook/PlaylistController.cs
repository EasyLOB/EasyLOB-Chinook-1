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
    public class PlaylistController : BaseControllerSCRUDApplication<PlaylistDTO, Playlist>
    {
        #region Methods

        public PlaylistController(IChinookGenericApplicationDTO<PlaylistDTO, Playlist> application)
        {
            Application = application;
        }
        
        #endregion
        
        #region Methods SCRUD

        // GET: Playlist
        // GET: Playlist/Index
        [HttpGet]
        public ActionResult Index()
        {
            ClearUrlDictionary();

            PlaylistCollectionModel playlistCollectionModel = new PlaylistCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index"
            };

            try
            {
                IsSearch(playlistCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                playlistCollectionModel.OperationResult.ParseException(exception);
            }

            return View(playlistCollectionModel);
        }        

        // GET & POST: Playlist/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null)
        {
            WriteUrlDictionary();

            PlaylistCollectionModel playlistCollectionModel = new PlaylistCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction
            };

            try
            {
                IsSearch(playlistCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                playlistCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView("_PlaylistCollection", playlistCollectionModel);
        }

        // GET & POST: Playlist/Lookup
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

            return PartialView("_PlaylistLookup", lookupModel);
        }

        // GET: Playlist/Create
        [HttpGet]
        public ActionResult Create()
        {
            PlaylistItemModel playlistItemModel = new PlaylistItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Playlist = new PlaylistViewModel(),
                ControllerAction = "Create"
            };

            try
            {
                IsCreate(playlistItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                playlistItemModel.OperationResult.ParseException(exception);
            }

            return View(playlistItemModel);
        }

        // POST: Playlist/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlaylistItemModel playlistItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Create(playlistItemModel.OperationResult, (PlaylistDTO)playlistItemModel.Playlist.ToDTO()))
                    {
                        return RedirectToUrlDictionary();
                    }
                }
            }
            catch (Exception exception)
            {
                playlistItemModel.OperationResult.ParseException(exception);
            }

            playlistItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(playlistItemModel);
        }

        // GET: Playlist/Read/1
        [HttpGet]
        public ActionResult Read(int? playlistId)
        {
            PlaylistItemModel playlistItemModel = new PlaylistItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Playlist = new PlaylistViewModel(),
                ControllerAction = "Read"
            };
            
            try
            {
                PlaylistDTO playlistDTO = Application.GetById(playlistItemModel.OperationResult, new object[] { playlistId });
                if (playlistDTO != null)
                {
                    playlistItemModel.Playlist = new PlaylistViewModel(playlistDTO);
                }
            }
            catch (Exception exception)
            {
                playlistItemModel.OperationResult.ParseException(exception);
            }

            return View(playlistItemModel);
        }

        // GET: Playlist/Update/1
        [HttpGet]
        public ActionResult Update(int? playlistId)
        {
            PlaylistItemModel playlistItemModel = new PlaylistItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Playlist = new PlaylistViewModel(),
                ControllerAction = "Update"
            };
            
            try
            {
                PlaylistDTO playlistDTO = Application.GetById(playlistItemModel.OperationResult, new object[] { playlistId });
                if (playlistDTO != null)
                {
                    playlistItemModel.Playlist = new PlaylistViewModel(playlistDTO);
                }
            }
            catch (Exception exception)
            {
                playlistItemModel.OperationResult.ParseException(exception);
            }

            return View(playlistItemModel);
        }

        // POST: Playlist/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(PlaylistItemModel playlistItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Update(playlistItemModel.OperationResult, (PlaylistDTO)playlistItemModel.Playlist.ToDTO()))
                    {
                        return RedirectToUrlDictionary();
                    }
                }
            }
            catch (Exception exception)
            {
                playlistItemModel.OperationResult.ParseException(exception);
            }

            playlistItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(playlistItemModel);
        }

        // GET: Playlist/Delete/1
        [HttpGet]
        public ActionResult Delete(int? playlistId)
        {
            PlaylistItemModel playlistItemModel = new PlaylistItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Playlist = new PlaylistViewModel(),
                ControllerAction = "Delete"
            };
            
            try
            {
                PlaylistDTO playlistDTO = Application.GetById(playlistItemModel.OperationResult, new object[] { playlistId });
                if (playlistDTO != null)
                {
                    playlistItemModel.Playlist = new PlaylistViewModel(playlistDTO);
                }
            }
            catch (Exception exception)
            {
                playlistItemModel.OperationResult.ParseException(exception);
            }

            return View(playlistItemModel);
        }

        // POST: Playlist/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PlaylistItemModel playlistItemModel)
        {
            try
            {
                if (Application.Delete(playlistItemModel.OperationResult, (PlaylistDTO)playlistItemModel.Playlist.ToDTO()))
                {
                    return RedirectToUrlDictionary();
                }
            }
            catch (Exception exception)
            {
                playlistItemModel.OperationResult.ParseException(exception);
            }

            playlistItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(playlistItemModel);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Playlist/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<PlaylistViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Playlist), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<PlaylistViewModel, PlaylistDTO, Playlist>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: Playlist/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, PlaylistResources.EntitySingular + ".xlsx");
        }

        // POST: Playlist/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, PlaylistResources.EntitySingular + ".pdf");
        }

        // POST: Playlist/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, PlaylistResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}