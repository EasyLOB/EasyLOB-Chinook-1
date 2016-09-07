using System;
using System.Collections.Generic;
using System.Web.Http;
using Chinook.Application;
using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Library.WebApi;

namespace Chinook.WebApi
{
    public class MediaTypeController : BaseApiControllerApplication<MediaTypeDTO, MediaType>
    {
        #region Methods

        public MediaTypeController(IChinookGenericApplicationDTO<MediaTypeDTO, MediaType> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/mediaType/1
        [Route("api/mediaType/{mediaTypeId}")]
        public IHttpActionResult DeleteMediaType(int mediaTypeId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                MediaTypeDTO mediaTypeDTO = Application.GetById(operationResult, new object[] { mediaTypeId });    
                if (mediaTypeDTO != null)
                {
                    if (Application.Delete(operationResult, mediaTypeDTO))
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

        // GET: api/mediaType
        public IHttpActionResult GetMediaTypes()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<MediaTypeDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/mediaType/1
        [Route("api/mediaType/{mediaTypeId}")]
        public IHttpActionResult GetMediaType(int mediaTypeId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                MediaTypeDTO mediaTypeDTO = Application.GetById(operationResult, new object[] { mediaTypeId });   
                if (mediaTypeDTO != null)
                {
                    return Ok(mediaTypeDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/mediaType
        public IHttpActionResult PostMediaType(MediaTypeDTO mediaTypeDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, mediaTypeDTO))
                    {
                        return CreatedAtRoute("DefaultApi", new { mediaTypeDTO.MediaTypeId }, mediaTypeDTO);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/mediaType/1
        [Route("api/mediaType/{mediaTypeId}")]
        public IHttpActionResult PutMediaType(int mediaTypeId, MediaTypeDTO mediaTypeDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, mediaTypeDTO))
                    {
                        return Ok(mediaTypeDTO);
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
