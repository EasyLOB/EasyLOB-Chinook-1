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
using EasyLOB.Persistence;

namespace EasyLOB.Security.Mvc
{
    public class UserRoleController : BaseControllerSCRUDPersistence<UserRole>
    {
        #region Methods

        public UserRoleController(ISecurityUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        
        #endregion
        
        #region Methods SCRUD

        // GET: UserRole
        // GET: UserRole/Index
        [HttpGet]
        public ActionResult Index(string masterRoleId = null, string masterUserId = null)
        {
            ClearUrlDictionary();

            UserRoleCollectionModel userRoleCollectionModel = new UserRoleCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index",
                MasterRoleId = masterRoleId, MasterUserId = masterUserId
            };

            try
            {
                IsSearch(userRoleCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                userRoleCollectionModel.OperationResult.ParseException(exception);
            }

            return View(userRoleCollectionModel);
        }

        // GET & POST: UserRole/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterUrl = null, string masterControllerAction = null, string masterRoleId = null, string masterUserId = null)
        {
            WriteUrlDictionary(masterUrl);

            UserRoleCollectionModel userRoleCollectionModel = new UserRoleCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction,
                MasterRoleId = masterRoleId, MasterUserId = masterUserId
            };

            try
            {
                IsSearch(userRoleCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                userRoleCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView(userRoleCollectionModel);
        }

        // GET & POST: UserRole/Lookup
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

            return PartialView("_UserRoleLookup", lookupModel);
        }

        // GET: UserRole/Create
        [HttpGet]
        public ActionResult Create(string masterRoleId = null, string masterUserId = null)
        {
            UserRoleItemModel userRoleItemModel = new UserRoleItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                UserRole = new UserRoleViewModel(),
                ControllerAction = "Create",
                MasterRoleId = masterRoleId, MasterUserId = masterUserId
            };

            try
            {
                IsCreate(userRoleItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                userRoleItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(userRoleItemModel);
        }

        // POST: UserRole/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserRoleItemModel userRoleItemModel)
        {
            try
            {
                if (IsCreate(userRoleItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Create(userRoleItemModel.OperationResult, (UserRole)userRoleItemModel.UserRole.ToData()))
                        {
                            UnitOfWork.Save(userRoleItemModel.OperationResult);
                            {
                                return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userRoleItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userRoleItemModel.OperationResult);
        }

        // GET: UserRole/Read/1
        [HttpGet]
        public ActionResult Read(string userId, string roleId, string masterRoleId = null, string masterUserId = null)
        {
            UserRoleItemModel userRoleItemModel = new UserRoleItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                UserRole = new UserRoleViewModel(),
                ControllerAction = "Read",
                MasterRoleId = masterRoleId, MasterUserId = masterUserId
            };
            
            try
            {
                if (IsRead(userRoleItemModel.OperationResult))
                {
                    UserRole userRole = Repository.GetById(new object[] { userId, roleId });
                    if (userRole != null)
                    {
                        userRoleItemModel.UserRole = new UserRoleViewModel(userRole);
                    }
                }
            }
            catch (Exception exception)
            {
                userRoleItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(userRoleItemModel);
        }

        // GET: UserRole/Update/1
        [HttpGet]
        public ActionResult Update(string userId, string roleId, string masterRoleId = null, string masterUserId = null)
        {
            UserRoleItemModel userRoleItemModel = new UserRoleItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                UserRole = new UserRoleViewModel(),
                ControllerAction = "Update",
                MasterRoleId = masterRoleId, MasterUserId = masterUserId
            };
            
            try
            {
                if (IsUpdate(userRoleItemModel.OperationResult))
                {
                    UserRole userRole = Repository.GetById(new object[] { userId, roleId });
                    if (userRole != null)
                    {
                        userRoleItemModel.UserRole = new UserRoleViewModel(userRole);
                    }
                }
            }
            catch (Exception exception)
            {
                userRoleItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(userRoleItemModel);
        }

        // POST: UserRole/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserRoleItemModel userRoleItemModel)
        {
            try
            {
                if (IsUpdate(userRoleItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Update(userRoleItemModel.OperationResult, (UserRole)userRoleItemModel.UserRole.ToData()))
                        {
                            if (UnitOfWork.Save(userRoleItemModel.OperationResult))
                            {
                                return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userRoleItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userRoleItemModel.OperationResult);
        }

        // GET: UserRole/Delete/1
        [HttpGet]
        public ActionResult Delete(string userId, string roleId, string masterRoleId = null, string masterUserId = null)
        {
            UserRoleItemModel userRoleItemModel = new UserRoleItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                UserRole = new UserRoleViewModel(),
                ControllerAction = "Delete",
                MasterRoleId = masterRoleId, MasterUserId = masterUserId
            };
            
            try
            {
                if (IsDelete(userRoleItemModel.OperationResult))
                {
                    UserRole userRole = Repository.GetById(new object[] { userId, roleId });
                    if (userRole != null)
                    {
                        userRoleItemModel.UserRole = new UserRoleViewModel(userRole);
                    }
                }
            }
            catch (Exception exception)
            {
                userRoleItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(userRoleItemModel);
        }

        // POST: UserRole/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserRoleItemModel userRoleItemModel)
        {
            try
            {
                if (IsDelete(userRoleItemModel.OperationResult))
                {
                    if (Repository.Delete(userRoleItemModel.OperationResult, (UserRole)userRoleItemModel.UserRole.ToData()))
                    {
                        if (UnitOfWork.Save(userRoleItemModel.OperationResult))
                        {
                            return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userRoleItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userRoleItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: UserRole/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<UserRoleViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(UserRole), UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<UserRoleViewModel, UserRoleDTO, UserRole>.ToViewList(Repository.Select(where, args.ToArray(), orderBy, dataManager.Skip, take));
        
                    if (dataManager.RequiresCounts)
                    {
                        countAll = Repository.Count(where, args.ToArray());
                    }
                }
                catch (Exception exception)
                {
                    operationResult.ParseException(exception);
                }
            }

            return Json(JsonConvert.SerializeObject(new { result = data, count = countAll }), JsonRequestBehavior.AllowGet);
        } 

        // POST: UserRole/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, UserRoleResources.EntitySingular + ".xlsx");
        }

        // POST: UserRole/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, UserRoleResources.EntitySingular + ".pdf");
        }

        // POST: UserRole/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, UserRoleResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}