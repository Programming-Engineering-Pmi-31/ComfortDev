﻿using Logic.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Logic
{
    // якщо б ти ше шось коментував було б простіше
    public struct ActionResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }

    public static class Actions
    {
        private const string API_ADDRESS = "http://localhost:31415";
        private static string token = null;

        public static async Task<ActionResult> RegisterUser(string username, string password)
        {
            try
            {
                using var client = new HttpClient();

                var url = API_ADDRESS + $"/api/user/register/?username={username}&password={password}";
                using var response = await client.PostAsync(url, null);

                return new ActionResult
                {
                    StatusCode = response.StatusCode,
                    Message = GetDataDict(response)?["message"]
                };
            }
            catch (Exception ex)
            {
                return new ActionResult
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public static async Task<ActionResult> AuthenticateUser(string username, string password)
        {
            try
            {
                using var client = new HttpClient();

                var url = API_ADDRESS + $"/api/user/authenticate/?username={username}&password={password}";
                using var response = await client.PostAsync(url, null);

                var dataDict = GetDataDict(response);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    token = dataDict["token"];
                }

                return new ActionResult
                {
                    StatusCode = response.StatusCode,
                    Message = dataDict?.GetValueOrDefault("message")
                };
            }
            catch (Exception ex)
            {
                return new ActionResult
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public static async Task<IEnumerable<Question>> GetTestQuestions()
        {
            try
            {
                using var client = new HttpClient();
                var streamTask = client.GetStreamAsync(API_ADDRESS + "/api/test");

                var serializer = new DataContractJsonSerializer(typeof(IEnumerable<Question>));
                var questions = serializer.ReadObject(await streamTask) as IEnumerable<Question>;
                return questions;
            }
            catch
            {
                return null;
            }
        }

        private static Dictionary<string, string> GetDataDict(HttpResponseMessage httpResponse)
        {
            string rawData = httpResponse.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(rawData);
        }
    }
}
