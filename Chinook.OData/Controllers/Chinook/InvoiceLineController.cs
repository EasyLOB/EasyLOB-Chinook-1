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
    public class InvoiceLineController : BaseODataControllerApplication<InvoiceLineDTO, InvoiceLine>
    {
        #region Methods

        public InvoiceLineController(IChinookGenericApplicationDTO<InvoiceLineDTO, InvoiceLine> application)
            : base("InvoiceLine")
        {
            Application = application;

        }

        // GET: /InvoiceLine?$...
        [HttpGet]
        public IQueryable<InvoiceLineDTO> Get(ODataQueryOptions queryOptions)
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

                    return query as IQueryable<InvoiceLineDTO>;
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PUT: /InvoiceLine(1)
        [HttpPut]
        public IHttpActionResult Put([FromODataUri] int key, InvoiceLineDTO invoiceLine)
        {
            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Update(OperationResult, invoiceLine);

                        return Updated(invoiceLine);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // POST: /InvoiceLine
        [HttpPost]
        public IHttpActionResult Post(InvoiceLineDTO invoiceLine)
        {
            try
            {
                if (IsCreate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Create(OperationResult, invoiceLine);

                        return Created(invoiceLine);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PATCH: /InvoiceLine(1)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<InvoiceLineDTO> patch)
        {
            InvoiceLineDTO invoiceLineDTO = null;

            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        InvoiceLine invoiceLine = Application.UnitOfWork.GetRepository<InvoiceLine>().GetById(key);
                        if (invoiceLine != null)
                        {
                            invoiceLineDTO = new InvoiceLineDTO(invoiceLine);
                            patch.Patch(invoiceLineDTO);
                            LibraryHelper.Clone<InvoiceLine>((InvoiceLine)invoiceLineDTO.ToData(), invoiceLine, null);

                            Application.UnitOfWork.Save(OperationResult);

                            return Updated(invoiceLineDTO);
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

        // DELETE: /InvoiceLine(1)
        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            try
            {
                if (IsDelete(OperationResult))
                {
                    InvoiceLine invoiceLine = Application.UnitOfWork.GetRepository<InvoiceLine>().GetById(key);
                    if (invoiceLine != null)
                    {
                        Application.Delete(OperationResult, new InvoiceLineDTO(invoiceLine));

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