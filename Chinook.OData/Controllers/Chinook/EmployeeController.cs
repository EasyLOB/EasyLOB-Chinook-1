using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using Chinook.Application;
using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Library.WebApi;

namespace Chinook.OData
{
    public class EmployeeController : BaseODataControllerApplication<EmployeeDTO, Employee>
    {
        #region Methods

        public EmployeeController(IChinookGenericApplicationDTO<EmployeeDTO, Employee> application)
            : base("Employee")
        {
            Application = application;

        }

        // GET: /Employee?$...
        [HttpGet]
        public IQueryable<EmployeeDTO> Get(ODataQueryOptions queryOptions)
        {
            IQueryable query = null;

            try
            {
                if (IsSearch(OperationResult))
                {
                    var settings = new ODataValidationSettings()
                    {
                        AllowedFunctions = AllowedFunctions.AllMathFunctions
                    };
                    queryOptions.Validate(settings);

                    query = queryOptions.ApplyTo(Application.QueryDTO);

                    return query as IQueryable<EmployeeDTO>;
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PUT: /Employee(1)
        [HttpPut]
        public IHttpActionResult Put([FromODataUri] int key, EmployeeDTO employee)
        {
            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Update(OperationResult, employee);

                        return Updated(employee);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // POST: /Employee
        [HttpPost]
        public IHttpActionResult Post(EmployeeDTO employee)
        {
            try
            {
                if (IsCreate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Create(OperationResult, employee);

                        return Created(employee);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PATCH: /Employee(1)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<EmployeeDTO> patch)
        {
            EmployeeDTO employeeDTO = null;

            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Employee employee = Application.UnitOfWork.GetRepository<Employee>().GetById(key);
                        if (employee != null)
                        {
                            employeeDTO = new EmployeeDTO(employee);
                            patch.Patch(employeeDTO);
                            LibraryHelper.Clone<Employee>((Employee)employeeDTO.ToData(), employee, null);

                            Application.UnitOfWork.Save(OperationResult);

                            return Updated(employeeDTO);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // DELETE: /Employee(1)
        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            try
            {
                if (IsDelete(OperationResult))
                {
                    Employee employee = Application.UnitOfWork.GetRepository<Employee>().GetById(key);
                    if (employee != null)
                    {
                        Application.Delete(OperationResult, new EmployeeDTO(employee));

                        return StatusCode(HttpStatusCode.NoContent);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //?.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion Methods
    }
}