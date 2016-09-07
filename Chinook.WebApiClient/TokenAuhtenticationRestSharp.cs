using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using EasyLOB.Library.WebApi;

namespace Chinook.WebApiClient
{
    internal partial class Program
    {
        private static void TokenAuthenticationRestSharp()
        {
            Console.WriteLine("\nWeb API Client - RestSharp - Authentication");

            RestClient client = new RestClient(WebApiUrl);

            Token token = new Token();

            var requestPost = new RestRequest("token", Method.POST);
            requestPost.RequestFormat = DataFormat.Json;
            requestPost.AddParameter("grant_type", "password");
            requestPost.AddParameter("username", "administrator");
            requestPost.AddParameter("password", "P@ssw0rd");

            var responsePost = client.Execute(requestPost);
            Console.WriteLine("Status: {0}", responsePost.StatusCode.ToString());
            if (responsePost.StatusCode == HttpStatusCode.OK)
            {
                token = JsonConvert.DeserializeObject<Token>(responsePost.Content);

                Console.WriteLine("Token: {0}", token.AccessToken);
                Console.WriteLine("Token Type: {0}", token.TokenType);

                var requestGet = new RestRequest("api/Authorize", Method.GET);
                requestGet.AddHeader("Authorization", String.Format("Bearer {0}", token.AccessToken));

                var responseGet = client.Execute(requestGet);
                Console.WriteLine("Status: {0}", responseGet.StatusCode.ToString());
                if (responseGet.StatusCode == HttpStatusCode.OK)
                {
                    string message = JsonConvert.DeserializeObject<string>(responseGet.Content);

                    Console.WriteLine("Authorization SUCCESS");
                    Console.WriteLine("Authorization Response:" + message);
                }
                else
                {
                    Console.WriteLine("Authorization FAILURE");
                }
            }
            else
            {
                Console.WriteLine("Status Code: " + responsePost.StatusCode.ToString());
                Console.WriteLine("Content: " + responsePost.Content);
            }
        }
    }
}