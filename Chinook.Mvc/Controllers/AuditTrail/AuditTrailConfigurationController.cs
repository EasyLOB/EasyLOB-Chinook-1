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
    public class AuditTrailConfigurationController : BaseControllerSCRUDPersistence<AuditTrailConfiguration>
    {
        #region Methods

        public AuditTrailConfigurationController(IAuditTrailUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        
        #endregion
        
        #region Methods SCRUD

        // GET: AuditTrailConfiguration
        // GET: AuditTrailConfiguration/Index
        [HttpGet]
        public ActionResult Index()
        {
            ClearUrlDictionary();

            AuditTrailConfigurationCollectionModel auditTrailConfigurationCollectionModel = new AuditTrailConfigurationCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index"
            };

            try
            {
                IsSearch(auditTrailConfigurationCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                auditTrailConfigurationCollectionModel.OperationResult.ParseException(exception);
            }

            return View(auditTrailConfigurationCollectionModel);
        }

        // GET & POST: AuditTrailConfiguration/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null)
        {
            WriteUrlDictionary();

            AuditTrailConfigurationCollectionModel auditTrailConfigurationCollectionModel = new AuditTrailConfigurationCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction
            };

            try
            {
                IsSearch(auditTrailConfigurationCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                auditTrailConfigurationCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView("_AuditTrailConfigurationCollection", auditTrailConfigurationCollectionModel);
        }

        // GET & POST: AuditTrailConfiguration/Lookup
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

            return PartialView("_AuditTrailConfigurationLookup", lookupModel);
        }

        // GET: AuditTrailConfiguration/Create
        [HttpGet]
        public ActionResult Create()
        {
            AuditTrailConfigurationItemModel auditTrailConfigurationItemModel = new AuditTrailConfigurationItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                AuditTrailConfiguration = new AuditTrailConfigurationViewModel(),
                ControllerAction = "Create"
            };

            try
            {
                IsCreate(auditTrailConfigurationItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                auditTrailConfigurationItemModel.OperationResult.ParseException(exception);
            }

            return View(auditTrailConfigurationItemModel);
        }

        // POST: AuditTrailConfiguration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuditTrailConfigurationItemModel auditTrailConfigurationItemModel)
        {
            try            
            {
                if (IsCreate(auditTrailConfigurationItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Create(auditTrailConfigurationItemModel.OperationResult, (AuditTrailConfiguration)auditTrailConfigurationItemModel.AuditTrailConfiguration.ToData()))
                        {
                            if (UnitOfWork.Save(auditTrailConfigurationItemModel.OperationResult))
                            {
                                return RedirectToUrlDictionary();
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailConfigurationItemModel.OperationResult.ParseException(exception);
            }

            auditTrailConfigurationItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(auditTrailConfigurationItemModel);
        }

        // GET: AuditTrailConfiguration/Read/1
        [HttpGet]
        public ActionResult Read(int? auditTrailConfigurationId)
        {
            AuditTrailConfigurationItemModel auditTrailConfigurationItemModel = new AuditTrailConfigurationItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                AuditTrailConfiguration = new AuditTrailConfigurationViewModel(),
                ControllerAction = "Read"
            };
            
            try
            {
                if (IsRead(auditTrailConfigurationItemModel.OperationResult))
                {
                    AuditTrailConfiguration auditTrailConfiguration = Repository.GetById(new object[] { auditTrailConfigurationId });
                    if (auditTrailConfiguration != null)
                    {
                        auditTrailConfigurationItemModel.AuditTrailConfiguration = new AuditTrailConfigurationViewModel(auditTrailConfiguration);
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailConfigurationItemModel.OperationResult.ParseException(exception);
            }            

            return View(auditTrailConfigurationItemModel);
        }

        // GET: AuditTrailConfiguration/Update/1
        [HttpGet]
        public ActionResult Update(int? auditTrailConfigurationId)
        {
            AuditTrailConfigurationItemModel auditTrailConfigurationItemModel = new AuditTrailConfigurationItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                AuditTrailConfiguration = new AuditTrailConfigurationViewModel(),
                ControllerAction = "Update"
            };
            
            try
            {
                if (IsUpdate(auditTrailConfigurationItemModel.OperationResult))
                {
                    AuditTrailConfiguration auditTrailConfiguration = Repository.GetById(new object[] { auditTrailConfigurationId });
                    if (auditTrailConfiguration != null)
                    {
                        auditTrailConfigurationItemModel.AuditTrailConfiguration = new AuditTrailConfigurationViewModel(auditTrailConfiguration);
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailConfigurationItemModel.OperationResult.ParseException(exception);
            }            

            return View(auditTrailConfigurationItemModel);
        }

        // POST: AuditTrailConfiguration/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(AuditTrailConfigurationItemModel auditTrailConfigurationItemModel)
        {
            try
            {
                if (IsUpdate(auditTrailConfigurationItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Update(auditTrailConfigurationItemModel.OperationResult, (AuditTrailConfiguration)auditTrailConfigurationItemModel.AuditTrailConfiguration.ToData()))
                        {
                            if (UnitOfWork.Save(auditTrailConfigurationItemModel.OperationResult))
                            {
                                return RedirectToUrlDictionary();
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailConfigurationItemModel.OperationResult.ParseException(exception);
            }

            auditTrailConfigurationItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(auditTrailConfigurationItemModel);
        }

        // GET: AuditTrailConfiguration/Delete/1
        [HttpGet]
        public ActionResult Delete(int? auditTrailConfigurationId)
        {
            AuditTrailConfigurationItemModel auditTrailConfigurationItemModel = new AuditTrailConfigurationItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                AuditTrailConfiguration = new AuditTrailConfigurationViewModel(),
                ControllerAction = "Delete"
            };
            
            try
            {
                if (IsDelete(auditTrailConfigurationItemModel.OperationResult))
                {
                    AuditTrailConfiguration auditTrailConfiguration = Repository.GetById(new object[] { auditTrailConfigurationId });
                    if (auditTrailConfiguration != null)
                    {
                        auditTrailConfigurationItemModel.AuditTrailConfiguration = new AuditTrailConfigurationViewModel(auditTrailConfiguration);
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailConfigurationItemModel.OperationResult.ParseException(exception);
            }            

            return View(auditTrailConfigurationItemModel);
        }

        // POST: AuditTrailConfiguration/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(AuditTrailConfigurationItemModel auditTrailConfigurationItemModel)
        {
            try
            {
                if (IsDelete(auditTrailConfigurationItemModel.OperationResult))
                {
                    if (Repository.Delete(auditTrailConfigurationItemModel.OperationResult, (AuditTrailConfiguration)auditTrailConfigurationItemModel.AuditTrailConfiguration.ToData()))
                    {
                        if (UnitOfWork.Save(auditTrailConfigurationItemModel.OperationResult))
                        {
                            return RedirectToUrlDictionary();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                auditTrailConfigurationItemModel.OperationResult.ParseException(exception);
            }

            auditTrailConfigurationItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(auditTrailConfigurationItemModel);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: AuditTrailConfiguration/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<AuditTrailConfigurationViewModel>(); 
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(AuditTrailConfiguration), UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<AuditTrailConfigurationViewModel, AuditTrailConfigurationDTO, AuditTrailConfiguration>.ToViewList(Repository.Select(where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: AuditTrailConfiguration/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, AuditTrailConfigurationResources.EntitySingular + ".xlsx");
        }

        // POST: AuditTrailConfiguration/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, AuditTrailConfigurationResources.EntitySingular + ".pdf");
        }

        // POST: AuditTrailConfiguration/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, AuditTrailConfigurationResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}