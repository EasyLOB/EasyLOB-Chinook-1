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
    public class CustomerDocumentController : BaseControllerSCRUDApplication<CustomerDocumentDTO, CustomerDocument>
    {
        #region Methods

        public CustomerDocumentController(IChinookGenericApplicationDTO<CustomerDocumentDTO, CustomerDocument> application)
        {
            Application = application;            
        }
        
        #endregion
        
        #region Methods SCRUD

        // GET: CustomerDocument
        // GET: CustomerDocument/Index
        [HttpGet]
        public ActionResult Index(int? masterCustomerId = null)
        {
            ClearUrlDictionary();

            CustomerDocumentCollectionModel customerDocumentCollectionModel = new CustomerDocumentCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index",
                MasterCustomerId = masterCustomerId
            };

            try
            {
                IsSearch(customerDocumentCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                customerDocumentCollectionModel.OperationResult.ParseException(exception);
            }

            return View(customerDocumentCollectionModel);
        }        

        // GET & POST: CustomerDocument/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterUrl = null, string masterControllerAction = null, int? masterCustomerId = null)
        {
            WriteUrlDictionary(masterUrl);

            CustomerDocumentCollectionModel customerDocumentCollectionModel = new CustomerDocumentCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction,
                MasterCustomerId = masterCustomerId
            };

            try
            {
                IsSearch(customerDocumentCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                customerDocumentCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView("_CustomerDocumentCollection", customerDocumentCollectionModel);
        }

        // GET & POST: CustomerDocument/Lookup
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

            return PartialView("_CustomerDocumentLookup", lookupModel);
        }

        // GET: CustomerDocument/Create
        [HttpGet]
        public ActionResult Create(int? masterCustomerId = null)
        {
            CustomerDocumentItemModel customerDocumentItemModel = new CustomerDocumentItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                CustomerDocument = new CustomerDocumentViewModel(),
                ControllerAction = "Create",
                MasterCustomerId = masterCustomerId
            };

            try
            {
                IsCreate(customerDocumentItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                customerDocumentItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(customerDocumentItemModel);
        }

        // POST: CustomerDocument/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerDocumentItemModel customerDocumentItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Create(customerDocumentItemModel.OperationResult, (CustomerDocumentDTO)customerDocumentItemModel.CustomerDocument.ToDTO()))
                    {
                        return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                    }
                }
            }
            catch (Exception exception)
            {
                customerDocumentItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(customerDocumentItemModel.OperationResult);
        }

        // GET: CustomerDocument/Read/1
        [HttpGet]
        public ActionResult Read(int? customerDocumentId, int? masterCustomerId = null)
        {
            CustomerDocumentItemModel customerDocumentItemModel = new CustomerDocumentItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                CustomerDocument = new CustomerDocumentViewModel(),
                ControllerAction = "Read",
                MasterCustomerId = masterCustomerId
            };
            
            try
            {
                CustomerDocumentDTO customerDocumentDTO = Application.GetById(customerDocumentItemModel.OperationResult, new object[] { customerDocumentId });
                if (customerDocumentDTO != null)
                {
                    customerDocumentItemModel.CustomerDocument = new CustomerDocumentViewModel(customerDocumentDTO);
                }
            }
            catch (Exception exception)
            {
                customerDocumentItemModel.OperationResult.ParseException(exception);
            }            

            return PartialView(customerDocumentItemModel);
        }

        // GET: CustomerDocument/Update/1
        [HttpGet]
        public ActionResult Update(int? customerDocumentId, int? masterCustomerId = null)
        {
            CustomerDocumentItemModel customerDocumentItemModel = new CustomerDocumentItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                CustomerDocument = new CustomerDocumentViewModel(),
                ControllerAction = "Update",
                MasterCustomerId = masterCustomerId
            };
            
            try
            {
                CustomerDocumentDTO customerDocumentDTO = Application.GetById(customerDocumentItemModel.OperationResult, new object[] { customerDocumentId });
                if (customerDocumentDTO != null)
                {
                    customerDocumentItemModel.CustomerDocument = new CustomerDocumentViewModel(customerDocumentDTO);
                }
            }
            catch (Exception exception)
            {
                customerDocumentItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(customerDocumentItemModel);
        }

        // POST: CustomerDocument/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CustomerDocumentItemModel customerDocumentItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Update(customerDocumentItemModel.OperationResult, (CustomerDocumentDTO)customerDocumentItemModel.CustomerDocument.ToDTO()))
                    {
                        return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                    }
                }
            }
            catch (Exception exception)
            {
                customerDocumentItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(customerDocumentItemModel.OperationResult);
        }

        // GET: CustomerDocument/Delete/1
        [HttpGet]
        public ActionResult Delete(int? customerDocumentId, int? masterCustomerId = null)
        {
            CustomerDocumentItemModel customerDocumentItemModel = new CustomerDocumentItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                CustomerDocument = new CustomerDocumentViewModel(),
                ControllerAction = "Delete",
                MasterCustomerId = masterCustomerId
            };
            
            try
            {
                CustomerDocumentDTO customerDocumentDTO = Application.GetById(customerDocumentItemModel.OperationResult, new object[] { customerDocumentId });
                if (customerDocumentDTO != null)
                {
                    customerDocumentItemModel.CustomerDocument = new CustomerDocumentViewModel(customerDocumentDTO);
                }
            }
            catch (Exception exception)
            {
                customerDocumentItemModel.OperationResult.ParseException(exception);
            }

            return PartialView(customerDocumentItemModel);
        }

        // POST: CustomerDocument/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CustomerDocumentItemModel customerDocumentItemModel)
        {
            try
            {
                if (Application.Delete(customerDocumentItemModel.OperationResult, (CustomerDocumentDTO)customerDocumentItemModel.CustomerDocument.ToDTO()))
                {
                    return JsonResultSuccess(new { uri = ReadUrlDictionary() });
                }
            }
            catch (Exception exception)
            {
                customerDocumentItemModel.OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(customerDocumentItemModel.OperationResult);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: CustomerDocument/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<CustomerDocumentViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(CustomerDocument), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<CustomerDocumentViewModel, CustomerDocumentDTO, CustomerDocument>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: CustomerDocument/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, CustomerDocumentResources.EntitySingular + ".xlsx");
        }

        // POST: CustomerDocument/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, CustomerDocumentResources.EntitySingular + ".pdf");
        }

        // POST: CustomerDocument/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, CustomerDocumentResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}