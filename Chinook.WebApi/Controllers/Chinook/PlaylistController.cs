using System;
using System.Collections.Generic;
using System.Web.Http;
using Chinook.Application;
using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Library.WebApi;

namespace Chinook.WebApi
{
    public class PlaylistController : BaseApiControllerApplication<PlaylistDTO, Playlist>
    {
        #region Methods

        public PlaylistController(IChinookGenericApplicationDTO<PlaylistDTO, Playlist> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/playlist/1
        [Route("api/playlist/{playlistId}")]
        public IHttpActionResult DeletePlaylist(int playlistId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                PlaylistDTO playlistDTO = Application.GetById(operationResult, new object[] { playlistId });    
                if (playlistDTO != null)
                {
                    if (Application.Delete(operationResult, playlistDTO))
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

        // GET: api/playlist
        public IHttpActionResult GetPlaylists()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<PlaylistDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/playlist/1
        [Route("api/playlist/{playlistId}")]
        public IHttpActionResult GetPlaylist(int playlistId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                PlaylistDTO playlistDTO = Application.GetById(operationResult, new object[] { playlistId });   
                if (playlistDTO != null)
                {
                    return Ok(playlistDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/playlist
        public IHttpActionResult PostPlaylist(PlaylistDTO playlistDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, playlistDTO))
                    {
                        return CreatedAtRoute("DefaultApi", new { playlistDTO.PlaylistId }, playlistDTO);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/playlist/1
        [Route("api/playlist/{playlistId}")]
        public IHttpActionResult PutPlaylist(int playlistId, PlaylistDTO playlistDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, playlistDTO))
                    {
                        return Ok(playlistDTO);
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
