using Newtonsoft.Json;
using Syncfusion.JavaScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using EasyLOB.Security.Data;
using EasyLOB.Security.Data.Resources;
using EasyLOB.Security.Persistence;
using EasyLOB;
using EasyLOB.Data;
using EasyLOB.Library;
using EasyLOB.Library.Mvc;
using EasyLOB.Library.Syncfusion;

namespace EasyLOB.Security.Mvc
{
    public class ActivityController : BaseControllerSCRUDPersistence<EasyLOB.Security.Data.Activity> // !!!
    {
        #region Methods

        public ActivityController(ISecurityUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        #endregion Methods

        #region Methods SCRUD

        // GET: Activity
        // GET: Activity/Index
        [HttpGet]
        public ActionResult Index()
        {
            ClearUrlDictionary();

            ActivityCollectionModel activityCollectionModel = new ActivityCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index"
            };

            try
            {
                IsSearch(activityCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                activityCollectionModel.OperationResult.ParseException(exception);
            }

            return View(activityCollectionModel);
        }

        // GET & POST: Activity/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterUrl = null, string masterControllerAction = null)
        {
            WriteUrlDictionary(masterUrl);

            ActivityCollectionModel activityCollectionModel = new ActivityCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction
            };

            try
            {
                IsSearch(activityCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                activityCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView(activityCollectionModel);
        }

        // GET & POST: Activity/Lookup
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

            return PartialView("_ActivityLookup", lookupModel);
        }

        // GET: Activity/Create
        [HttpGet]
        public ActionResult Create()
        {
            ActivityItemModel activityItemModel = new ActivityItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Activity = new ActivityViewModel(),
                ControllerAction = "Create"
            };

            try
            {
                IsCreate(activityItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                activityItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(activityItemModel);
        }

        // POST: Activity/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActivityItemModel activityItemModel)
        {
            try
            {
                if (IsCreate(activityItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Create(activityItemModel.OperationResult, (EasyLOB.Security.Data.Activity)activityItemModel.Activity.ToData())) // !!!
                        {
                            UnitOfWork.Save(activityItemModel.OperationResult);
                            {
                                return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                activityItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(activityItemModel.OperationResult);
        }

        // GET: Activity/Read/1
        [HttpGet]
        public ActionResult Read(string id)
        {
            ActivityItemModel activityItemModel = new ActivityItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Activity = new ActivityViewModel(),
                ControllerAction = "Read"
            };

            try
            {
                if (IsRead(activityItemModel.OperationResult))
                {
                    EasyLOB.Security.Data.Activity activity = Repository.GetById(new object[] { id }); // !!!
                    if (activity != null)
                    {
                        activityItemModel.Activity = new ActivityViewModel(activity);
                    }
                }
            }
            catch (Exception exception)
            {
                activityItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(activityItemModel);
        }

        // GET: Activity/Update/1
        [HttpGet]
        public ActionResult Update(string id)
        {
            ActivityItemModel activityItemModel = new ActivityItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Activity = new ActivityViewModel(),
                ControllerAction = "Update"
            };

            try
            {
                if (IsUpdate(activityItemModel.OperationResult))
                {
                    EasyLOB.Security.Data.Activity activity = Repository.GetById(new object[] { id }); // !!!
                    if (activity != null)
                    {
                        activityItemModel.Activity = new ActivityViewModel(activity);
                    }
                }
            }
            catch (Exception exception)
            {
                activityItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(activityItemModel);
        }

        // POST: Activity/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ActivityItemModel activityItemModel)
        {
            try
            {
                if (IsUpdate(activityItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Update(activityItemModel.OperationResult, (EasyLOB.Security.Data.Activity)activityItemModel.Activity.ToData())) // !!!
                        {
                            if (UnitOfWork.Save(activityItemModel.OperationResult))
                            {
                                return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                activityItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(activityItemModel.OperationResult);
        }

        // GET: Activity/Delete/1
        [HttpGet]
        public ActionResult Delete(string id)
        {
            ActivityItemModel activityItemModel = new ActivityItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Activity = new ActivityViewModel(),
                ControllerAction = "Delete"
            };

            try
            {
                if (IsDelete(activityItemModel.OperationResult))
                {
                    EasyLOB.Security.Data.Activity activity = Repository.GetById(new object[] { id }); // !!!
                    if (activity != null)
                    {
                        activityItemModel.Activity = new ActivityViewModel(activity);
                    }
                }
            }
            catch (Exception exception)
            {
                activityItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(activityItemModel);
        }

        // POST: Activity/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ActivityItemModel activityItemModel)
        {
            try
            {
                if (IsDelete(activityItemModel.OperationResult))
                {
                    if (Repository.Delete(activityItemModel.OperationResult, (EasyLOB.Security.Data.Activity)activityItemModel.Activity.ToData())) // !!!
                    {
                        if (UnitOfWork.Save(activityItemModel.OperationResult))
                        {
                            return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                activityItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(activityItemModel.OperationResult);
        }

        #endregion Methods SCRUD

        #region Methods Syncfusion

        // POST: Activity/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<ActivityViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(EasyLOB.Security.Data.Activity), UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<ActivityViewModel, ActivityDTO, EasyLOB.Security.Data.Activity>.ToViewList(Repository.Select(where, args.ToArray(), orderBy, dataManager.Skip, take)); // !!!

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

        // POST: Activity/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, ActivityResources.EntitySingular + ".xlsx");
        }

        // POST: Activity/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, ActivityResources.EntitySingular + ".pdf");
        }

        // POST: Activity/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, ActivityResources.EntitySingular + ".docx");
        }

        #endregion Methods Syncfusion
    }
}