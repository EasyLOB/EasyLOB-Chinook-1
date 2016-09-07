using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Chinook.Application;
using Chinook.Data;
using EasyLOB.Library.Mvc;
using EasyLOB.Library.Resources;

/*
SELECT TOP 10 * FROM Invoice ORDER BY InvoiceId DESC
SELECT TOP 10 * FROM InvoiceLine ORDER BY InvoiceId DESC,InvoiceLineId DESC

--DELETE FROM InvoiceLine WHERE InvoiceId > 412
--DELETE FROM Invoice WHERE InvoiceId > 412
*/

namespace Chinook.Mvc
{
    public partial class ChinookTasksController
    {
        // GET: Tasks/Sales
        [HttpGet]
        public ActionResult Sales()
        {
            if (!IsAuthorized("Sales", OperationResult))
            {
                return View("OperationResult", new OperationResultModel(OperationResult));
            }

            TaskSalesModel viewModel = new TaskSalesModel();
            viewModel.OperationResult.StatusMessage = PresentationResources.Loading;

            return View(viewModel);
        }

        // GET: Tasks/_Sales
        [HttpGet]
        public ActionResult _Sales()
        {
            try
            {
                if (IsAuthorized("Sales", OperationResult))
                {
                    TaskSalesModel viewModel = new TaskSalesModel();

                    return PartialView("_Sales", viewModel);
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(OperationResult);
        }

        // POST: Tasks/CreateInvoice
        [HttpPost]
        public ActionResult CreateInvoice(int customerId, DateTime invoiceDate)
        {
            try
            {
                if (IsAuthorized("Sales", OperationResult))
                {
                    Invoice invoice = new Invoice(0,
                        customerId,
                        invoiceDate,
                        0);

                    IChinookGenericApplication<Invoice> invoiceApplication =
                        DependencyResolver.Current.GetService<IChinookGenericApplication<Invoice>>();
                    if (invoiceApplication.Create(OperationResult, invoice))
                    {
                        OperationResult.Data = new { InvoiceId = invoice.InvoiceId };

                        return JsonResultOperationResult(OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(OperationResult);
        }

        // POST: Tasks/CreateInvoiceLine
        [HttpPost]
        public ActionResult CreateInvoiceLine(int invoiceId, int trackId, decimal unitPrice, int quantity)
        {
            try
            {
                if (IsAuthorized("Sales", OperationResult))
                {
                    InvoiceLine invoiceLine = new InvoiceLine(0,
                        invoiceId,
                        trackId,
                        unitPrice,
                        quantity);

                    IChinookGenericApplication<InvoiceLine> invoiceLineApplication =
                        DependencyResolver.Current.GetService<IChinookGenericApplication<InvoiceLine>>();
                    if (invoiceLineApplication.Create(OperationResult, invoiceLine))
                    {
                        /*
                        IChinookGenericApplication<Invoice> invoiceApplication =
                            DependencyResolver.Current.GetService<IChinookGenericApplication<Invoice>>();
                        Invoice invoice = invoiceApplication.GetById(invoiceId);
                        OperationResult.Data = new { Total = invoice.Total }; // Invoice.Total is updated by DBMS Triggers
                         */

                        Expression<Func<InvoiceLine, bool>> where = (x => x.InvoiceId == invoiceId);
                        IEnumerable<InvoiceLine> invoiceLines = invoiceLineApplication.Select(OperationResult, where);
                        decimal total = invoiceLines.Sum(x => Convert.ToDecimal(x.Quantity * x.UnitPrice));
                        OperationResult.Data = new { Total = total };

                        return JsonResultOperationResult(OperationResult);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            return JsonResultOperationResult(OperationResult);
        }
    }
}