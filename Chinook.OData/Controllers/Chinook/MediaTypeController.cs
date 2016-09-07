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
    public class MediaTypeController : BaseODataControllerApplication<MediaTypeDTO, MediaType>
    {
        #region Methods

        public MediaTypeController(IChinookGenericApplicationDTO<MediaTypeDTO, MediaType> application)
            : base("MediaType")
        {
            Application = application;

        }

        // GET: /MediaType?$...
        [HttpGet]
        public IQueryable<MediaTypeDTO> Get(ODataQueryOptions queryOptions)
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

                    return query as IQueryable<MediaTypeDTO>;
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PUT: /MediaType(1)
        [HttpPut]
        public IHttpActionResult Put([FromODataUri] int key, MediaTypeDTO mediaType)
        {
            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Update(OperationResult, mediaType);

                        return Updated(mediaType);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // POST: /MediaType
        [HttpPost]
        public IHttpActionResult Post(MediaTypeDTO mediaType)
        {
            try
            {
                if (IsCreate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Create(OperationResult, mediaType);

                        return Created(mediaType);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PATCH: /MediaType(1)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<MediaTypeDTO> patch)
        {
            MediaTypeDTO mediaTypeDTO = null;

            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        MediaType mediaType = Application.UnitOfWork.GetRepository<MediaType>().GetById(key);
                        if (mediaType != null)
                        {
                            mediaTypeDTO = new MediaTypeDTO(mediaType);
                            patch.Patch(mediaTypeDTO);
                            LibraryHelper.Clone<MediaType>((MediaType)mediaTypeDTO.ToData(), mediaType, null);

                            Application.UnitOfWork.Save(OperationResult);

                            return Updated(mediaTypeDTO);
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

        // DELETE: /MediaType(1)
        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            try
            {
                if (IsDelete(OperationResult))
                {
                    MediaType mediaType = Application.UnitOfWork.GetRepository<MediaType>().GetById(key);
                    if (mediaType != null)
                    {
                        Application.Delete(OperationResult, new MediaTypeDTO(mediaType));

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