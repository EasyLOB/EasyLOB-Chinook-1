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
    public class RoleController : BaseControllerSCRUDPersistence<Role>
    {
        #region Methods

        public RoleController(ISecurityUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        
        #endregion
        
        #region Methods SCRUD

        // GET: Role
        // GET: Role/Index
        [HttpGet]
        public ActionResult Index()
        {
            ClearUrlDictionary();

            RoleCollectionModel roleCollectionModel = new RoleCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index"
            };

            try
            {
                IsSearch(roleCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                roleCollectionModel.OperationResult.ParseException(exception);
            }

            return View(roleCollectionModel);
        }

        // GET & POST: Role/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null)
        {
            WriteUrlDictionary();

            RoleCollectionModel roleCollectionModel = new RoleCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction
            };

            try
            {
                IsSearch(roleCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                roleCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView("_RoleCollection", roleCollectionModel);
        }

        // GET & POST: Role/Lookup
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

            return PartialView("_RoleLookup", lookupModel);
        }

        // GET: Role/Create
        [HttpGet]
        public ActionResult Create()
        {
            RoleItemModel roleItemModel = new RoleItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Role = new RoleViewModel(),
                ControllerAction = "Create"
            };

            try
            {
                IsCreate(roleItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                roleItemModel.OperationResult.ParseException(exception);
            }

            return View(roleItemModel);
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleItemModel roleItemModel)
        {
            try            
            {
                if (IsCreate(roleItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Create(roleItemModel.OperationResult, (Role)roleItemModel.Role.ToData()))
                        {
                            if (UnitOfWork.Save(roleItemModel.OperationResult))
                            {
                                return RedirectToUrlDictionary();
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                roleItemModel.OperationResult.ParseException(exception);
            }

            roleItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(roleItemModel);
        }

        // GET: Role/Read/1
        [HttpGet]
        public ActionResult Read(string id)
        {
            RoleItemModel roleItemModel = new RoleItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Role = new RoleViewModel(),
                ControllerAction = "Read"
            };
            
            try
            {
                if (IsRead(roleItemModel.OperationResult))
                {
                    Role role = Repository.GetById(new object[] { id });
                    if (role != null)
                    {
                        roleItemModel.Role = new RoleViewModel(role);
                    }
                }
            }
            catch (Exception exception)
            {
                roleItemModel.OperationResult.ParseException(exception);
            }            

            return View(roleItemModel);
        }

        // GET: Role/Update/1
        [HttpGet]
        public ActionResult Update(string id)
        {
            RoleItemModel roleItemModel = new RoleItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Role = new RoleViewModel(),
                ControllerAction = "Update"
            };
            
            try
            {
                if (IsUpdate(roleItemModel.OperationResult))
                {
                    Role role = Repository.GetById(new object[] { id });
                    if (role != null)
                    {
                        roleItemModel.Role = new RoleViewModel(role);
                    }
                }
            }
            catch (Exception exception)
            {
                roleItemModel.OperationResult.ParseException(exception);
            }            

            return View(roleItemModel);
        }

        // POST: Role/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(RoleItemModel roleItemModel)
        {
            try
            {
                if (IsUpdate(roleItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Update(roleItemModel.OperationResult, (Role)roleItemModel.Role.ToData()))
                        {
                            if (UnitOfWork.Save(roleItemModel.OperationResult))
                            {
                                return RedirectToUrlDictionary();
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                roleItemModel.OperationResult.ParseException(exception);
            }

            roleItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(roleItemModel);
        }

        // GET: Role/Delete/1
        [HttpGet]
        public ActionResult Delete(string id)
        {
            RoleItemModel roleItemModel = new RoleItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Role = new RoleViewModel(),
                ControllerAction = "Delete"
            };
            
            try
            {
                if (IsDelete(roleItemModel.OperationResult))
                {
                    Role role = Repository.GetById(new object[] { id });
                    if (role != null)
                    {
                        roleItemModel.Role = new RoleViewModel(role);
                    }
                }
            }
            catch (Exception exception)
            {
                roleItemModel.OperationResult.ParseException(exception);
            }            

            return View(roleItemModel);
        }

        // POST: Role/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(RoleItemModel roleItemModel)
        {
            try
            {
                if (IsDelete(roleItemModel.OperationResult))
                {
                    if (Repository.Delete(roleItemModel.OperationResult, (Role)roleItemModel.Role.ToData()))
                    {
                        if (UnitOfWork.Save(roleItemModel.OperationResult))
                        {
                            return RedirectToUrlDictionary();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                roleItemModel.OperationResult.ParseException(exception);
            }

            roleItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(roleItemModel);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Role/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<RoleViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Role), UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<RoleViewModel, RoleDTO, Role>.ToViewList(Repository.Select(where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: Role/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, RoleResources.EntitySingular + ".xlsx");
        }

        // POST: Role/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, RoleResources.EntitySingular + ".pdf");
        }

        // POST: Role/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, RoleResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}