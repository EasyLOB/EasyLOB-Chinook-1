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
    public class MediaTypeController : BaseControllerSCRUDPersistence<MediaType>
    {
        #region Methods

        public MediaTypeController(IChinookUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        
        #endregion
        
        #region Methods SCRUD

        // GET: MediaType
        // GET: MediaType/Index
        [HttpGet]
        public ActionResult Index()
        {
            ClearUrlDictionary();

            MediaTypeCollectionModel mediaTypeCollectionModel = new MediaTypeCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index"
            };

            try
            {
                IsSearch(mediaTypeCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                mediaTypeCollectionModel.OperationResult.ParseException(exception);
            }

            return View(mediaTypeCollectionModel);
        }

        // GET & POST: MediaType/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterUrl = null, string masterControllerAction = null)
        {
            WriteUrlDictionary(masterUrl);

            MediaTypeCollectionModel mediaTypeCollectionModel = new MediaTypeCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction
            };

            try
            {
                IsSearch(mediaTypeCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                mediaTypeCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView("_MediaTypeCollection", mediaTypeCollectionModel);
        }

        // GET & POST: MediaType/Lookup
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

            return PartialView("_MediaTypeLookup", lookupModel);
        }

        // GET: MediaType/Create
        [HttpGet]
        public ActionResult Create()
        {
            MediaTypeItemModel mediaTypeItemModel = new MediaTypeItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                MediaType = new MediaTypeViewModel(),
                ControllerAction = "Create"
            };

            try
            {
                IsCreate(mediaTypeItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                mediaTypeItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(mediaTypeItemModel);
        }

        // POST: MediaType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MediaTypeItemModel mediaTypeItemModel)
        {
            try
            {
                if (IsCreate(mediaTypeItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Create(mediaTypeItemModel.OperationResult, (MediaType)mediaTypeItemModel.MediaType.ToData()))
                        {
                            UnitOfWork.Save(mediaTypeItemModel.OperationResult);
                            {
                                return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                mediaTypeItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(mediaTypeItemModel.OperationResult);
        }

        // GET: MediaType/Read/1
        [HttpGet]
        public ActionResult Read(int? mediaTypeId)
        {
            MediaTypeItemModel mediaTypeItemModel = new MediaTypeItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                MediaType = new MediaTypeViewModel(),
                ControllerAction = "Read"
            };
            
            try
            {
                if (IsRead(mediaTypeItemModel.OperationResult))
                {
                    MediaType mediaType = Repository.GetById(new object[] { mediaTypeId });
                    if (mediaType != null)
                    {
                        mediaTypeItemModel.MediaType = new MediaTypeViewModel(mediaType);
                    }
                }
            }
            catch (Exception exception)
            {
                mediaTypeItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(mediaTypeItemModel);
        }

        // GET: MediaType/Update/1
        [HttpGet]
        public ActionResult Update(int? mediaTypeId)
        {
            MediaTypeItemModel mediaTypeItemModel = new MediaTypeItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                MediaType = new MediaTypeViewModel(),
                ControllerAction = "Update"
            };
            
            try
            {
                if (IsUpdate(mediaTypeItemModel.OperationResult))
                {
                    MediaType mediaType = Repository.GetById(new object[] { mediaTypeId });
                    if (mediaType != null)
                    {
                        mediaTypeItemModel.MediaType = new MediaTypeViewModel(mediaType);
                    }
                }
            }
            catch (Exception exception)
            {
                mediaTypeItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(mediaTypeItemModel);
        }

        // POST: MediaType/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(MediaTypeItemModel mediaTypeItemModel)
        {
            try
            {
                if (IsUpdate(mediaTypeItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Update(mediaTypeItemModel.OperationResult, (MediaType)mediaTypeItemModel.MediaType.ToData()))
                        {
                            if (UnitOfWork.Save(mediaTypeItemModel.OperationResult))
                            {
                                return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                mediaTypeItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(mediaTypeItemModel.OperationResult);
        }

        // GET: MediaType/Delete/1
        [HttpGet]
        public ActionResult Delete(int? mediaTypeId)
        {
            MediaTypeItemModel mediaTypeItemModel = new MediaTypeItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                MediaType = new MediaTypeViewModel(),
                ControllerAction = "Delete"
            };
            
            try
            {
                if (IsDelete(mediaTypeItemModel.OperationResult))
                {
                    MediaType mediaType = Repository.GetById(new object[] { mediaTypeId });
                    if (mediaType != null)
                    {
                        mediaTypeItemModel.MediaType = new MediaTypeViewModel(mediaType);
                    }
                }
            }
            catch (Exception exception)
            {
                mediaTypeItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(mediaTypeItemModel);
        }

        // POST: MediaType/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(MediaTypeItemModel mediaTypeItemModel)
        {
            try
            {
                if (IsDelete(mediaTypeItemModel.OperationResult))
                {
                    if (Repository.Delete(mediaTypeItemModel.OperationResult, (MediaType)mediaTypeItemModel.MediaType.ToData()))
                    {
                        if (UnitOfWork.Save(mediaTypeItemModel.OperationResult))
                        {
                            return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                mediaTypeItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(mediaTypeItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: MediaType/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<MediaTypeViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();
            
            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(MediaType), UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<MediaTypeViewModel, MediaTypeDTO, MediaType>.ToViewList(Repository.Select(where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: MediaType/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, MediaTypeResources.EntitySingular + ".xlsx");
        }

        // POST: MediaType/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, MediaTypeResources.EntitySingular + ".pdf");
        }

        // POST: MediaType/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, MediaTypeResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}