using System;
using System.Collections.Generic;
using System.Web.Http;
using Chinook.Application;
using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Library.WebApi;

namespace Chinook.WebApi
{
    public class CustomerController : BaseApiControllerApplication<CustomerDTO, Customer>
    {
        #region Methods

        public CustomerController(IChinookGenericApplicationDTO<CustomerDTO, Customer> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/customer/1
        [Route("api/customer/{customerId}")]
        public IHttpActionResult DeleteCustomer(int customerId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                CustomerDTO customerDTO = Application.GetById(operationResult, new object[] { customerId });    
                if (customerDTO != null)
                {
                    if (Application.Delete(operationResult, customerDTO))
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

        // GET: api/customer
        public IHttpActionResult GetCustomers()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<CustomerDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/customer/1
        [Route("api/customer/{customerId}")]
        public IHttpActionResult GetCustomer(int customerId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                CustomerDTO customerDTO = Application.GetById(operationResult, new object[] { customerId });   
                if (customerDTO != null)
                {
                    return Ok(customerDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/customer
        public IHttpActionResult PostCustomer(CustomerDTO customerDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, customerDTO))
                    {
                        return CreatedAtRoute("DefaultApi", new { customerDTO.CustomerId }, customerDTO);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/customer/1
        [Route("api/customer/{customerId}")]
        public IHttpActionResult PutCustomer(int customerId, CustomerDTO customerDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, customerDTO))
                    {
                        return Ok(customerDTO);
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
