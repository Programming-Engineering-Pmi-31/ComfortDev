using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Flurl.Http;
namespace Logic
{
    public static class Actions
    {
        private const string API_ADDRESS = "http://localhost:31415";
        private static string token = null;

        public static string RegisterUser(string username, string password)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(
                    API_ADDRESS + $"/api/user/register/?username={username}&password={password}", 
                    null
                );
                return response.Result.StatusCode.ToString();
            }
        }

        public static void AuthenticateUser(string username, string password)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(
                    API_ADDRESS + $"/api/user/authenticate/?username={username}&password={password}", 
                    null
                ).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<Dictionary<string, string>>(result)["token"];
            }
        }
    }
}
