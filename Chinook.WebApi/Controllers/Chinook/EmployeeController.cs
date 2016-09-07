using System;
using System.Collections.Generic;
using System.Web.Http;
using Chinook.Application;
using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Library.WebApi;

namespace Chinook.WebApi
{
    public class EmployeeController : BaseApiControllerApplication<EmployeeDTO, Employee>
    {
        #region Methods

        public EmployeeController(IChinookGenericApplicationDTO<EmployeeDTO, Employee> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/employee/1
        [Route("api/employee/{employeeId}")]
        public IHttpActionResult DeleteEmployee(int employeeId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                EmployeeDTO employeeDTO = Application.GetById(operationResult, new object[] { employeeId });    
                if (employeeDTO != null)
                {
                    if (Application.Delete(operationResult, employeeDTO))
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

        // GET: api/employee
        public IHttpActionResult GetEmployees()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<EmployeeDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/employee/1
        [Route("api/employee/{employeeId}")]
        public IHttpActionResult GetEmployee(int employeeId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                EmployeeDTO employeeDTO = Application.GetById(operationResult, new object[] { employeeId });   
                if (employeeDTO != null)
                {
                    return Ok(employeeDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/employee
        public IHttpActionResult PostEmployee(EmployeeDTO employeeDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, employeeDTO))
                    {
                        return CreatedAtRoute("DefaultApi", new { employeeDTO.EmployeeId }, employeeDTO);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/employee/1
        [Route("api/employee/{employeeId}")]
        public IHttpActionResult PutEmployee(int employeeId, EmployeeDTO employeeDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, employeeDTO))
                    {
                        return Ok(employeeDTO);
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
