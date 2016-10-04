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
    public class InvoiceController : BaseControllerSCRUDApplication<InvoiceDTO, Invoice>
    {
        #region Methods

        public InvoiceController(IChinookGenericApplicationDTO<InvoiceDTO, Invoice> application)
        {
            Application = application;
        }
        
        #endregion
        
        #region Methods SCRUD

        // GET: Invoice
        // GET: Invoice/Index
        [HttpGet]
        public ActionResult Index(int? masterCustomerId = null)
        {
            ClearUrlDictionary();

            InvoiceCollectionModel invoiceCollectionModel = new InvoiceCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index",
                MasterCustomerId = masterCustomerId
            };

            try
            {
                IsSearch(invoiceCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                invoiceCollectionModel.OperationResult.ParseException(exception);
            }

            return View(invoiceCollectionModel);
        }        

        // GET & POST: Invoice/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null, int? masterCustomerId = null)
        {
            WriteUrlDictionary();

            InvoiceCollectionModel invoiceCollectionModel = new InvoiceCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction,
                MasterCustomerId = masterCustomerId
            };

            try
            {
                IsSearch(invoiceCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                invoiceCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView(invoiceCollectionModel);
        }

        // GET & POST: Invoice/Lookup
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

            return PartialView("_InvoiceLookup", lookupModel);
        }

        // GET: Invoice/Create
        [HttpGet]
        public ActionResult Create(int? masterCustomerId = null)
        {
            InvoiceItemModel invoiceItemModel = new InvoiceItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Invoice = new InvoiceViewModel(),
                ControllerAction = "Create",
                MasterCustomerId = masterCustomerId
            };

            try
            {
                IsCreate(invoiceItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                invoiceItemModel.OperationResult.ParseException(exception);
            }

            return View(invoiceItemModel);
        }

        // POST: Invoice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InvoiceItemModel invoiceItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Create(invoiceItemModel.OperationResult, (InvoiceDTO)invoiceItemModel.Invoice.ToDTO()))
                    {
                        return RedirectToUrlDictionary();
                    }
                }
            }
            catch (Exception exception)
            {
                invoiceItemModel.OperationResult.ParseException(exception);
            }

            invoiceItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(invoiceItemModel);
        }

        // GET: Invoice/Read/1
        [HttpGet]
        public ActionResult Read(int? invoiceId, int? masterCustomerId = null)
        {
            InvoiceItemModel invoiceItemModel = new InvoiceItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Invoice = new InvoiceViewModel(),
                ControllerAction = "Read",
                MasterCustomerId = masterCustomerId
            };
            
            try
            {
                InvoiceDTO invoiceDTO = Application.GetById(invoiceItemModel.OperationResult, new object[] { invoiceId });
                if (invoiceDTO != null)
                {
                    invoiceItemModel.Invoice = new InvoiceViewModel(invoiceDTO);
                }
            }
            catch (Exception exception)
            {
                invoiceItemModel.OperationResult.ParseException(exception);
            }

            return View(invoiceItemModel);
        }

        // GET: Invoice/Update/1
        [HttpGet]
        public ActionResult Update(int? invoiceId, int? masterCustomerId = null)
        {
            InvoiceItemModel invoiceItemModel = new InvoiceItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Invoice = new InvoiceViewModel(),
                ControllerAction = "Update",
                MasterCustomerId = masterCustomerId
            };
            
            try
            {
                InvoiceDTO invoiceDTO = Application.GetById(invoiceItemModel.OperationResult, new object[] { invoiceId });
                if (invoiceDTO != null)
                {
                    invoiceItemModel.Invoice = new InvoiceViewModel(invoiceDTO);
                }
            }
            catch (Exception exception)
            {
                invoiceItemModel.OperationResult.ParseException(exception);
            }

            return View(invoiceItemModel);
        }

        // POST: Invoice/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(InvoiceItemModel invoiceItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Update(invoiceItemModel.OperationResult, (InvoiceDTO)invoiceItemModel.Invoice.ToDTO()))
                    {
                        return RedirectToUrlDictionary();
                    }
                }
            }
            catch (Exception exception)
            {
                invoiceItemModel.OperationResult.ParseException(exception);
            }

            invoiceItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(invoiceItemModel);
        }

        // GET: Invoice/Delete/1
        [HttpGet]
        public ActionResult Delete(int? invoiceId, int? masterCustomerId = null)
        {
            InvoiceItemModel invoiceItemModel = new InvoiceItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                Invoice = new InvoiceViewModel(),
                ControllerAction = "Delete",
                MasterCustomerId = masterCustomerId
            };
            
            try
            {
                InvoiceDTO invoiceDTO = Application.GetById(invoiceItemModel.OperationResult, new object[] { invoiceId });
                if (invoiceDTO != null)
                {
                    invoiceItemModel.Invoice = new InvoiceViewModel(invoiceDTO);
                }
            }
            catch (Exception exception)
            {
                invoiceItemModel.OperationResult.ParseException(exception);
            }

            return View(invoiceItemModel);
        }

        // POST: Invoice/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(InvoiceItemModel invoiceItemModel)
        {
            try
            {
                if (Application.Delete(invoiceItemModel.OperationResult, (InvoiceDTO)invoiceItemModel.Invoice.ToDTO()))
                {
                    return RedirectToUrlDictionary();
                }
            }
            catch (Exception exception)
            {
                invoiceItemModel.OperationResult.ParseException(exception);
            }

            invoiceItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(invoiceItemModel);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: Invoice/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<InvoiceViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(Invoice), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<InvoiceViewModel, InvoiceDTO, Invoice>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: Invoice/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, InvoiceResources.EntitySingular + ".xlsx");
        }

        // POST: Invoice/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, InvoiceResources.EntitySingular + ".pdf");
        }

        // POST: Invoice/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, InvoiceResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}