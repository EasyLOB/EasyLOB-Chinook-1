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
    public class UserLoginController : BaseControllerSCRUDPersistence<UserLogin>
    {
        #region Methods

        public UserLoginController(ISecurityUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        
        #endregion
        
        #region Methods SCRUD

        // GET: UserLogin
        // GET: UserLogin/Index
        [HttpGet]
        public ActionResult Index(string masterUserId = null)
        {
            ClearUrlDictionary();

            UserLoginCollectionModel userLoginCollectionModel = new UserLoginCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index",
                MasterUserId = masterUserId
            };

            try
            {
                IsSearch(userLoginCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                userLoginCollectionModel.OperationResult.ParseException(exception);
            }

            return View(userLoginCollectionModel);
        }

        // GET & POST: UserLogin/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterUrl = null, string masterControllerAction = null, string masterUserId = null)
        {
            WriteUrlDictionary(masterUrl);

            UserLoginCollectionModel userLoginCollectionModel = new UserLoginCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction,
                MasterUserId = masterUserId
            };

            try
            {
                IsSearch(userLoginCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                userLoginCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView("_UserLoginCollection", userLoginCollectionModel);
        }

        // GET & POST: UserLogin/Lookup
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

            return PartialView("_UserLoginLookup", lookupModel);
        }

        // GET: UserLogin/Create
        [HttpGet]
        public ActionResult Create(string masterUserId = null)
        {
            UserLoginItemModel userLoginItemModel = new UserLoginItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                UserLogin = new UserLoginViewModel(),
                ControllerAction = "Create",
                MasterUserId = masterUserId
            };

            try
            {
                IsCreate(userLoginItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                userLoginItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(userLoginItemModel);
        }

        // POST: UserLogin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserLoginItemModel userLoginItemModel)
        {
            try
            {
                if (IsCreate(userLoginItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Create(userLoginItemModel.OperationResult, (UserLogin)userLoginItemModel.UserLogin.ToData()))
                        {
                            UnitOfWork.Save(userLoginItemModel.OperationResult);
                            {
                                return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userLoginItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userLoginItemModel.OperationResult);
        }

        // GET: UserLogin/Read/1
        [HttpGet]
        public ActionResult Read(string loginProvider, string providerKey, string userId, string masterUserId = null)
        {
            UserLoginItemModel userLoginItemModel = new UserLoginItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                UserLogin = new UserLoginViewModel(),
                ControllerAction = "Read",
                MasterUserId = masterUserId
            };
            
            try
            {
                if (IsRead(userLoginItemModel.OperationResult))
                {
                    UserLogin userLogin = Repository.GetById(new object[] { loginProvider, providerKey, userId });
                    if (userLogin != null)
                    {
                        userLoginItemModel.UserLogin = new UserLoginViewModel(userLogin);
                    }
                }
            }
            catch (Exception exception)
            {
                userLoginItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(userLoginItemModel);
        }

        // GET: UserLogin/Update/1
        [HttpGet]
        public ActionResult Update(string loginProvider, string providerKey, string userId, string masterUserId = null)
        {
            UserLoginItemModel userLoginItemModel = new UserLoginItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                UserLogin = new UserLoginViewModel(),
                ControllerAction = "Update",
                MasterUserId = masterUserId
            };
            
            try
            {
                if (IsUpdate(userLoginItemModel.OperationResult))
                {
                    UserLogin userLogin = Repository.GetById(new object[] { loginProvider, providerKey, userId });
                    if (userLogin != null)
                    {
                        userLoginItemModel.UserLogin = new UserLoginViewModel(userLogin);
                    }
                }
            }
            catch (Exception exception)
            {
                userLoginItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(userLoginItemModel);
        }

        // POST: UserLogin/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserLoginItemModel userLoginItemModel)
        {
            try
            {
                if (IsUpdate(userLoginItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Update(userLoginItemModel.OperationResult, (UserLogin)userLoginItemModel.UserLogin.ToData()))
                        {
                            if (UnitOfWork.Save(userLoginItemModel.OperationResult))
                            {
                                return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userLoginItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userLoginItemModel.OperationResult);
        }

        // GET: UserLogin/Delete/1
        [HttpGet]
        public ActionResult Delete(string loginProvider, string providerKey, string userId, string masterUserId = null)
        {
            UserLoginItemModel userLoginItemModel = new UserLoginItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                UserLogin = new UserLoginViewModel(),
                ControllerAction = "Delete",
                MasterUserId = masterUserId
            };
            
            try
            {
                if (IsDelete(userLoginItemModel.OperationResult))
                {
                    UserLogin userLogin = Repository.GetById(new object[] { loginProvider, providerKey, userId });
                    if (userLogin != null)
                    {
                        userLoginItemModel.UserLogin = new UserLoginViewModel(userLogin);
                    }
                }
            }
            catch (Exception exception)
            {
                userLoginItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(userLoginItemModel);
        }

        // POST: UserLogin/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserLoginItemModel userLoginItemModel)
        {
            try
            {
                if (IsDelete(userLoginItemModel.OperationResult))
                {
                    if (Repository.Delete(userLoginItemModel.OperationResult, (UserLogin)userLoginItemModel.UserLogin.ToData()))
                    {
                        if (UnitOfWork.Save(userLoginItemModel.OperationResult))
                        {
                            return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userLoginItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userLoginItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: UserLogin/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<UserLoginViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(UserLogin), UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<UserLoginViewModel, UserLoginDTO, UserLogin>.ToViewList(Repository.Select(where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: UserLogin/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, UserLoginResources.EntitySingular + ".xlsx");
        }

        // POST: UserLogin/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, UserLoginResources.EntitySingular + ".pdf");
        }

        // POST: UserLogin/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, UserLoginResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}