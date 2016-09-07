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
    public class GenreController : BaseODataControllerApplication<GenreDTO, Genre>
    {
        #region Methods

        public GenreController(IChinookGenericApplicationDTO<GenreDTO, Genre> application)
            : base("Genre")
        {
            Application = application;

        }

        // GET: /Genre?$...
        [HttpGet]
        public IQueryable<GenreDTO> Get(ODataQueryOptions queryOptions)
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

                    return query as IQueryable<GenreDTO>;
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PUT: /Genre(1)
        [HttpPut]
        public IHttpActionResult Put([FromODataUri] int key, GenreDTO genre)
        {
            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Update(OperationResult, genre);

                        return Updated(genre);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // POST: /Genre
        [HttpPost]
        public IHttpActionResult Post(GenreDTO genre)
        {
            try
            {
                if (IsCreate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Create(OperationResult, genre);

                        return Created(genre);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PATCH: /Genre(1)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<GenreDTO> patch)
        {
            GenreDTO genreDTO = null;

            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Genre genre = Application.UnitOfWork.GetRepository<Genre>().GetById(key);
                        if (genre != null)
                        {
                            genreDTO = new GenreDTO(genre);
                            patch.Patch(genreDTO);
                            LibraryHelper.Clone<Genre>((Genre)genreDTO.ToData(), genre, null);

                            Application.UnitOfWork.Save(OperationResult);

                            return Updated(genreDTO);
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

        // DELETE: /Genre(1)
        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            try
            {
                if (IsDelete(OperationResult))
                {
                    Genre genre = Application.UnitOfWork.GetRepository<Genre>().GetById(key);
                    if (genre != null)
                    {
                        Application.Delete(OperationResult, new GenreDTO(genre));

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