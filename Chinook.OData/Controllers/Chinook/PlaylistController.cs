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
    public class PlaylistController : BaseODataControllerApplication<PlaylistDTO, Playlist>
    {
        #region Methods

        public PlaylistController(IChinookGenericApplicationDTO<PlaylistDTO, Playlist> application)
            : base("Playlist")
        {
            Application = application;

        }

        // GET: /Playlist?$...
        [HttpGet]
        public IQueryable<PlaylistDTO> Get(ODataQueryOptions queryOptions)
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

                    return query as IQueryable<PlaylistDTO>;
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PUT: /Playlist(1)
        [HttpPut]
        public IHttpActionResult Put([FromODataUri] int key, PlaylistDTO playlist)
        {
            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Update(OperationResult, playlist);

                        return Updated(playlist);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // POST: /Playlist
        [HttpPost]
        public IHttpActionResult Post(PlaylistDTO playlist)
        {
            try
            {
                if (IsCreate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Create(OperationResult, playlist);

                        return Created(playlist);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PATCH: /Playlist(1)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<PlaylistDTO> patch)
        {
            PlaylistDTO playlistDTO = null;

            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Playlist playlist = Application.UnitOfWork.GetRepository<Playlist>().GetById(key);
                        if (playlist != null)
                        {
                            playlistDTO = new PlaylistDTO(playlist);
                            patch.Patch(playlistDTO);
                            LibraryHelper.Clone<Playlist>((Playlist)playlistDTO.ToData(), playlist, null);

                            Application.UnitOfWork.Save(OperationResult);

                            return Updated(playlistDTO);
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

        // DELETE: /Playlist(1)
        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            try
            {
                if (IsDelete(OperationResult))
                {
                    Playlist playlist = Application.UnitOfWork.GetRepository<Playlist>().GetById(key);
                    if (playlist != null)
                    {
                        Application.Delete(OperationResult, new PlaylistDTO(playlist));

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