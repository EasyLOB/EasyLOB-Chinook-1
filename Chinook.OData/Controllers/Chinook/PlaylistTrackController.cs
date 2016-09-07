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
    public class PlaylistTrackController : BaseODataControllerApplication<PlaylistTrackDTO, PlaylistTrack>
    {
        #region Methods

        public PlaylistTrackController(IChinookGenericApplicationDTO<PlaylistTrackDTO, PlaylistTrack> application)
            : base("PlaylistTrack")
        {
            Application = application;

        }

        // GET: /PlaylistTrack?$...
        [HttpGet]
        public IQueryable<PlaylistTrackDTO> Get(ODataQueryOptions queryOptions)
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

                    return query as IQueryable<PlaylistTrackDTO>;
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PUT: /PlaylistTrack(1)
        [HttpPut]
        public IHttpActionResult Put([FromODataUri] int key, PlaylistTrackDTO playlistTrack)
        {
            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Update(OperationResult, playlistTrack);

                        return Updated(playlistTrack);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // POST: /PlaylistTrack
        [HttpPost]
        public IHttpActionResult Post(PlaylistTrackDTO playlistTrack)
        {
            try
            {
                if (IsCreate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Create(OperationResult, playlistTrack);

                        return Created(playlistTrack);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PATCH: /PlaylistTrack(1)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<PlaylistTrackDTO> patch)
        {
            PlaylistTrackDTO playlistTrackDTO = null;

            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        PlaylistTrack playlistTrack = Application.UnitOfWork.GetRepository<PlaylistTrack>().GetById(key);
                        if (playlistTrack != null)
                        {
                            playlistTrackDTO = new PlaylistTrackDTO(playlistTrack);
                            patch.Patch(playlistTrackDTO);
                            LibraryHelper.Clone<PlaylistTrack>((PlaylistTrack)playlistTrackDTO.ToData(), playlistTrack, null);

                            Application.UnitOfWork.Save(OperationResult);

                            return Updated(playlistTrackDTO);
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

        // DELETE: /PlaylistTrack(1)
        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            try
            {
                if (IsDelete(OperationResult))
                {
                    PlaylistTrack playlistTrack = Application.UnitOfWork.GetRepository<PlaylistTrack>().GetById(key);
                    if (playlistTrack != null)
                    {
                        Application.Delete(OperationResult, new PlaylistTrackDTO(playlistTrack));

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