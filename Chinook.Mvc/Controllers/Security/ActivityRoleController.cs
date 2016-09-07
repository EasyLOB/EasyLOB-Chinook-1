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
    public class ActivityRoleController : BaseControllerSCRUDPersistence<ActivityRole>
    {
        #region Methods

        public ActivityRoleController(ISecurityUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        
        #endregion
        
        #region Methods SCRUD

        // GET: ActivityRole
        // GET: ActivityRole/Index
        [HttpGet]
        public ActionResult Index(string masterActivityId = null, string masterRoleId = null)
        {
            ClearUrlDictionary();

            ActivityRoleCollectionModel activityRoleCollectionModel = new ActivityRoleCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index",
                MasterActivityId = masterActivityId, MasterRoleId = masterRoleId
            };

            try
            {
                IsSearch(activityRoleCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                activityRoleCollectionModel.OperationResult.ParseException(exception);
            }

            return View(activityRoleCollectionModel);
        }

        // GET & POST: ActivityRole/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null, string masterActivityId = null, string masterRoleId = null)
        {
            WriteUrlDictionary();

            ActivityRoleCollectionModel activityRoleCollectionModel = new ActivityRoleCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction,
                MasterActivityId = masterActivityId, MasterRoleId = masterRoleId
            };

            try
            {
                IsSearch(activityRoleCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                activityRoleCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView("_ActivityRoleCollection", activityRoleCollectionModel);
        }

        // GET & POST: ActivityRole/Lookup
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

            return PartialView("_ActivityRoleLookup", lookupModel);
        }

        // GET: ActivityRole/Create
        [HttpGet]
        public ActionResult Create(string masterActivityId = null, string masterRoleId = null)
        {
            ActivityRoleItemModel activityRoleItemModel = new ActivityRoleItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ActivityRole = new ActivityRoleViewModel(),
                ControllerAction = "Create",
                MasterActivityId = masterActivityId, MasterRoleId = masterRoleId
            };

            try
            {
                IsCreate(activityRoleItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                activityRoleItemModel.OperationResult.ParseException(exception);
            }

            return View(activityRoleItemModel);
        }

        // POST: ActivityRole/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActivityRoleItemModel activityRoleItemModel)
        {
            try            
            {
                if (IsCreate(activityRoleItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Create(activityRoleItemModel.OperationResult, (ActivityRole)activityRoleItemModel.ActivityRole.ToData()))
                        {
                            if (UnitOfWork.Save(activityRoleItemModel.OperationResult))
                            {
                                return RedirectToUrlDictionary();
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                activityRoleItemModel.OperationResult.ParseException(exception);
            }

            activityRoleItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(activityRoleItemModel);
        }

        // GET: ActivityRole/Read/1
        [HttpGet]
        public ActionResult Read(string activityId, string roleId, string masterActivityId = null, string masterRoleId = null)
        {
            ActivityRoleItemModel activityRoleItemModel = new ActivityRoleItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ActivityRole = new ActivityRoleViewModel(),
                ControllerAction = "Read",
                MasterActivityId = masterActivityId, MasterRoleId = masterRoleId
            };
            
            try
            {
                if (IsRead(activityRoleItemModel.OperationResult))
                {
                    ActivityRole activityRole = Repository.GetById(new object[] { activityId, roleId });
                    if (activityRole != null)
                    {
                        activityRoleItemModel.ActivityRole = new ActivityRoleViewModel(activityRole);
                    }
                }
            }
            catch (Exception exception)
            {
                activityRoleItemModel.OperationResult.ParseException(exception);
            }            

            return View(activityRoleItemModel);
        }

        // GET: ActivityRole/Update/1
        [HttpGet]
        public ActionResult Update(string activityId, string roleId, string masterActivityId = null, string masterRoleId = null)
        {
            ActivityRoleItemModel activityRoleItemModel = new ActivityRoleItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ActivityRole = new ActivityRoleViewModel(),
                ControllerAction = "Update",
                MasterActivityId = masterActivityId, MasterRoleId = masterRoleId
            };
            
            try
            {
                if (IsUpdate(activityRoleItemModel.OperationResult))
                {
                    ActivityRole activityRole = Repository.GetById(new object[] { activityId, roleId });
                    if (activityRole != null)
                    {
                        activityRoleItemModel.ActivityRole = new ActivityRoleViewModel(activityRole);
                    }
                }
            }
            catch (Exception exception)
            {
                activityRoleItemModel.OperationResult.ParseException(exception);
            }            

            return View(activityRoleItemModel);
        }

        // POST: ActivityRole/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ActivityRoleItemModel activityRoleItemModel)
        {
            try
            {
                if (IsUpdate(activityRoleItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Update(activityRoleItemModel.OperationResult, (ActivityRole)activityRoleItemModel.ActivityRole.ToData()))
                        {
                            if (UnitOfWork.Save(activityRoleItemModel.OperationResult))
                            {
                                return RedirectToUrlDictionary();
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                activityRoleItemModel.OperationResult.ParseException(exception);
            }

            activityRoleItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(activityRoleItemModel);
        }

        // GET: ActivityRole/Delete/1
        [HttpGet]
        public ActionResult Delete(string activityId, string roleId, string masterActivityId = null, string masterRoleId = null)
        {
            ActivityRoleItemModel activityRoleItemModel = new ActivityRoleItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ActivityRole = new ActivityRoleViewModel(),
                ControllerAction = "Delete",
                MasterActivityId = masterActivityId, MasterRoleId = masterRoleId
            };
            
            try
            {
                if (IsDelete(activityRoleItemModel.OperationResult))
                {
                    ActivityRole activityRole = Repository.GetById(new object[] { activityId, roleId });
                    if (activityRole != null)
                    {
                        activityRoleItemModel.ActivityRole = new ActivityRoleViewModel(activityRole);
                    }
                }
            }
            catch (Exception exception)
            {
                activityRoleItemModel.OperationResult.ParseException(exception);
            }            

            return View(activityRoleItemModel);
        }

        // POST: ActivityRole/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ActivityRoleItemModel activityRoleItemModel)
        {
            try
            {
                if (IsDelete(activityRoleItemModel.OperationResult))
                {
                    if (Repository.Delete(activityRoleItemModel.OperationResult, (ActivityRole)activityRoleItemModel.ActivityRole.ToData()))
                    {
                        if (UnitOfWork.Save(activityRoleItemModel.OperationResult))
                        {
                            return RedirectToUrlDictionary();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                activityRoleItemModel.OperationResult.ParseException(exception);
            }

            activityRoleItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(activityRoleItemModel);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: ActivityRole/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<ActivityRoleViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(ActivityRole), UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<ActivityRoleViewModel, ActivityRoleDTO, ActivityRole>.ToViewList(Repository.Select(where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: ActivityRole/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, ActivityRoleResources.EntitySingular + ".xlsx");
        }

        // POST: ActivityRole/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, ActivityRoleResources.EntitySingular + ".pdf");
        }

        // POST: ActivityRole/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, ActivityRoleResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}