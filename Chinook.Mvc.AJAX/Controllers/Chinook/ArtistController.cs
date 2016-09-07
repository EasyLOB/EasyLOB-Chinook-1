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
    public class ArtistController : BaseControllerSCRUDApplication<ArtistDTO, Artist>
    {
        #region Methods

        public ArtistController(IChinookGenericApplicationDTO<ArtistDTO, Artist> application)
        {
            Application = application;            
        }
        
        #endregion
        
        #region Methods SCRUD

        // GET: Artist
        // GET: Artist/Index
        [HttpGet]
        public ActionResult Index()
        {
            ClearUrlDictionary();

            ArtistCollectionModel artistCollectionModel = new ArtistCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index"
            };

            try
            {
                IsSearch(artistCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                artistCollectionModel.OperationResult.ParseException(exception);
            }

            return View(artistCollectionModel);
        }        

        // GET & POST: Artist/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterUrl = null, string masterControllerAction = null)
        {
            WriteUrlDictionary(masterUrl);

            ArtistCollectionModel artistCollectionModel = new ArtistCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction
            };

            try
            {
                IsSearch(artistCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                artistCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView("_ArtistCollection", artistCollectionModel);
        }

        // GET & POST: Artist/Lookup
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

            return PartialView("_ArtistLookup", lookupModel);
        }

        // GET: Artist/Create
        [HttpGet]
        public ActionResult Create()
        {
            ArtistItemModel artistItemModel = new ArtistItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Artist = new ArtistViewModel(),
                ControllerAction = "Create"
            };

            try
            {
                IsCreate(artistItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                artistItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(artistItemModel);
        }

        // POST: Artist/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArtistItemModel artistItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Create(artistItemModel.OperationResult, (ArtistDTO)artistItemModel.Artist.ToDTO()))
                    {
                        return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                    }
                }
            }
            catch (Exception exception)
            {
                artistItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(artistItemModel.OperationResult);
        }

        // GET: Artist/Read/1
        [HttpGet]
        public ActionResult Read(int? artistId)
        {
            ArtistItemModel artistItemModel = new ArtistItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Artist = new ArtistViewModel(),
                ControllerAction = "Read"
            };
            
            try
            {
                ArtistDTO artistDTO = Application.GetById(artistItemModel.OperationResult, new object[] { artistId });
                if (artistDTO != null)
                {
                    artistItemModel.Artist = new ArtistViewModel(artistDTO);
                }
            }
            catch (Exception exception)
            {
                artistItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(artistItemModel);
        }

        // GET: Artist/Update/1
        [HttpGet]
        public ActionResult Update(int? artistId)
        {
            ArtistItemModel artistItemModel = new ArtistItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Artist = new ArtistViewModel(),
                ControllerAction = "Update"
            };
            
            try
            {
                ArtistDTO artistDTO = Application.GetById(artistItemModel.OperationResult, new object[] { artistId });
                if (artistDTO != null)
                {
                    artistItemModel.Artist = new ArtistViewModel(artistDTO);
                }
            }
            catch (Exception exception)
            {
                artistItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(artistItemModel);
        }

        // POST: Artist/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ArtistItemModel artistItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Update(artistItemModel.OperationResult, (ArtistDTO)artistItemModel.Artist.ToDTO()))
                    {
                        return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                    }
                }
            }
            catch (Exception exception)
            {
                artistItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(artistItemModel.OperationResult);
        }

        // GET: Artist/Delete/1
        [HttpGet]
        public ActionResult Delete(int? artistId)
        {
            ArtistItemModel artistItemModel = new ArtistItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Artist = new ArtistViewModel(),
                ControllerAction = "Delete"
            };
            
            try
            {
                ArtistDTO artistDTO = Application.GetById(artistItemModel.OperationResult, new object[] { artistId });
                if (artistDTO != null)
                {
                    artistItemModel.Artist = new ArtistViewModel(artistDTO);
                }
            }
            catch (Exception exception)
            {
                artistItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(artistItemModel);
        }

        // POST: Artist/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ArtistItemModel artistItemModel)
        {
            try
            {
                if (Application.Delete(artistItemModel.OperationResult, (ArtistDTO)artistItemModel.Artist.ToDTO()))
                {
                    return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                }
            }
            catch (Exception exception)
            {
                artistItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(artistItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Artist/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<ArtistViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Artist), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<ArtistViewModel, ArtistDTO, Artist>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: Artist/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, ArtistResources.EntitySingular + ".xlsx");
        }

        // POST: Artist/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, ArtistResources.EntitySingular + ".pdf");
        }

        // POST: Artist/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, ArtistResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}