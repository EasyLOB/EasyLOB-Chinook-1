using Newtonsoft.Json;
using Syncfusion.JavaScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using EasyLOB.AuditTrail.Data;
using EasyLOB.AuditTrail.Data.Resources;
using EasyLOB.AuditTrail.Persistence;
using EasyLOB;
using EasyLOB.Data;
using EasyLOB.Library;
using EasyLOB.Library.Mvc;
using EasyLOB.Library.Syncfusion;

namespace EasyLOB.AuditTrail.Mvc
{
    public class AuditTrailLogController : BaseControllerSCRUDPersistence<AuditTrailLog>
    {
        #region Methods

        public AuditTrailLogController(IAuditTrailUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;

            // !!!
            IsSecurityOperations.IsCreate = false;
            IsSecurityOperations.IsRead = false;
            IsSecurityOperations.IsUpdate = false;
            IsSecurityOperations.IsDelete = false;
        }

        #endregion

        #region Methods SCRUD

        // GET: AuditTrailLog
        // GET: AuditTrailLog/Index
        [HttpGet]
        public ActionResult Index()
        {
            ClearUrlDictionary();

            AuditTrailLogCollectionModel auditTrailLogCollectionModel = new AuditTrailLogCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index"
            };

            try
            {
                IsSearch(auditTrailLogCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                auditTrailLogCollectionModel.OperationResult.ParseException(exception);
            }

            return View(auditTrailLogCollectionModel);
        }

        // GET & POST: AuditTrailLog/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterUrl = null, string masterControllerAction = null)
        {
            WriteUrlDictionary(masterUrl);

            AuditTrailLogCollectionModel auditTrailLogCollectionModel = new AuditTrailLogCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction
            };

            try
            {
                IsSearch(auditTrailLogCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                auditTrailLogCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView("_AuditTrailLogCollection", auditTrailLogCollectionModel);
        }

        // GET & POST: AuditTrailLog/Lookup
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

            return PartialView("_AuditTrailLogLookup", lookupModel);
        }

        // GET: AuditTrailLog/Create
        [HttpGet]
        public ActionResult Create()
        {
            AuditTrailLogItemModel auditTrailLogItemModel = new AuditTrailLogItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                AuditTrailLog = new AuditTrailLogViewModel(),
                ControllerAction = "Create"
            };

            try
            {
                IsCreate(auditTrailLogItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                auditTrailLogItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(auditTrailLogItemModel);
        }

        // POST: AuditTrailLog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuditTrailLogItemModel auditTrailLogItemModel)
        {
            try
            {
                if (IsCreate(auditTrailLogItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Create(auditTrailLogItemModel.OperationResult, (AuditTrailLog)auditTrailLogItemModel.AuditTrailLog.ToData()))
                        {
                            UnitOfWork.Save(auditTrailLogItemModel.OperationResult);
                            {
                                return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailLogItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(auditTrailLogItemModel.OperationResult);
        }

        // GET: AuditTrailLog/Read/1
        [HttpGet]
        public ActionResult Read(int? auditTrailLogId)
        {
            AuditTrailLogItemModel auditTrailLogItemModel = new AuditTrailLogItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                AuditTrailLog = new AuditTrailLogViewModel(),
                ControllerAction = "Read"
            };
            
            try
            {
                if (IsRead(auditTrailLogItemModel.OperationResult))
                {
                    AuditTrailLog auditTrailLog = Repository.GetById(new object[] { auditTrailLogId });
                    if (auditTrailLog != null)
                    {
                        auditTrailLogItemModel.AuditTrailLog = new AuditTrailLogViewModel(auditTrailLog);
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailLogItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(auditTrailLogItemModel);
        }

        // GET: AuditTrailLog/Update/1
        [HttpGet]
        public ActionResult Update(int? auditTrailLogId)
        {
            AuditTrailLogItemModel auditTrailLogItemModel = new AuditTrailLogItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                AuditTrailLog = new AuditTrailLogViewModel(),
                ControllerAction = "Update"
            };
            
            try
            {
                if (IsUpdate(auditTrailLogItemModel.OperationResult))
                {
                    AuditTrailLog auditTrailLog = Repository.GetById(new object[] { auditTrailLogId });
                    if (auditTrailLog != null)
                    {
                        auditTrailLogItemModel.AuditTrailLog = new AuditTrailLogViewModel(auditTrailLog);
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailLogItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(auditTrailLogItemModel);
        }

        // POST: AuditTrailLog/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(AuditTrailLogItemModel auditTrailLogItemModel)
        {
            try
            {
                if (IsUpdate(auditTrailLogItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Update(auditTrailLogItemModel.OperationResult, (AuditTrailLog)auditTrailLogItemModel.AuditTrailLog.ToData()))
                        {
                            if (UnitOfWork.Save(auditTrailLogItemModel.OperationResult))
                            {
                                return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailLogItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(auditTrailLogItemModel.OperationResult);
        }

        // GET: AuditTrailLog/Delete/1
        [HttpGet]
        public ActionResult Delete(int? auditTrailLogId)
        {
            AuditTrailLogItemModel auditTrailLogItemModel = new AuditTrailLogItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                AuditTrailLog = new AuditTrailLogViewModel(),
                ControllerAction = "Delete"
            };
            
            try
            {
                if (IsDelete(auditTrailLogItemModel.OperationResult))
                {
                    AuditTrailLog auditTrailLog = Repository.GetById(new object[] { auditTrailLogId });
                    if (auditTrailLog != null)
                    {
                        auditTrailLogItemModel.AuditTrailLog = new AuditTrailLogViewModel(auditTrailLog);
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailLogItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(auditTrailLogItemModel);
        }

        // POST: AuditTrailLog/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(AuditTrailLogItemModel auditTrailLogItemModel)
        {
            try
            {
                if (IsDelete(auditTrailLogItemModel.OperationResult))
                {
                    if (Repository.Delete(auditTrailLogItemModel.OperationResult, (AuditTrailLog)auditTrailLogItemModel.AuditTrailLog.ToData()))
                    {
                        if (UnitOfWork.Save(auditTrailLogItemModel.OperationResult))
                        {
                            return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailLogItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(auditTrailLogItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: AuditTrailLog/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<AuditTrailLogViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(AuditTrailLog), UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<AuditTrailLogViewModel, AuditTrailLogDTO, AuditTrailLog>.ToViewList(Repository.Select(where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: AuditTrailLog/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, AuditTrailLogResources.EntitySingular + ".xlsx");
        }

        // POST: AuditTrailLog/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, AuditTrailLogResources.EntitySingular + ".pdf");
        }

        // POST: AuditTrailLog/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, AuditTrailLogResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}