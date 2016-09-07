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
    public class UserController : BaseControllerSCRUDPersistence<User>
    {
        #region Methods

        public UserController(ISecurityUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        #endregion

        #region Methods SCRUD

        // GET: User
        // GET: User/Index
        [HttpGet]
        public ActionResult Index()
        {
            ClearUrlDictionary();

            UserCollectionModel userCollectionModel = new UserCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index"
            };

            try
            {
                IsSearch(userCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                userCollectionModel.OperationResult.ParseException(exception);
            }

            return View(userCollectionModel);
        }

        // GET & POST: User/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterUrl = null, string masterControllerAction = null)
        {
            WriteUrlDictionary(masterUrl);

            UserCollectionModel userCollectionModel = new UserCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction
            };

            try
            {
                IsSearch(userCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                userCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView("_UserCollection", userCollectionModel);
        }

        // GET & POST: User/Lookup
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

            return PartialView("_UserLookup", lookupModel);
        }

        // GET: User/Create
        [HttpGet]
        public ActionResult Create()
        {
            UserItemModel userItemModel = new UserItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                User = new UserViewModel(),
                ControllerAction = "Create"
            };

            try
            {
                IsCreate(userItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                userItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(userItemModel);
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserItemModel userItemModel)
        {
            try
            {
                if (IsCreate(userItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Create(userItemModel.OperationResult, (User)userItemModel.User.ToData()))
                        {
                            UnitOfWork.Save(userItemModel.OperationResult);
                            {
                                return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userItemModel.OperationResult);
        }

        // GET: User/Read/1
        [HttpGet]
        public ActionResult Read(string id)
        {
            UserItemModel userItemModel = new UserItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                User = new UserViewModel(),
                ControllerAction = "Read"
            };
            
            try
            {
                if (IsRead(userItemModel.OperationResult))
                {
                    User user = Repository.GetById(new object[] { id });
                    if (user != null)
                    {
                        userItemModel.User = new UserViewModel(user);
                    }
                }
            }
            catch (Exception exception)
            {
                userItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(userItemModel);
        }

        // GET: User/Update/1
        [HttpGet]
        public ActionResult Update(string id)
        {
            UserItemModel userItemModel = new UserItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                User = new UserViewModel(),
                ControllerAction = "Update"
            };
            
            try
            {
                if (IsUpdate(userItemModel.OperationResult))
                {
                    User user = Repository.GetById(new object[] { id });
                    if (user != null)
                    {
                        userItemModel.User = new UserViewModel(user);
                    }
                }
            }
            catch (Exception exception)
            {
                userItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(userItemModel);
        }

        // POST: User/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserItemModel userItemModel)
        {
            /* !!!
            try
            {
                if (IsUpdate(userItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Update(userItemModel.OperationResult, (User)userItemModel.User.ToData()))
                        {
                            if (UnitOfWork.Save(userItemModel.OperationResult))
                            {
                                return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userItemModel.OperationResult);
             */

            return JsonResultSuccess(new { uri = ReadUrlDictionary() });
        }

        // GET: User/Delete/1
        [HttpGet]
        public ActionResult Delete(string id)
        {
            UserItemModel userItemModel = new UserItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                User = new UserViewModel(),
                ControllerAction = "Delete"
            };
            
            try
            {
                if (IsDelete(userItemModel.OperationResult))
                {
                    User user = Repository.GetById(new object[] { id });
                    if (user != null)
                    {
                        userItemModel.User = new UserViewModel(user);
                    }
                }
            }
            catch (Exception exception)
            {
                userItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(userItemModel);
        }

        // POST: User/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserItemModel userItemModel)
        {
            try
            {
                if (IsDelete(userItemModel.OperationResult))
                {
                    if (Repository.Delete(userItemModel.OperationResult, (User)userItemModel.User.ToData()))
                    {
                        if (UnitOfWork.Save(userItemModel.OperationResult))
                        {
                            return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: User/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<UserViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(User), UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<UserViewModel, UserDTO, User>.ToViewList(Repository.Select(where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: User/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, UserResources.EntitySingular + ".xlsx");
        }

        // POST: User/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, UserResources.EntitySingular + ".pdf");
        }

        // POST: User/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, UserResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}