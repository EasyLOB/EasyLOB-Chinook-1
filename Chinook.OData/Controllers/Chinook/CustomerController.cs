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
    public class CustomerController : BaseODataControllerApplication<CustomerDTO, Customer>
    {
        #region Methods

        public CustomerController(IChinookGenericApplicationDTO<CustomerDTO, Customer> application)
            : base("Customer")
        {
            Application = application;

        }

        // GET: /Customer?$...
        [HttpGet]
        public IQueryable<CustomerDTO> Get(ODataQueryOptions queryOptions)
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

                    return query as IQueryable<CustomerDTO>;
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PUT: /Customer(1)
        [HttpPut]
        public IHttpActionResult Put([FromODataUri] int key, CustomerDTO customer)
        {
            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Update(OperationResult, customer);

                        return Updated(customer);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // POST: /Customer
        [HttpPost]
        public IHttpActionResult Post(CustomerDTO customer)
        {
            try
            {
                if (IsCreate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Create(OperationResult, customer);

                        return Created(customer);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PATCH: /Customer(1)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<CustomerDTO> patch)
        {
            CustomerDTO customerDTO = null;

            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Customer customer = Application.UnitOfWork.GetRepository<Customer>().GetById(key);
                        if (customer != null)
                        {
                            customerDTO = new CustomerDTO(customer);
                            patch.Patch(customerDTO);
                            LibraryHelper.Clone<Customer>((Customer)customerDTO.ToData(), customer, null);

                            Application.UnitOfWork.Save(OperationResult);

                            return Updated(customerDTO);
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

        // DELETE: /Customer(1)
        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            try
            {
                if (IsDelete(OperationResult))
                {
                    Customer customer = Application.UnitOfWork.GetRepository<Customer>().GetById(key);
                    if (customer != null)
                    {
                        Application.Delete(OperationResult, new CustomerDTO(customer));

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