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
    public class ArtistController : BaseODataControllerApplication<ArtistDTO, Artist>
    {
        #region Methods

        public ArtistController(IChinookGenericApplicationDTO<ArtistDTO, Artist> application)
            : base("Artist")
        {
            Application = application;

        }

        // GET: /Artist?$...
        [HttpGet]
        public IQueryable<ArtistDTO> Get(ODataQueryOptions queryOptions)
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

                    return query as IQueryable<ArtistDTO>;
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PUT: /Artist(1)
        [HttpPut]
        public IHttpActionResult Put([FromODataUri] int key, ArtistDTO artist)
        {
            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Update(OperationResult, artist);

                        return Updated(artist);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // POST: /Artist
        [HttpPost]
        public IHttpActionResult Post(ArtistDTO artist)
        {
            try
            {
                if (IsCreate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Create(OperationResult, artist);

                        return Created(artist);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PATCH: /Artist(1)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<ArtistDTO> patch)
        {
            ArtistDTO artistDTO = null;

            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Artist artist = Application.UnitOfWork.GetRepository<Artist>().GetById(key);
                        if (artist != null)
                        {
                            artistDTO = new ArtistDTO(artist);
                            patch.Patch(artistDTO);
                            LibraryHelper.Clone<Artist>((Artist)artistDTO.ToData(), artist, null);

                            Application.UnitOfWork.Save(OperationResult);

                            return Updated(artistDTO);
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

        // DELETE: /Artist(1)
        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            try
            {
                if (IsDelete(OperationResult))
                {
                    Artist artist = Application.UnitOfWork.GetRepository<Artist>().GetById(key);
                    if (artist != null)
                    {
                        Application.Delete(OperationResult, new ArtistDTO(artist));

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