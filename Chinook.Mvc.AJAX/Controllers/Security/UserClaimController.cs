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
    public class UserClaimController : BaseControllerSCRUDPersistence<UserClaim>
    {
        #region Methods

        public UserClaimController(ISecurityUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        
        #endregion
        
        #region Methods SCRUD

        // GET: UserClaim
        // GET: UserClaim/Index
        [HttpGet]
        public ActionResult Index(string masterUserId = null)
        {
            ClearUrlDictionary();

            UserClaimCollectionModel userClaimCollectionModel = new UserClaimCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index",
                MasterUserId = masterUserId
            };

            try
            {
                IsSearch(userClaimCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                userClaimCollectionModel.OperationResult.ParseException(exception);
            }

            return View(userClaimCollectionModel);
        }

        // GET & POST: UserClaim/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterUrl = null, string masterControllerAction = null, string masterUserId = null)
        {
            WriteUrlDictionary(masterUrl);

            UserClaimCollectionModel userClaimCollectionModel = new UserClaimCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction,
                MasterUserId = masterUserId
            };

            try
            {
                IsSearch(userClaimCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                userClaimCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView(userClaimCollectionModel);
        }

        // GET & POST: UserClaim/Lookup
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

            return PartialView("_UserClaimLookup", lookupModel);
        }

        // GET: UserClaim/Create
        [HttpGet]
        public ActionResult Create(string masterUserId = null)
        {
            UserClaimItemModel userClaimItemModel = new UserClaimItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                UserClaim = new UserClaimViewModel(),
                ControllerAction = "Create",
                MasterUserId = masterUserId
            };

            try
            {
                IsCreate(userClaimItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                userClaimItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(userClaimItemModel);
        }

        // POST: UserClaim/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserClaimItemModel userClaimItemModel)
        {
            try
            {
                if (IsCreate(userClaimItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Create(userClaimItemModel.OperationResult, (UserClaim)userClaimItemModel.UserClaim.ToData()))
                        {
                            UnitOfWork.Save(userClaimItemModel.OperationResult);
                            {
                                return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userClaimItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userClaimItemModel.OperationResult);
        }

        // GET: UserClaim/Read/1
        [HttpGet]
        public ActionResult Read(int? id, string masterUserId = null)
        {
            UserClaimItemModel userClaimItemModel = new UserClaimItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                UserClaim = new UserClaimViewModel(),
                ControllerAction = "Read",
                MasterUserId = masterUserId
            };
            
            try
            {
                if (IsRead(userClaimItemModel.OperationResult))
                {
                    UserClaim userClaim = Repository.GetById(new object[] { id });
                    if (userClaim != null)
                    {
                        userClaimItemModel.UserClaim = new UserClaimViewModel(userClaim);
                    }
                }
            }
            catch (Exception exception)
            {
                userClaimItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(userClaimItemModel);
        }

        // GET: UserClaim/Update/1
        [HttpGet]
        public ActionResult Update(int? id, string masterUserId = null)
        {
            UserClaimItemModel userClaimItemModel = new UserClaimItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                UserClaim = new UserClaimViewModel(),
                ControllerAction = "Update",
                MasterUserId = masterUserId
            };
            
            try
            {
                if (IsUpdate(userClaimItemModel.OperationResult))
                {
                    UserClaim userClaim = Repository.GetById(new object[] { id });
                    if (userClaim != null)
                    {
                        userClaimItemModel.UserClaim = new UserClaimViewModel(userClaim);
                    }
                }
            }
            catch (Exception exception)
            {
                userClaimItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(userClaimItemModel);
        }

        // POST: UserClaim/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserClaimItemModel userClaimItemModel)
        {
            try
            {
                if (IsUpdate(userClaimItemModel.OperationResult))
                {
                    if (ValidateModelState(Repository))
                    {
                        if (Repository.Update(userClaimItemModel.OperationResult, (UserClaim)userClaimItemModel.UserClaim.ToData()))
                        {
                            if (UnitOfWork.Save(userClaimItemModel.OperationResult))
                            {
                                return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userClaimItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userClaimItemModel.OperationResult);
        }

        // GET: UserClaim/Delete/1
        [HttpGet]
        public ActionResult Delete(int? id, string masterUserId = null)
        {
            UserClaimItemModel userClaimItemModel = new UserClaimItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                UserClaim = new UserClaimViewModel(),
                ControllerAction = "Delete",
                MasterUserId = masterUserId
            };
            
            try
            {
                if (IsDelete(userClaimItemModel.OperationResult))
                {
                    UserClaim userClaim = Repository.GetById(new object[] { id });
                    if (userClaim != null)
                    {
                        userClaimItemModel.UserClaim = new UserClaimViewModel(userClaim);
                    }
                }
            }
            catch (Exception exception)
            {
                userClaimItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(userClaimItemModel);
        }

        // POST: UserClaim/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserClaimItemModel userClaimItemModel)
        {
            try
            {
                if (IsDelete(userClaimItemModel.OperationResult))
                {
                    if (Repository.Delete(userClaimItemModel.OperationResult, (UserClaim)userClaimItemModel.UserClaim.ToData()))
                    {
                        if (UnitOfWork.Save(userClaimItemModel.OperationResult))
                        {
                            return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                userClaimItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(userClaimItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: UserClaim/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<UserClaimViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(UserClaim), UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<UserClaimViewModel, UserClaimDTO, UserClaim>.ToViewList(Repository.Select(where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: UserClaim/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, UserClaimResources.EntitySingular + ".xlsx");
        }

        // POST: UserClaim/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, UserClaimResources.EntitySingular + ".pdf");
        }

        // POST: UserClaim/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, UserClaimResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}