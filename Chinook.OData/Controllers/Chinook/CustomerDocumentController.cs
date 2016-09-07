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
    public class CustomerDocumentController : BaseODataControllerApplication<CustomerDocumentDTO, CustomerDocument>
    {
        #region Methods

        public CustomerDocumentController(IChinookGenericApplicationDTO<CustomerDocumentDTO, CustomerDocument> application)
            : base("CustomerDocument")
        {
            Application = application;

        }

        // GET: /CustomerDocument?$...
        [HttpGet]
        public IQueryable<CustomerDocumentDTO> Get(ODataQueryOptions queryOptions)
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

                    return query as IQueryable<CustomerDocumentDTO>;
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PUT: /CustomerDocument(1)
        [HttpPut]
        public IHttpActionResult Put([FromODataUri] int key, CustomerDocumentDTO customerDocument)
        {
            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Update(OperationResult, customerDocument);

                        return Updated(customerDocument);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // POST: /CustomerDocument
        [HttpPost]
        public IHttpActionResult Post(CustomerDocumentDTO customerDocument)
        {
            try
            {
                if (IsCreate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Create(OperationResult, customerDocument);

                        return Created(customerDocument);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PATCH: /CustomerDocument(1)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<CustomerDocumentDTO> patch)
        {
            CustomerDocumentDTO customerDocumentDTO = null;

            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        CustomerDocument customerDocument = Application.UnitOfWork.GetRepository<CustomerDocument>().GetById(key);
                        if (customerDocument != null)
                        {
                            customerDocumentDTO = new CustomerDocumentDTO(customerDocument);
                            patch.Patch(customerDocumentDTO);
                            LibraryHelper.Clone<CustomerDocument>((CustomerDocument)customerDocumentDTO.ToData(), customerDocument, null);

                            Application.UnitOfWork.Save(OperationResult);

                            return Updated(customerDocumentDTO);
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

        // DELETE: /CustomerDocument(1)
        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            try
            {
                if (IsDelete(OperationResult))
                {
                    CustomerDocument customerDocument = Application.UnitOfWork.GetRepository<CustomerDocument>().GetById(key);
                    if (customerDocument != null)
                    {
                        Application.Delete(OperationResult, new CustomerDocumentDTO(customerDocument));

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