using System;
using System.Collections.Generic;
using System.Web.Http;
using Chinook.Application;
using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Library.WebApi;

namespace Chinook.WebApi
{
    public class ArtistController : BaseApiControllerApplication<ArtistDTO, Artist>
    {
        #region Methods

        public ArtistController(IChinookGenericApplicationDTO<ArtistDTO, Artist> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/artist/1
        [Route("api/artist/{artistId}")]
        public IHttpActionResult DeleteArtist(int artistId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                ArtistDTO artistDTO = Application.GetById(operationResult, new object[] { artistId });    
                if (artistDTO != null)
                {
                    if (Application.Delete(operationResult, artistDTO))
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

        // GET: api/artist
        public IHttpActionResult GetArtists()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<ArtistDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/artist/1
        [Route("api/artist/{artistId}")]
        public IHttpActionResult GetArtist(int artistId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                ArtistDTO artistDTO = Application.GetById(operationResult, new object[] { artistId });   
                if (artistDTO != null)
                {
                    return Ok(artistDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/artist
        public IHttpActionResult PostArtist(ArtistDTO artistDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, artistDTO))
                    {
                        return CreatedAtRoute("DefaultApi", new { artistDTO.ArtistId }, artistDTO);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/artist/1
        [Route("api/artist/{artistId}")]
        public IHttpActionResult PutArtist(int artistId, ArtistDTO artistDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, artistDTO))
                    {
                        return Ok(artistDTO);
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
