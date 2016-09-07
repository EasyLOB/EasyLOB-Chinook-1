using System;
using System.Collections.Generic;
using System.Web.Http;
using Chinook.Application;
using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Library.WebApi;

namespace Chinook.WebApi
{
    public class InvoiceController : BaseApiControllerApplication<InvoiceDTO, Invoice>
    {
        #region Methods

        public InvoiceController(IChinookGenericApplicationDTO<InvoiceDTO, Invoice> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/invoice/1
        [Route("api/invoice/{invoiceId}")]
        public IHttpActionResult DeleteInvoice(int invoiceId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                InvoiceDTO invoiceDTO = Application.GetById(operationResult, new object[] { invoiceId });    
                if (invoiceDTO != null)
                {
                    if (Application.Delete(operationResult, invoiceDTO))
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

        // GET: api/invoice
        public IHttpActionResult GetInvoices()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<InvoiceDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/invoice/1
        [Route("api/invoice/{invoiceId}")]
        public IHttpActionResult GetInvoice(int invoiceId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                InvoiceDTO invoiceDTO = Application.GetById(operationResult, new object[] { invoiceId });   
                if (invoiceDTO != null)
                {
                    return Ok(invoiceDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/invoice
        public IHttpActionResult PostInvoice(InvoiceDTO invoiceDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, invoiceDTO))
                    {
                        return CreatedAtRoute("DefaultApi", new { invoiceDTO.InvoiceId }, invoiceDTO);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/invoice/1
        [Route("api/invoice/{invoiceId}")]
        public IHttpActionResult PutInvoice(int invoiceId, InvoiceDTO invoiceDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, invoiceDTO))
                    {
                        return Ok(invoiceDTO);
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
