using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Library.WebApi;

// Install-Package RestSharp

namespace Chinook.WebApiClient
{
    internal partial class Program
    {
        private static void RunAsyncRestSharpAlbum()
        {
            bool ok = true;

            Console.WriteLine("\nWeb API Client - RestSharp - Album");

            RestClient client = new RestClient(WebApiUrl);
            //client.Authenticator = new HttpBasicAuthenticator(userName, password);

            // Token

            Token token = GetToken();
            if (String.IsNullOrEmpty(token.Error))
            {
                client.AddDefaultHeader("Authorization", String.Format("Bearer {0}", token.AccessToken));
            }

            // HTTP GET ALL

            if (ok)
            {
                Console.WriteLine("\nGET ALL");

                var requestGetAll = new RestRequest("api/album", Method.GET) { RequestFormat = DataFormat.Json };

                //var responseGetAll = client.Execute<List<Album>>(requestGetAll); // .Data
                var responseGetAll = client.Execute(requestGetAll);
                Console.WriteLine("Status: {0}", responseGetAll.StatusCode.ToString());
                if (responseGetAll.StatusCode == HttpStatusCode.OK)
                {
                    //List<Album> albums = responseGetAll.Data; // .Data
                    List<AlbumDTO> albums = JsonConvert.DeserializeObject<List<AlbumDTO>>(responseGetAll.Content);
                    foreach (AlbumDTO x in albums)
                    {
                        Console.WriteLine("{0}:{1}", x.AlbumId, x.Title);
                    }
                }
                else if (responseGetAll.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ZOperationResult operationResult = DeserializeZOperationResult(responseGetAll.Content);
                    Console.WriteLine(operationResult.Text);

                    ok = false;
                }
                else
                {
                    Console.WriteLine("Status Code: " + responseGetAll.StatusCode.ToString());
                    Console.WriteLine("Content: " + responseGetAll.Content);

                    ok = false;
                }
            }

            // HTTP GET

            if (ok)
            {
                Console.WriteLine("\nGET");

                var requestGet = new RestRequest("api/album/{id}", Method.GET);
                requestGet.AddUrlSegment("id", "1");

                //var responseGet = client.Execute<Album>(requestGet); // .Data
                var responseGet = client.Execute(requestGet);
                Console.WriteLine("Status: {0}", responseGet.StatusCode.ToString());
                if (responseGet.StatusCode == HttpStatusCode.OK)
                {
                    //Album albumGet = responseGet.Data; // .Data
                    AlbumDTO albumDTOGet = JsonConvert.DeserializeObject<AlbumDTO>(responseGet.Content);

                    Console.WriteLine("{0}:{1}", albumDTOGet.AlbumId, albumDTOGet.Title);
                }
                else if (responseGet.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ZOperationResult operationResult = DeserializeZOperationResult(responseGet.Content);
                    Console.WriteLine(operationResult.Text);

                    ok = false;
                }
                else
                {
                    Console.WriteLine("Status Code: " + responseGet.StatusCode.ToString());
                    Console.WriteLine("Content: " + responseGet.Content);

                    ok = false;
                }
            }

            AlbumDTO albumDTO = new AlbumDTO();

            // HTTP POST

            if (ok)
            {
                Console.WriteLine("\nPOST");

                var requestPost = new RestRequest("api/album", Method.POST);
                requestPost.RequestFormat = DataFormat.Json;
                albumDTO = new AlbumDTO() { AlbumId = 0, Title = "Album REST POST", ArtistId = 1 };
                requestPost.AddBody(albumDTO);

                //var responsePost = client.Execute<Album>(requestPost); // .Data
                var responsePost = client.Execute(requestPost);
                Console.WriteLine("Status: {0}", responsePost.StatusCode.ToString());
                if (responsePost.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    //albumDTO = responsePost.Data; // .Data
                    albumDTO = JsonConvert.DeserializeObject<AlbumDTO>(responsePost.Content);

                    Console.WriteLine("{0}:{1}", albumDTO.AlbumId, albumDTO.Title);
                }
                else if (responsePost.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ZOperationResult operationResult = DeserializeZOperationResult(responsePost.Content);
                    Console.WriteLine(operationResult.Text);

                    ok = false;
                }
                else
                {
                    Console.WriteLine("Status Code: " + responsePost.StatusCode.ToString());
                    Console.WriteLine("Content: " + responsePost.Content);

                    ok = false;
                }
            }

            // HTTP PUT

            if (ok)
            {
                Console.WriteLine("\nPUT");

                albumDTO.Title = "Album REST PUT";

                var requestPut = new RestRequest("api/album/{albumId}", Method.PUT);
                requestPut.AddUrlSegment("albumId", albumDTO.AlbumId.ToString());
                requestPut.RequestFormat = DataFormat.Json;
                requestPut.AddBody(albumDTO);

                var responsePut = client.Execute(requestPut);
                Console.WriteLine("Status: {0}", responsePut.StatusCode.ToString());
                if (responsePut.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //albumDTO = responsePost.Data; // .Data
                    albumDTO = JsonConvert.DeserializeObject<AlbumDTO>(responsePut.Content);

                    Console.WriteLine("{0}:{1}", albumDTO.AlbumId, albumDTO.Title);
                }
                else if (responsePut.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ZOperationResult operationResult = DeserializeZOperationResult(responsePut.Content);
                    Console.WriteLine(operationResult.Text);

                    ok = false;
                }
                else
                {
                    Console.WriteLine("Status Code: " + responsePut.StatusCode.ToString());
                    Console.WriteLine("Content: " + responsePut.Content);

                    ok = false;
                }
            }

            // HTTP DELETE

            if (ok)
            {
                Console.WriteLine("\nDELETE");

                var requestDelete = new RestRequest("api/album/{albumId}", Method.DELETE);
                requestDelete.AddUrlSegment("albumId", albumDTO.AlbumId.ToString());

                //var responseDelete = client.Execute<Album>(requestDelete); // .Data
                var responseDelete = client.Execute(requestDelete);
                Console.WriteLine("Status: {0}", responseDelete.StatusCode.ToString());
                if (responseDelete.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("OK");
                }
                else if (responseDelete.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ZOperationResult operationResult = DeserializeZOperationResult(responseDelete.Content);
                    Console.WriteLine(operationResult.Text);

                    ok = false;
                }
                else
                {
                    Console.WriteLine("Status Code: " + responseDelete.StatusCode.ToString());
                    Console.WriteLine("Content: " + responseDelete.Content);

                    ok = false;
                }
            }
        }

        private static void RunAsyncRestSharpGenre()
        {
            bool ok = true;

            Console.WriteLine("\nWeb API Client - RestSharp - Genre");

            RestClient client = new RestClient(WebApiUrl);
            //client.Authenticator = new HttpBasicAuthenticator(userName, password);

            // Token

            Token token = GetToken();
            if (String.IsNullOrEmpty(token.Error))
            {
                client.AddDefaultHeader("Authorization", String.Format("Bearer {0}", token.AccessToken));
            }

            // HTTP GET ALL

            if (ok)
            {
                Console.WriteLine("\nGET ALL");

                var requestGetAll = new RestRequest("api/genre", Method.GET) { RequestFormat = DataFormat.Json };

                //var responseGetAll = client.Execute<List<Genre>>(requestGetAll); // .Data
                var responseGetAll = client.Execute(requestGetAll);
                Console.WriteLine("Status: {0}", responseGetAll.StatusCode.ToString());
                if (responseGetAll.StatusCode == HttpStatusCode.OK)
                {
                    //List<Genre> genres = responseGetAll.Data; // .Data
                    List<GenreDTO> genres = JsonConvert.DeserializeObject<List<GenreDTO>>(responseGetAll.Content);
                    foreach (GenreDTO x in genres)
                    {
                        Console.WriteLine("{0}:{1}", x.GenreId, x.Name);
                    }
                }
                else if (responseGetAll.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ZOperationResult operationResult = DeserializeZOperationResult(responseGetAll.Content);
                    Console.WriteLine(operationResult.Text);

                    ok = false;
                }
                else
                {
                    Console.WriteLine("Status Code: " + responseGetAll.StatusCode.ToString());
                    Console.WriteLine("Content: " + responseGetAll.Content);

                    ok = false;
                }
            }

            // HTTP GET

            if (ok)
            {
                Console.WriteLine("\nGET");

                var requestGet = new RestRequest("api/genre/{id}", Method.GET);
                requestGet.AddUrlSegment("id", "1");

                //var responseGet = client.Execute<Genre>(requestGet); // .Data
                var responseGet = client.Execute(requestGet);
                Console.WriteLine("Status: {0}", responseGet.StatusCode.ToString());
                if (responseGet.StatusCode == HttpStatusCode.OK)
                {
                    //Genre genreGet = responseGet.Data; // .Data
                    GenreDTO genreDTOGet = JsonConvert.DeserializeObject<GenreDTO>(responseGet.Content);

                    Console.WriteLine("{0}:{1}", genreDTOGet.GenreId, genreDTOGet.Name);
                }
                else if (responseGet.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ZOperationResult operationResult = DeserializeZOperationResult(responseGet.Content);
                    Console.WriteLine(operationResult.Text);

                    ok = false;
                }
                else
                {
                    Console.WriteLine("Status Code: " + responseGet.StatusCode.ToString());
                    Console.WriteLine("Content: " + responseGet.Content);

                    ok = false;
                }
            }

            GenreDTO genreDTO = new GenreDTO();

            // HTTP POST

            if (ok)
            {
                Console.WriteLine("\nPOST");

                var requestPost = new RestRequest("api/genre", Method.POST);
                requestPost.RequestFormat = DataFormat.Json;
                genreDTO = new GenreDTO() { GenreId = 0, Name = "Genre REST POST" };
                requestPost.AddBody(genreDTO);

                //var responsePost = client.Execute<Genre>(requestPost); // .Data
                var responsePost = client.Execute(requestPost);
                Console.WriteLine("Status: {0}", responsePost.StatusCode.ToString());
                if (responsePost.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    //genreDTO = responsePost.Data; // .Data
                    genreDTO = JsonConvert.DeserializeObject<GenreDTO>(responsePost.Content);

                    Console.WriteLine("{0}:{1}", genreDTO.GenreId, genreDTO.Name);
                }
                else if (responsePost.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ZOperationResult operationResult = DeserializeZOperationResult(responsePost.Content);
                    Console.WriteLine(operationResult.Text);

                    ok = false;
                }
                else
                {
                    Console.WriteLine("Status Code: " + responsePost.StatusCode.ToString());
                    Console.WriteLine("Content: " + responsePost.Content);

                    ok = false;
                }
            }

            // HTTP PUT

            if (ok)
            {
                Console.WriteLine("\nPUT");

                genreDTO.Name = "Genre REST PUT";

                var requestPut = new RestRequest("api/genre/{genreId}", Method.PUT);
                requestPut.AddUrlSegment("genreId", genreDTO.GenreId.ToString());
                requestPut.RequestFormat = DataFormat.Json;
                requestPut.AddBody(genreDTO);

                var responsePut = client.Execute(requestPut);
                Console.WriteLine("Status: {0}", responsePut.StatusCode.ToString());
                if (responsePut.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //genreDTO = responsePost.Data; // .Data
                    genreDTO = JsonConvert.DeserializeObject<GenreDTO>(responsePut.Content);

                    Console.WriteLine("{0}:{1}", genreDTO.GenreId, genreDTO.Name);
                }
                else if (responsePut.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ZOperationResult operationResult = DeserializeZOperationResult(responsePut.Content);
                    Console.WriteLine(operationResult.Text);

                    ok = false;
                }
                else
                {
                    Console.WriteLine("Status Code: " + responsePut.StatusCode.ToString());
                    Console.WriteLine("Content: " + responsePut.Content);

                    ok = false;
                }
            }

            // HTTP DELETE

            if (ok)
            {
                Console.WriteLine("\nDELETE");

                var requestDelete = new RestRequest("api/genre/{genreId}", Method.DELETE);
                requestDelete.AddUrlSegment("genreId", genreDTO.GenreId.ToString());

                //var responseDelete = client.Execute<Genre>(requestDelete); // .Data
                var responseDelete = client.Execute(requestDelete);
                Console.WriteLine("Status: {0}", responseDelete.StatusCode.ToString());
                if (responseDelete.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine("OK");
                }
                else if (responseDelete.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ZOperationResult operationResult = DeserializeZOperationResult(responseDelete.Content);
                    Console.WriteLine(operationResult.Text);

                    ok = false;
                }
                else
                {
                    Console.WriteLine("Status Code: " + responseDelete.StatusCode.ToString());
                    Console.WriteLine("Content: " + responseDelete.Content);

                    ok = false;
                }
            }
        }
    }
}