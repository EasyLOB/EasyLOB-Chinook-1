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
    public class AlbumController : BaseODataControllerApplication<AlbumDTO, Album>
    {
        #region Methods

        public AlbumController(IChinookGenericApplicationDTO<AlbumDTO, Album> application)
            : base("Album")
        {
            Application = application;

        }

        // GET: /Album?$...
        [HttpGet]
        public IQueryable<AlbumDTO> Get(ODataQueryOptions queryOptions)
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

                    return query as IQueryable<AlbumDTO>;
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PUT: /Album(1)
        [HttpPut]
        public IHttpActionResult Put([FromODataUri] int key, AlbumDTO album)
        {
            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Update(OperationResult, album);

                        return Updated(album);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // POST: /Album
        [HttpPost]
        public IHttpActionResult Post(AlbumDTO album)
        {
            try
            {
                if (IsCreate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Application.Create(OperationResult, album);

                        return Created(album);
                    }
                }
            }
            catch (Exception exception)
            {
                OperationResult.ParseException(exception);
            }

            throw ODataHelper.OperationResultResponseException(Request, OperationResult);
        }

        // PATCH: /Album(1)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<AlbumDTO> patch)
        {
            AlbumDTO albumDTO = null;

            try
            {
                if (IsUpdate(OperationResult))
                {
                    if (ValidateModelState(OperationResult))
                    {
                        Album album = Application.UnitOfWork.GetRepository<Album>().GetById(key);
                        if (album != null)
                        {
                            albumDTO = new AlbumDTO(album);
                            patch.Patch(albumDTO);
                            LibraryHelper.Clone<Album>((Album)albumDTO.ToData(), album, null);

                            Application.UnitOfWork.Save(OperationResult);

                            return Updated(albumDTO);
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

        // DELETE: /Album(1)
        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            try
            {
                if (IsDelete(OperationResult))
                {
                    Album album = Application.UnitOfWork.GetRepository<Album>().GetById(key);
                    if (album != null)
                    {
                        Application.Delete(OperationResult, new AlbumDTO(album));

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