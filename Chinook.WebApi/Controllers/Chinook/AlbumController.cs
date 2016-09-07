using System;
using System.Collections.Generic;
using System.Web.Http;
using Chinook.Application;
using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Library.WebApi;

namespace Chinook.WebApi
{
    public class AlbumController : BaseApiControllerApplication<AlbumDTO, Album>
    {
        #region Methods

        public AlbumController(IChinookGenericApplicationDTO<AlbumDTO, Album> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/album/1
        [Route("api/album/{albumId}")]
        public IHttpActionResult DeleteAlbum(int albumId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                AlbumDTO albumDTO = Application.GetById(operationResult, new object[] { albumId });    
                if (albumDTO != null)
                {
                    if (Application.Delete(operationResult, albumDTO))
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

        // GET: api/album
        public IHttpActionResult GetAlbums()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<AlbumDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/album/1
        [Route("api/album/{albumId}")]
        public IHttpActionResult GetAlbum(int albumId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                AlbumDTO albumDTO = Application.GetById(operationResult, new object[] { albumId });   
                if (albumDTO != null)
                {
                    return Ok(albumDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/album
        public IHttpActionResult PostAlbum(AlbumDTO albumDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, albumDTO))
                    {
                        return CreatedAtRoute("DefaultApi", new { albumDTO.AlbumId }, albumDTO);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/album/1
        [Route("api/album/{albumId}")]
        public IHttpActionResult PutAlbum(int albumId, AlbumDTO albumDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, albumDTO))
                    {
                        return Ok(albumDTO);
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
