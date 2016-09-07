using System;
using System.Collections.Generic;
using System.Web.Http;
using Chinook.Application;
using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Library.WebApi;

namespace Chinook.WebApi
{
    public class CustomerDocumentController : BaseApiControllerApplication<CustomerDocumentDTO, CustomerDocument>
    {
        #region Methods

        public CustomerDocumentController(IChinookGenericApplicationDTO<CustomerDocumentDTO, CustomerDocument> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/customerDocument/1
        [Route("api/customerDocument/{customerDocumentId}")]
        public IHttpActionResult DeleteCustomerDocument(int customerDocumentId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                CustomerDocumentDTO customerDocumentDTO = Application.GetById(operationResult, new object[] { customerDocumentId });    
                if (customerDocumentDTO != null)
                {
                    if (Application.Delete(operationResult, customerDocumentDTO))
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

        // GET: api/customerDocument
        public IHttpActionResult GetCustomerDocuments()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<CustomerDocumentDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/customerDocument/1
        [Route("api/customerDocument/{customerDocumentId}")]
        public IHttpActionResult GetCustomerDocument(int customerDocumentId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                CustomerDocumentDTO customerDocumentDTO = Application.GetById(operationResult, new object[] { customerDocumentId });   
                if (customerDocumentDTO != null)
                {
                    return Ok(customerDocumentDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/customerDocument
        public IHttpActionResult PostCustomerDocument(CustomerDocumentDTO customerDocumentDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, customerDocumentDTO))
                    {
                        return CreatedAtRoute("DefaultApi", new { customerDocumentDTO.CustomerDocumentId }, customerDocumentDTO);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/customerDocument/1
        [Route("api/customerDocument/{customerDocumentId}")]
        public IHttpActionResult PutCustomerDocument(int customerDocumentId, CustomerDocumentDTO customerDocumentDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, customerDocumentDTO))
                    {
                        return Ok(customerDocumentDTO);
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
