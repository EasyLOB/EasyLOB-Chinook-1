using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Chinook.Data;
using EasyLOB.Library;
using EasyLOB.Library.WebApi;

namespace Chinook.WebApiClient
{
    internal partial class Program
    {
        private static async Task RunAsyncHttpClientAlbum()
        {
            bool ok = true;

            Console.WriteLine("\nWeb API Client - HttpClient - Album");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WebApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Token

                Token token = GetToken();
                if (String.IsNullOrEmpty(token.Error))
                {
                    client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token.AccessToken));
                }

                // HTTP GET ALL

                if (ok)
                {
                    Console.WriteLine("\nGET ALL");

                    var responseGetAll = await client.GetAsync("api/album");
                    Console.WriteLine("Status: {0}", responseGetAll.StatusCode.ToString());
                    if (responseGetAll.IsSuccessStatusCode)
                    {
                        List<AlbumDTO> albums = await responseGetAll.Content.ReadAsAsync<List<AlbumDTO>>();

                        foreach (AlbumDTO x in albums)
                        {
                            Console.WriteLine("{0}:{1}", x.AlbumId, x.Title);
                        }
                    }
                    else if (responseGetAll.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ZOperationResult operationResult = DeserializeZOperationResult(responseGetAll.Content.ReadAsStringAsync().Result);
                        Console.WriteLine(operationResult.Text);

                        Console.WriteLine(responseGetAll.Content.ToString());

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

                    var responseGet = await client.GetAsync("api/album/1");
                    Console.WriteLine("Status: {0}", responseGet.StatusCode.ToString());
                    if (responseGet.IsSuccessStatusCode)
                    {
                        AlbumDTO albumDTOGet = await responseGet.Content.ReadAsAsync<AlbumDTO>();

                        Console.WriteLine("{0}:{1}", albumDTOGet.AlbumId, albumDTOGet.Title);
                    }
                    else if (responseGet.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ZOperationResult operationResult = DeserializeZOperationResult(responseGet.Content.ReadAsStringAsync().Result);
                        Console.WriteLine(operationResult.Text);

                        Console.WriteLine(responseGet.Content.ToString());

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

                    albumDTO = new AlbumDTO() { AlbumId = 0, Title = "Album REST POST", ArtistId = 1 };
                    var responsePost = await client.PostAsJsonAsync("api/album", albumDTO);
                    Console.WriteLine("Status: {0}", responsePost.StatusCode.ToString());
                    if (responsePost.IsSuccessStatusCode)
                    {
                        Uri uri = responsePost.Headers.Location;
                        albumDTO = responsePost.Content.ReadAsAsync<AlbumDTO>().Result;

                        Console.WriteLine("{0}:{1}", albumDTO.AlbumId, albumDTO.Title);
                    }
                    else if (responsePost.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ZOperationResult operationResult = DeserializeZOperationResult(responsePost.Content.ReadAsStringAsync().Result);
                        Console.WriteLine(operationResult.Text);

                        Console.WriteLine(responsePost.Content.ToString());

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

                    //var responsePut = await client.PutAsJsonAsync(uri.PathAndQuery.StringReplaceFirst("/", ""), albumDTO);
                    var responsePut = await client.PutAsJsonAsync("api/album/" + albumDTO.AlbumId.ToString(), albumDTO);
                    Console.WriteLine("Status: {0}", responsePut.StatusCode.ToString());
                    if (responsePut.IsSuccessStatusCode)
                    {
                        albumDTO = responsePut.Content.ReadAsAsync<AlbumDTO>().Result;

                        Console.WriteLine("{0}:{1}", albumDTO.AlbumId, albumDTO.Title);
                    }
                    else if (responsePut.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ZOperationResult operationResult = DeserializeZOperationResult(responsePut.Content.ReadAsStringAsync().Result);
                        Console.WriteLine(operationResult.Text);

                        Console.WriteLine(responsePut.Content.ToString());

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

                    //var responseDelete = await client.DeleteAsync(uri.PathAndQuery);
                    var responseDelete = await client.DeleteAsync("api/album/" + albumDTO.AlbumId.ToString());
                    Console.WriteLine("Status: {0}", responseDelete.StatusCode.ToString());
                    if (responseDelete.IsSuccessStatusCode)
                    {
                        Console.WriteLine("OK");
                    }
                    else if (responseDelete.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ZOperationResult operationResult = DeserializeZOperationResult(responseDelete.Content.ReadAsStringAsync().Result);
                        Console.WriteLine(operationResult.Text);

                        Console.WriteLine(responseDelete.Content.ToString());

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

        private static async Task RunAsyncHttpClientGenre()
        {
            bool ok = true;

            Console.WriteLine("\nWeb API Cliet - HttpClient - Genre");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WebApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Token

                Token token = GetToken();
                if (String.IsNullOrEmpty(token.Error))
                {
                    client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token.AccessToken));
                }

                // HTTP GET ALL

                if (ok)
                {
                    Console.WriteLine("\nGET ALL");

                    var responseGetAll = await client.GetAsync("api/genre");
                    Console.WriteLine("Status: {0}", responseGetAll.StatusCode.ToString());
                    if (responseGetAll.IsSuccessStatusCode)
                    {
                        List<GenreDTO> genres = await responseGetAll.Content.ReadAsAsync<List<GenreDTO>>();

                        foreach (GenreDTO x in genres)
                        {
                            Console.WriteLine("{0}:{1}", x.GenreId, x.Name);
                        }
                    }
                    else if (responseGetAll.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ZOperationResult operationResult = DeserializeZOperationResult(responseGetAll.Content.ReadAsStringAsync().Result);
                        Console.WriteLine(operationResult.Text);

                        Console.WriteLine(responseGetAll.Content.ToString());

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

                    var responseGet = await client.GetAsync("api/genre/1");
                    Console.WriteLine("Status: {0}", responseGet.StatusCode.ToString());
                    if (responseGet.IsSuccessStatusCode)
                    {
                        GenreDTO genreDTOGet = await responseGet.Content.ReadAsAsync<GenreDTO>();

                        Console.WriteLine("{0}:{1}", genreDTOGet.GenreId, genreDTOGet.Name);
                    }
                    else if (responseGet.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ZOperationResult operationResult = DeserializeZOperationResult(responseGet.Content.ReadAsStringAsync().Result);
                        Console.WriteLine(operationResult.Text);

                        Console.WriteLine(responseGet.Content.ToString());

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

                    genreDTO = new GenreDTO() { GenreId = 0, Name = "Genre REST POST" };
                    var responsePost = await client.PostAsJsonAsync("api/genre", genreDTO);
                    Console.WriteLine("Status: {0}", responsePost.StatusCode.ToString());
                    if (responsePost.IsSuccessStatusCode)
                    {
                        Uri uri = responsePost.Headers.Location;
                        genreDTO = responsePost.Content.ReadAsAsync<GenreDTO>().Result;

                        Console.WriteLine("{0}:{1}", genreDTO.GenreId, genreDTO.Name);
                    }
                    else if (responsePost.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ZOperationResult operationResult = DeserializeZOperationResult(responsePost.Content.ReadAsStringAsync().Result);
                        Console.WriteLine(operationResult.Text);

                        Console.WriteLine(responsePost.Content.ToString());

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

                    //var responsePut = await client.PutAsJsonAsync(uri.PathAndQuery.StringReplaceFirst("/", ""), genreDTO);
                    var responsePut = await client.PutAsJsonAsync("api/genre/" + genreDTO.GenreId.ToString(), genreDTO);
                    Console.WriteLine("Status: {0}", responsePut.StatusCode.ToString());
                    if (responsePut.IsSuccessStatusCode)
                    {
                        genreDTO = responsePut.Content.ReadAsAsync<GenreDTO>().Result;

                        Console.WriteLine("{0}:{1}", genreDTO.GenreId, genreDTO.Name);
                    }
                    else if (responsePut.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ZOperationResult operationResult = DeserializeZOperationResult(responsePut.Content.ReadAsStringAsync().Result);
                        Console.WriteLine(operationResult.Text);

                        Console.WriteLine(responsePut.Content.ToString());

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

                    //var responseDelete = await client.DeleteAsync(uri.PathAndQuery);
                    var responseDelete = await client.DeleteAsync("api/genre/" + genreDTO.GenreId.ToString());
                    Console.WriteLine("Status: {0}", responseDelete.StatusCode.ToString());
                    if (responseDelete.IsSuccessStatusCode)
                    {
                        Console.WriteLine("OK");
                    }
                    else if (responseDelete.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ZOperationResult operationResult =
                            DeserializeZOperationResult(responseDelete.Content.ReadAsStringAsync().Result);
                        Console.WriteLine(operationResult.Text);

                        Console.WriteLine(responseDelete.Content.ToString());

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
}