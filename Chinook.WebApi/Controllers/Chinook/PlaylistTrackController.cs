using System;
using System.Collections.Generic;
using System.Web.Http;
using Chinook.Application;
using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Library.WebApi;

namespace Chinook.WebApi
{
    public class PlaylistTrackController : BaseApiControllerApplication<PlaylistTrackDTO, PlaylistTrack>
    {
        #region Methods

        public PlaylistTrackController(IChinookGenericApplicationDTO<PlaylistTrackDTO, PlaylistTrack> application)
        {
            Application = application;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/playlistTrack/1
        [Route("api/playlistTrack/{playlistId}/{trackId}")]
        public IHttpActionResult DeletePlaylistTrack(int playlistId, int trackId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                PlaylistTrackDTO playlistTrackDTO = Application.GetById(operationResult, new object[] { playlistId, trackId });    
                if (playlistTrackDTO != null)
                {
                    if (Application.Delete(operationResult, playlistTrackDTO))
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

        // GET: api/playlistTrack
        public IHttpActionResult GetPlaylistTracks()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                return Ok<IEnumerable<PlaylistTrackDTO>>(Application.SelectAll(operationResult));
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/playlistTrack/1
        [Route("api/playlistTrack/{playlistId}/{trackId}")]
        public IHttpActionResult GetPlaylistTrack(int playlistId, int trackId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                PlaylistTrackDTO playlistTrackDTO = Application.GetById(operationResult, new object[] { playlistId, trackId });   
                if (playlistTrackDTO != null)
                {
                    return Ok(playlistTrackDTO);
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/playlistTrack
        public IHttpActionResult PostPlaylistTrack(PlaylistTrackDTO playlistTrackDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, playlistTrackDTO))
                    {
                        return CreatedAtRoute("DefaultApi", new { playlistTrackDTO.PlaylistId, playlistTrackDTO.TrackId }, playlistTrackDTO);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/playlistTrack/1
        [Route("api/playlistTrack/{playlistId}/{trackId}")]
        public IHttpActionResult PutPlaylistTrack(int playlistId, int trackId, PlaylistTrackDTO playlistTrackDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (ValidateModelState(operationResult, Application.Repository))
                {
                    if (Application.Create(operationResult, playlistTrackDTO))
                    {
                        return Ok(playlistTrackDTO);
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
