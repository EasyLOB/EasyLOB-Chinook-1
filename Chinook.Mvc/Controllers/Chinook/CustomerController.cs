using Newtonsoft.Json;
using Syncfusion.JavaScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Chinook.Application;
using Chinook.Data;
using Chinook.Data.Resources;
using EasyLOB;
using EasyLOB.Data;
using EasyLOB.Library;
using EasyLOB.Library.Mvc;
using EasyLOB.Library.Syncfusion;

namespace Chinook.Mvc
{
    public class CustomerController : BaseControllerSCRUDApplication<CustomerDTO, Customer>
    {
        #region Methods

        public CustomerController(IChinookGenericApplicationDTO<CustomerDTO, Customer> application)
        {
            Application = application;
        }
        
        #endregion
        
        #region Methods SCRUD

        // GET: Customer
        // GET: Customer/Index
        [HttpGet]
        public ActionResult Index(int? masterSupportRepId = null)
        {
            ClearUrlDictionary();

            CustomerCollectionModel customerCollectionModel = new CustomerCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index",
                MasterSupportRepId = masterSupportRepId
            };

            try
            {
                IsSearch(customerCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                customerCollectionModel.OperationResult.ParseException(exception);
            }

            return View(customerCollectionModel);
        }        

        // GET & POST: Customer/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null, int? masterSupportRepId = null)
        {
            WriteUrlDictionary();

            CustomerCollectionModel customerCollectionModel = new CustomerCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction,
                MasterSupportRepId = masterSupportRepId
            };

            try
            {
                IsSearch(customerCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                customerCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView(customerCollectionModel);
        }

        // GET & POST: Customer/Lookup
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

            return PartialView("_CustomerLookup", lookupModel);
        }

        // GET: Customer/Create
        [HttpGet]
        public ActionResult Create(int? masterSupportRepId = null)
        {
            CustomerItemModel customerItemModel = new CustomerItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Customer = new CustomerViewModel(),
                ControllerAction = "Create",
                MasterSupportRepId = masterSupportRepId
            };

            try
            {
                IsCreate(customerItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                customerItemModel.OperationResult.ParseException(exception);
            }

            return View(customerItemModel);
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerItemModel customerItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Create(customerItemModel.OperationResult, (CustomerDTO)customerItemModel.Customer.ToDTO()))
                    {
                        return RedirectToUrlDictionary();
                    }
                }
            }
            catch (Exception exception)
            {
                customerItemModel.OperationResult.ParseException(exception);
            }

            customerItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(customerItemModel);
        }

        // GET: Customer/Read/1
        [HttpGet]
        public ActionResult Read(int? customerId, int? masterSupportRepId = null)
        {
            CustomerItemModel customerItemModel = new CustomerItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Customer = new CustomerViewModel(),
                ControllerAction = "Read",
                MasterSupportRepId = masterSupportRepId
            };
            
            try
            {
                CustomerDTO customerDTO = Application.GetById(customerItemModel.OperationResult, new object[] { customerId });
                if (customerDTO != null)
                {
                    customerItemModel.Customer = new CustomerViewModel(customerDTO);
                }
            }
            catch (Exception exception)
            {
                customerItemModel.OperationResult.ParseException(exception);
            }

            return View(customerItemModel);
        }

        // GET: Customer/Update/1
        [HttpGet]
        public ActionResult Update(int? customerId, int? masterSupportRepId = null)
        {
            CustomerItemModel customerItemModel = new CustomerItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Customer = new CustomerViewModel(),
                ControllerAction = "Update",
                MasterSupportRepId = masterSupportRepId
            };
            
            try
            {
                CustomerDTO customerDTO = Application.GetById(customerItemModel.OperationResult, new object[] { customerId });
                if (customerDTO != null)
                {
                    customerItemModel.Customer = new CustomerViewModel(customerDTO);
                }
            }
            catch (Exception exception)
            {
                customerItemModel.OperationResult.ParseException(exception);
            }

            return View(customerItemModel);
        }

        // POST: Customer/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CustomerItemModel customerItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Update(customerItemModel.OperationResult, (CustomerDTO)customerItemModel.Customer.ToDTO()))
                    {
                        return RedirectToUrlDictionary();
                    }
                }
            }
            catch (Exception exception)
            {
                customerItemModel.OperationResult.ParseException(exception);
            }

            customerItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(customerItemModel);
        }

        // GET: Customer/Delete/1
        [HttpGet]
        public ActionResult Delete(int? customerId, int? masterSupportRepId = null)
        {
            CustomerItemModel customerItemModel = new CustomerItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Customer = new CustomerViewModel(),
                ControllerAction = "Delete",
                MasterSupportRepId = masterSupportRepId
            };
            
            try
            {
                CustomerDTO customerDTO = Application.GetById(customerItemModel.OperationResult, new object[] { customerId });
                if (customerDTO != null)
                {
                    customerItemModel.Customer = new CustomerViewModel(customerDTO);
                }
            }
            catch (Exception exception)
            {
                customerItemModel.OperationResult.ParseException(exception);
            }

            return View(customerItemModel);
        }

        // POST: Customer/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CustomerItemModel customerItemModel)
        {
            try
            {
                if (Application.Delete(customerItemModel.OperationResult, (CustomerDTO)customerItemModel.Customer.ToDTO()))
                {
                    return RedirectToUrlDictionary();
                }
            }
            catch (Exception exception)
            {
                customerItemModel.OperationResult.ParseException(exception);
            }

            customerItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(customerItemModel);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Customer/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<CustomerViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Customer), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<CustomerViewModel, CustomerDTO, Customer>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
                    if (dataManager.RequiresCounts)
                    {
                        countAll = Application.Count(where, args.ToArray());
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

        // POST: Customer/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, CustomerResources.EntitySingular + ".xlsx");
        }

        // POST: Customer/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, CustomerResources.EntitySingular + ".pdf");
        }

        // POST: Customer/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, CustomerResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}