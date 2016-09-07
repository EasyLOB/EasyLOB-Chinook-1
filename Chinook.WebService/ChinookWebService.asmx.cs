using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;
using EasyLOB.Library;
using EasyLOB.Library.WebService;
using Chinook.Data;

namespace Chinook.WebService
{
    /// <summary>
    /// Summary description for ChinookWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    // [System.Web.Script.Services.ScriptService]
    public class ChinookWebService : System.Web.Services.WebService
    {
        public AuthenticationHeader Authentication;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [SoapHeader("Authentication")]
        [WebMethod]
        public List<GenreDTO> SelectGenres()
        {
            List<GenreDTO> result = new List<GenreDTO>();

            if (Authentication.Username == "UserName" && Authentication.Password == "Password")
            {
                try
                {
                    GenreDTO genreDTO;

                    genreDTO = new GenreDTO();
                    genreDTO.GenreId = 1;
                    genreDTO.Name = "Genre 1";
                    result.Add(genreDTO);

                    genreDTO = new GenreDTO();
                    genreDTO.GenreId = 2;
                    genreDTO.Name = "Genre 2";
                    result.Add(genreDTO);

                    genreDTO = new GenreDTO();
                    genreDTO.GenreId = 3;
                    genreDTO.Name = "Genre 3";
                    result.Add(genreDTO);
                }
                catch (Exception exception)
                {
                    throw new SoapException(exception.ExceptionMessage(), SoapException.ServerFaultCode);
                }
            }
            else
            {
                throw new SoapException("Authentication", SoapException.ServerFaultCode);
            }

            return result;
        }
    }
}