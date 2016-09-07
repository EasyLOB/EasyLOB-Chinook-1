using System;
using System.Collections.Generic;
using System.Web.Http;
using Chinook.Application;
using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Library.WebApi;

namespace Chinook.WebApi
{
    public class TrackController : BaseApiControllerApplication<TrackDTO, Track>
    {
        #region Methods

        public TrackController(IChinookGenericApplicationDTO<TrackDTO, Track> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/track/1
        [Route("api/track/{trackId}")]
        public IHttpActionResult DeleteTrack(int trackId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                TrackDTO trackDTO = Application.GetById(operationResult, new object[] { trackId });    
                if (trackDTO != null)
                {
                    if (Application.Delete(operationResult, trackDTO))
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

        // GET: api/track
        public IHttpActionResult GetTracks()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<TrackDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/track/1
        [Route("api/track/{trackId}")]
        public IHttpActionResult GetTrack(int trackId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                TrackDTO trackDTO = Application.GetById(operationResult, new object[] { trackId });   
                if (trackDTO != null)
                {
                    return Ok(trackDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/track
        public IHttpActionResult PostTrack(TrackDTO trackDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, trackDTO))
                    {
                        return CreatedAtRoute("DefaultApi", new { trackDTO.TrackId }, trackDTO);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/track/1
        [Route("api/track/{trackId}")]
        public IHttpActionResult PutTrack(int trackId, TrackDTO trackDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, trackDTO))
                    {
                        return Ok(trackDTO);
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
