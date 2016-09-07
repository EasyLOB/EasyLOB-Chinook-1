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
    public class InvoiceController : BaseODataControllerApplication<InvoiceDTO, Invoice>
    {
        #region Methods

        public InvoiceController(IChinookGenericApplicationDTO<InvoiceDTO, Invoice> application)
            : base("Invoice")
        {
            Application = application;

        }

        // GET: /Invoice?$...
        [HttpGet]
        public IQueryable<InvoiceDTO> Get(ODataQueryOptions queryOptions)
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

                    return query as IQueryable<InvoiceDTO>;
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PUT: /Invoice(1)
        [HttpPut]
        public IHttpActionResult Put([FromODataUri] int key, InvoiceDTO invoice)
        {
            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Update(OperationResult, invoice);

                        return Updated(invoice);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // POST: /Invoice
        [HttpPost]
        public IHttpActionResult Post(InvoiceDTO invoice)
        {
            try
            {
                if (IsCreate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Create(OperationResult, invoice);

                        return Created(invoice);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PATCH: /Invoice(1)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<InvoiceDTO> patch)
        {
            InvoiceDTO invoiceDTO = null;

            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Invoice invoice = Application.UnitOfWork.GetRepository<Invoice>().GetById(key);
                        if (invoice != null)
                        {
                            invoiceDTO = new InvoiceDTO(invoice);
                            patch.Patch(invoiceDTO);
                            LibraryHelper.Clone<Invoice>((Invoice)invoiceDTO.ToData(), invoice, null);

                            Application.UnitOfWork.Save(OperationResult);

                            return Updated(invoiceDTO);
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

        // DELETE: /Invoice(1)
        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            try
            {
                if (IsDelete(OperationResult))
                {
                    Invoice invoice = Application.UnitOfWork.GetRepository<Invoice>().GetById(key);
                    if (invoice != null)
                    {
                        Application.Delete(OperationResult, new InvoiceDTO(invoice));

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