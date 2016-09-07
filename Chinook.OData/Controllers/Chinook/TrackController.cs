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
    public class TrackController : BaseODataControllerApplication<TrackDTO, Track>
    {
        #region Methods

        public TrackController(IChinookGenericApplicationDTO<TrackDTO, Track> application)
            : base("Track")
        {
            Application = application;

        }

        // GET: /Track?$...
        [HttpGet]
        public IQueryable<TrackDTO> Get(ODataQueryOptions queryOptions)
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

                    return query as IQueryable<TrackDTO>;
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PUT: /Track(1)
        [HttpPut]
        public IHttpActionResult Put([FromODataUri] int key, TrackDTO track)
        {
            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Update(OperationResult, track);

                        return Updated(track);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // POST: /Track
        [HttpPost]
        public IHttpActionResult Post(TrackDTO track)
        {
            try
            {
                if (IsCreate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Create(OperationResult, track);

                        return Created(track);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PATCH: /Track(1)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<TrackDTO> patch)
        {
            TrackDTO trackDTO = null;

            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Track track = Application.UnitOfWork.GetRepository<Track>().GetById(key);
                        if (track != null)
                        {
                            trackDTO = new TrackDTO(track);
                            patch.Patch(trackDTO);
                            LibraryHelper.Clone<Track>((Track)trackDTO.ToData(), track, null);

                            Application.UnitOfWork.Save(OperationResult);

                            return Updated(trackDTO);
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

        // DELETE: /Track(1)
        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            try
            {
                if (IsDelete(OperationResult))
                {
                    Track track = Application.UnitOfWork.GetRepository<Track>().GetById(key);
                    if (track != null)
                    {
                        Application.Delete(OperationResult, new TrackDTO(track));

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