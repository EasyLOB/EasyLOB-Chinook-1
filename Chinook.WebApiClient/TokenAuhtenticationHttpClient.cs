using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using EasyLOB.Library;
using EasyLOB.Library.WebApi;

namespace Chinook.WebApiClient
{
    internal partial class Program
    {
        private static void TokenAuthenticationHttpClient()
        {
            Console.WriteLine("\nWeb API Client - HttpClient - Authentication");

            using (var tokenClient = new HttpClient())
            {
                var form = new Dictionary<string, string>
                {
                    {"grant_type", "password"},
                    {"username", "administrator"},
                    {"password", "P@ssw0rd"},
                };
                var tokenResponse = tokenClient.PostAsync(WebApiUrl + "/token", new FormUrlEncodedContent(form)).Result;
                Token token = tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;
                if (String.IsNullOrEmpty(token.Error))
                {
                    Console.WriteLine("Token: {0}", token.AccessToken);
                    Console.WriteLine("Token Type: {0}", token.TokenType);

                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri(WebApiUrl);
                        httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token.AccessToken));

                        HttpResponseMessage response;

                        // Authorize

                        response = httpClient.GetAsync("api/Authorize").Result;
                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Authorization SUCCESS");
                        }
                        else
                        {
                            Console.WriteLine("Authorization FAILURE");
                        }
                        string message = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine("Authorization Response:" + message);

                        // AuthorizeActivity

                        response = httpClient.GetAsync("api/AuthorizeActivity").Result;
                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Activity Authorization SUCCESS");
                        }
                        else
                        {
                            Console.WriteLine("Activity Authorization FAILURE");
                            ZOperationResult operationResult = response.Content.ReadAsAsync<ZOperationResult>().Result;
                            Console.WriteLine(operationResult.Text);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Token Error : {0}", token.Error);
                }
            }
        }

        private static Token GetToken()
        {
            Token token = null;

            using (var client = new HttpClient())
            {
                var form = new Dictionary<string, string>
                {
                    {"grant_type", "password"},
                    {"username", "administrator"},
                    {"password", "P@ssw0rd"},
                };
                var tokenResponse = client.PostAsync(WebApiUrl + "/token", new FormUrlEncodedContent(form)).Result;
                token = tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;
                if (!String.IsNullOrEmpty(token.Error))
                {
                    throw new Exception(token.Error);
                }
            }

            return token;
        }
    }
}