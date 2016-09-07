using System;
using System.Collections.Generic;
using System.Web.Http;
using Chinook.Data;
using Chinook.Persistence;
using EasyLOB.Data;
using EasyLOB.Library;
using EasyLOB.Library.WebApi;

namespace Chinook.WebApi
{
    public class GenreController : BaseApiControllerPersistence<Genre>
    {
        #region Methods

        public GenreController(IChinookUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        #endregion Methods

        #region Methods CRUD

        // DELETE: api/genre/1
        [Route("api/genre/{genreId}")]
        public IHttpActionResult DeleteGenre(int genreId)
        {
            ZOperationResult operationResult = new ZOperationResult();
            
            try
            {
                if (IsDelete(operationResult))
                {
                    Genre genre = Repository.GetById(new object[] { genreId });       
                    if (genre != null)
                    {
                        if (Repository.Delete(operationResult, genre))
                        {
                            if (UnitOfWork.Save(operationResult))
                            {        
                                GenreDTO genreDTO = new GenreDTO(genre);
            
                                return Ok();
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/genre
        public IHttpActionResult GetGenres()
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (IsSearch(operationResult))
                {
                    return Ok<IEnumerable<GenreDTO>>(ZDTOHelper<GenreDTO, Genre>.ToDTOList(Repository.SelectAll()));
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // GET: api/genre/1
        [Route("api/genre/{genreId}")]
        public IHttpActionResult GetGenre(int genreId)
        {
            ZOperationResult operationResult = new ZOperationResult();

            try
            {
                if (IsRead(operationResult))
                {
                    Genre genre = Repository.GetById(new object[] { genreId });        
                    if (genre != null)
                    {
                        GenreDTO genreDTO = new GenreDTO(genre);
    
                        return Ok(genreDTO);
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // POST: api/genre
        public IHttpActionResult PostGenre(GenreDTO genreDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();
    
            try
            {
                if (IsCreate(operationResult))
                {
                    if (ValidateModelState(operationResult, Repository))
                    {
                        Genre genre = (Genre)genreDTO.ToData();
                        if (Repository.Create(operationResult, genre))
                        {
                            if (UnitOfWork.Save(operationResult))
                            {
                                genreDTO = new GenreDTO(genre);
                                return CreatedAtRoute("DefaultApi", new { genreDTO.GenreId }, genreDTO);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }

            return new OperationResultActionResult(Request, operationResult);
        }

        // PUT: api/genre/1
        [Route("api/genre/{genreId}")]
        public IHttpActionResult PutGenre(int genreId, GenreDTO genreDTO)
        {
            ZOperationResult operationResult = new ZOperationResult();
    
            try
            {
                if (IsUpdate(operationResult))
                {
                    if (ValidateModelState(operationResult, Repository))
                    {
                        Genre genre = (Genre)genreDTO.ToData();
                        if (Repository.Update(operationResult, genre))
                        {
                            if (UnitOfWork.Save(operationResult))
                            {
                                genreDTO = new GenreDTO(genre);
        
                                return Ok(genreDTO);
                            }
                        }
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
