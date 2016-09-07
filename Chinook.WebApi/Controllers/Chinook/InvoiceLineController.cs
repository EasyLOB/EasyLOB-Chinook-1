using System;
using System.Collections.Generic;
using System.Web.Http;
using Chinook.Application;
using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Library.WebApi;

namespace Chinook.WebApi
{
    public class InvoiceLineController : BaseApiControllerApplication<InvoiceLineDTO, InvoiceLine>
    {
        #region Methods

        public InvoiceLineController(IChinookGenericApplicationDTO<InvoiceLineDTO, InvoiceLine> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/invoiceLine/1
        [Route("api/invoiceLine/{invoiceLineId}")]
        public IHttpActionResult DeleteInvoiceLine(int invoiceLineId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                InvoiceLineDTO invoiceLineDTO = Application.GetById(operationResult, new object[] { invoiceLineId });    
                if (invoiceLineDTO != null)
                {
                    if (Application.Delete(operationResult, invoiceLineDTO))
                    {
                        return Ok();
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/invoiceLine
        public IHttpActionResult GetInvoiceLines()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<InvoiceLineDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/invoiceLine/1
        [Route("api/invoiceLine/{invoiceLineId}")]
        public IHttpActionResult GetInvoiceLine(int invoiceLineId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                InvoiceLineDTO invoiceLineDTO = Application.GetById(operationResult, new object[] { invoiceLineId });   
                if (invoiceLineDTO != null)
                {
                    return Ok(invoiceLineDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/invoiceLine
        public IHttpActionResult PostInvoiceLine(InvoiceLineDTO invoiceLineDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, invoiceLineDTO))
                    {
                        return CreatedAtRoute("DefaultApi", new { invoiceLineDTO.InvoiceLineId }, invoiceLineDTO);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/invoiceLine/1
        [Route("api/invoiceLine/{invoiceLineId}")]
        public IHttpActionResult PutInvoiceLine(int invoiceLineId, InvoiceLineDTO invoiceLineDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, invoiceLineDTO))
                    {
                        return Ok(invoiceLineDTO);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        #endregion Methods REST
    }
}
