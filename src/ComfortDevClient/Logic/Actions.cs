using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Logic
{
    public static class Actions
    {
        private const string API_ADDRESS = "http://localhost:31415/api";
        private static string token = null;

        public static string RegisterUser(string username, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("Password", password)
            };
            var content = new FormUrlEncodedContent(pairs);

            using (var client = new HttpClient())
            {
                var response = client.PostAsync(API_ADDRESS + "/user/register", content).Result;
                return response.StatusCode.ToString();
            }
        }

        public static void AuthenticateUser(string username, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("Password", password)
            };
            var content = new FormUrlEncodedContent(pairs);

            using (var client = new HttpClient())
            {
                var response = client.PostAsync(API_ADDRESS + "/user/authenticate", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<Dictionary<string, string>>(result)["token"];
            }
        }
    }
}
