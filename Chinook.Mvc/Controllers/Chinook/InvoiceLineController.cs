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
    public class InvoiceLineController : BaseControllerSCRUDApplication<InvoiceLineDTO, InvoiceLine>
    {
        #region Methods

        public InvoiceLineController(IChinookGenericApplicationDTO<InvoiceLineDTO, InvoiceLine> application)
        {
            Application = application;
        }
        
        #endregion
        
        #region Methods SCRUD

        // GET: InvoiceLine
        // GET: InvoiceLine/Index
        [HttpGet]
        public ActionResult Index(int? masterInvoiceId = null, int? masterTrackId = null)
        {
            ClearUrlDictionary();

            InvoiceLineCollectionModel invoiceLineCollectionModel = new InvoiceLineCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Index",
                MasterInvoiceId = masterInvoiceId, MasterTrackId = masterTrackId
            };

            try
            {
                IsSearch(invoiceLineCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                invoiceLineCollectionModel.OperationResult.ParseException(exception);
            }

            return View(invoiceLineCollectionModel);
        }        

        // GET & POST: InvoiceLine/Search
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Search(string masterControllerAction = null, int? masterInvoiceId = null, int? masterTrackId = null)
        {
            WriteUrlDictionary();

            InvoiceLineCollectionModel invoiceLineCollectionModel = new InvoiceLineCollectionModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                ControllerAction = "Search",
                MasterControllerAction = masterControllerAction,
                MasterInvoiceId = masterInvoiceId, MasterTrackId = masterTrackId
            };

            try
            {
                IsSearch(invoiceLineCollectionModel.OperationResult);
            }
            catch (Exception exception)
            {
                invoiceLineCollectionModel.OperationResult.ParseException(exception);
            }

            return PartialView(invoiceLineCollectionModel);
        }

        // GET & POST: InvoiceLine/Lookup
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

            return PartialView("_InvoiceLineLookup", lookupModel);
        }

        // GET: InvoiceLine/Create
        [HttpGet]
        public ActionResult Create(int? masterInvoiceId = null, int? masterTrackId = null)
        {
            InvoiceLineItemModel invoiceLineItemModel = new InvoiceLineItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                InvoiceLine = new InvoiceLineViewModel(),
                ControllerAction = "Create",
                MasterInvoiceId = masterInvoiceId, MasterTrackId = masterTrackId
            };

            try
            {
                IsCreate(invoiceLineItemModel.OperationResult);
            }
            catch (Exception exception)
            {
                invoiceLineItemModel.OperationResult.ParseException(exception);
            }

            return View(invoiceLineItemModel);
        }

        // POST: InvoiceLine/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InvoiceLineItemModel invoiceLineItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Create(invoiceLineItemModel.OperationResult, (InvoiceLineDTO)invoiceLineItemModel.InvoiceLine.ToDTO()))
                    {
                        return RedirectToUrlDictionary();
                    }
                }
            }
            catch (Exception exception)
            {
                invoiceLineItemModel.OperationResult.ParseException(exception);
            }

            invoiceLineItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(invoiceLineItemModel);
        }

        // GET: InvoiceLine/Read/1
        [HttpGet]
        public ActionResult Read(int? invoiceLineId, int? masterInvoiceId = null, int? masterTrackId = null)
        {
            InvoiceLineItemModel invoiceLineItemModel = new InvoiceLineItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                InvoiceLine = new InvoiceLineViewModel(),
                ControllerAction = "Read",
                MasterInvoiceId = masterInvoiceId, MasterTrackId = masterTrackId
            };
            
            try
            {
                InvoiceLineDTO invoiceLineDTO = Application.GetById(invoiceLineItemModel.OperationResult, new object[] { invoiceLineId });
                if (invoiceLineDTO != null)
                {
                    invoiceLineItemModel.InvoiceLine = new InvoiceLineViewModel(invoiceLineDTO);
                }
            }
            catch (Exception exception)
            {
                invoiceLineItemModel.OperationResult.ParseException(exception);
            }

            return View(invoiceLineItemModel);
        }

        // GET: InvoiceLine/Update/1
        [HttpGet]
        public ActionResult Update(int? invoiceLineId, int? masterInvoiceId = null, int? masterTrackId = null)
        {
            InvoiceLineItemModel invoiceLineItemModel = new InvoiceLineItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                InvoiceLine = new InvoiceLineViewModel(),
                ControllerAction = "Update",
                MasterInvoiceId = masterInvoiceId, MasterTrackId = masterTrackId
            };
            
            try
            {
                InvoiceLineDTO invoiceLineDTO = Application.GetById(invoiceLineItemModel.OperationResult, new object[] { invoiceLineId });
                if (invoiceLineDTO != null)
                {
                    invoiceLineItemModel.InvoiceLine = new InvoiceLineViewModel(invoiceLineDTO);
                }
            }
            catch (Exception exception)
            {
                invoiceLineItemModel.OperationResult.ParseException(exception);
            }

            return View(invoiceLineItemModel);
        }

        // POST: InvoiceLine/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(InvoiceLineItemModel invoiceLineItemModel)
        {
            try
            {
                if (ValidateModelState(Application.Repository))
                {
                    if (Application.Update(invoiceLineItemModel.OperationResult, (InvoiceLineDTO)invoiceLineItemModel.InvoiceLine.ToDTO()))
                    {
                        return RedirectToUrlDictionary();
                    }
                }
            }
            catch (Exception exception)
            {
                invoiceLineItemModel.OperationResult.ParseException(exception);
            }

            invoiceLineItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(invoiceLineItemModel);
        }

        // GET: InvoiceLine/Delete/1
        [HttpGet]
        public ActionResult Delete(int? invoiceLineId, int? masterInvoiceId = null, int? masterTrackId = null)
        {
            InvoiceLineItemModel invoiceLineItemModel = new InvoiceLineItemModel()
            {
                IsSecurityOperations = this.IsSecurityOperations,
                InvoiceLine = new InvoiceLineViewModel(),
                ControllerAction = "Delete",
                MasterInvoiceId = masterInvoiceId, MasterTrackId = masterTrackId
            };
            
            try
            {
                InvoiceLineDTO invoiceLineDTO = Application.GetById(invoiceLineItemModel.OperationResult, new object[] { invoiceLineId });
                if (invoiceLineDTO != null)
                {
                    invoiceLineItemModel.InvoiceLine = new InvoiceLineViewModel(invoiceLineDTO);
                }
            }
            catch (Exception exception)
            {
                invoiceLineItemModel.OperationResult.ParseException(exception);
            }

            return View(invoiceLineItemModel);
        }

        // POST: InvoiceLine/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(InvoiceLineItemModel invoiceLineItemModel)
        {
            try
            {
                if (Application.Delete(invoiceLineItemModel.OperationResult, (InvoiceLineDTO)invoiceLineItemModel.InvoiceLine.ToDTO()))
                {
                    return RedirectToUrlDictionary();
                }
            }
            catch (Exception exception)
            {
                invoiceLineItemModel.OperationResult.ParseException(exception);
            }

            invoiceLineItemModel.IsSecurityOperations = IsSecurityOperations;

            return View(invoiceLineItemModel);
        }
        
        #endregion Methods SCRUD
        
        #region Methods Syncfusion

        // POST: InvoiceLine/DataSource
        [HttpPost]
        public ActionResult DataSource(DataManager dataManager)
        {
            IEnumerable data = new List<InvoiceLineViewModel>();
            int countAll = 0;
            ZOperationResult operationResult = new ZOperationResult();

            if (IsSearch())
            {
                try
                {
                    SyncfusionGrid syncfusionGrid = new SyncfusionGrid(typeof(InvoiceLine), Application.UnitOfWork.DBMS);
                    ArrayList args = new ArrayList();
                    string where = syncfusionGrid.ToLinqWhere(dataManager.Search, dataManager.Where, args);
                    string orderBy = syncfusionGrid.ToLinqOrderBy(dataManager.Sorted);        
                    int take = (dataManager.Skip == 0 && dataManager.Take == 0) ? AppDefaults.SyncfusionRecordsBySearch : dataManager.Take; // Excel Filtering
                    data = ZViewHelper<InvoiceLineViewModel, InvoiceLineDTO, InvoiceLine>.ToViewList(Application.Select(operationResult, where, args.ToArray(), orderBy, dataManager.Skip, take));
        
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

        // POST: InvoiceLine/ExportToExcel
        [HttpPost]
        public void ExportToExcel(string gridModel)
        {
            ExportToExcel(gridModel, InvoiceLineResources.EntitySingular + ".xlsx");
        }

        // POST: InvoiceLine/ExportToPdf
        [HttpPost]
        public void ExportToPdf(string gridModel)
        {
            ExportToPdf(gridModel, InvoiceLineResources.EntitySingular + ".pdf");
        }

        // POST: InvoiceLine/ExportToWord
        [HttpPost]
        public void ExportToWord(string gridModel)
        {
            ExportToWord(gridModel, InvoiceLineResources.EntitySingular + ".docx");
        }
        
        #endregion Methods Syncfusion
    }
}