using Newtonsoft.Json;
using System;
using EasyLOB.Library;

// Secure a Web API with Individual Accounts and Local Login in ASP.NET Web API 2.2
// http://www.asp.net/web-api/overview/security/individual-accounts-in-web-api

// Token Based Authentication Using ASP.Net Web API, OWIN and Identity With Entity Framework
// http://www.c-sharpcorner.com/UploadFile/ff2f08/token-based-authentication-using-Asp-Net-web-api-owin-and-i

namespace Chinook.WebApiClient
{
    internal partial class Program
    {
        private static string WebApiUrl = LibraryHelper.AppSettings<string>("WebAPI.Url");

        private static JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
        };

        private static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Chinook Web API Client\n");
                Console.WriteLine("<0> EXIT");
                Console.WriteLine("<1> Token Authentication HttpClient");
                Console.WriteLine("<2> Token Authentication RestSharp");
                Console.WriteLine("<3> HttpClient Album CRUD");
                Console.WriteLine("<4> HttpClient Genre CRUD");
                Console.WriteLine("<5> RestSharp Album CRUD");
                Console.WriteLine("<6> RestSharp Genre CRUD");
                Console.Write("\nChoose an option... ");

                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();

                switch (key.KeyChar) // <ENTER> = '\r'
                {
                    case ('0'):
                        exit = true;
                        break;

                    case ('1'):
                        TokenAuthenticationHttpClient();
                        break;

                    case ('2'):
                        TokenAuthenticationRestSharp();
                        break;

                    case ('3'):
                        RunAsyncHttpClientAlbum().Wait();
                        break;

                    case ('4'):
                        RunAsyncHttpClientGenre().Wait();
                        break;

                    case ('5'):
                        RunAsyncRestSharpAlbum();
                        break;

                    case ('6'):
                        RunAsyncRestSharpGenre();
                        break;
                }

                if (!exit)
                {
                    Console.Write("\nPress <KEY> to continue... ");
                    Console.ReadKey();
                }
            }
        }

        private static ZOperationResult DeserializeZOperationResult(string content)
        {
            //{
            //  "_data":null,
            //	"_statusCode":"",
            //	"_statusMessage":"",
            //	"_errorCode":"",
            //	"_errorMessage":"",
            //	"_operationErrors":[
            //		{
            //			"_errorCode":"",
            //			"_errorMessage":"Atividade-Operação \"Genre\"-\"Search\" não autorizada para o usuário \"\" ",
            //			"_errorMembers":[]
            //    }
            //	]
            //}

            //{
            //	"Data":null,
            //	"Ok":false,
            //	"StatusCode":"",
            //	"StatusMessage":"",
            //	"ErrorCode":"",
            //	"ErrorMessage":"",
            //	"Html":"<br /><label class="label label-danger">Erro: Atividade-Operação "Genre"-"Search" não autorizada para o usuário ""</label>",
            //	"OperationErrors":[
            //		{
            //			"ErrorCode":"",
            //			"ErrorMessage":"Atividade-Operação \"Genre\"-\"Search\" não autorizada para o usuário \"\" ",
            //			"ErrorMembers":[]
            //		}
            //	],
            //	"Text":"Erro: Atividade-Operação "Genre"-"Search" não autorizada para o usuário """
            //}

            return JsonConvert.DeserializeObject<ZOperationResult>(content.Replace("\"_", "\""), JsonSettings);
        }
    }
}