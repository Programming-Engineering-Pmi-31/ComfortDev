using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Logic
{
    public struct ActionResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }

    public static class Actions
    {
        private const string API_ADDRESS = "http://localhost:31415";
        private static string token = null;

        public static ActionResult RegisterUser(string username, string password)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(
                    API_ADDRESS + $"/api/user/register/?username={username}&password={password}", 
                    null
                );

                return new ActionResult
                {
                    StatusCode = response.Result.StatusCode,
                    Message = GetDataDict(response.Result)["message"]
                };
            }
        }

        public static ActionResult AuthenticateUser(string username, string password)
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(
                    API_ADDRESS + $"/api/user/authenticate/?username={username}&password={password}",
                    null
                );

                var dataDict = GetDataDict(response.Result);
                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    token = dataDict["token"];
                }

                return new ActionResult
                {
                    StatusCode = response.Result.StatusCode,
                    Message = dataDict["message"]
                };
            }
        }

        static Dictionary<string, string> GetDataDict(HttpResponseMessage httpResponse)
        {
            string rawData = httpResponse.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(rawData);
        }
    }
}
