using Logic.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

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

        public static async Task<IEnumerable<Topic>> GetTopics()
        {
            try
            {
                using var client = new HttpClient();
                var streamTask = client.GetStreamAsync(API_ADDRESS + "/api/topics");

                var serializer = new DataContractJsonSerializer(typeof(IEnumerable<Topic>));
                var topics = serializer.ReadObject(await streamTask) as IEnumerable<Topic>;
                return topics;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<UserCourse> CreateUserCourse(int topicId)
        {
            if (token == null)
            {
                return null;
            }

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var streamTask = client.GetStreamAsync(API_ADDRESS + $"/api/userCourses/create?topicId={topicId}");

                var jsonSerializerSettings = new DataContractJsonSerializerSettings
                {
                    DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ss.fffffffK")
                };
                var serializer = new DataContractJsonSerializer(typeof(UserCourse), jsonSerializerSettings);
                var userCourse = serializer.ReadObject(await streamTask) as UserCourse;
                return userCourse;
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
