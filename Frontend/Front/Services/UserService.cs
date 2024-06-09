using Front.DTOs;
using Front.Services.Abstractions;
using System.Net.Http.Json;
using System.Net.Http;
using Blazored.LocalStorage;
using System.Text.Json;
using Front.DTOs.TestDoing;
using Front.Models;
using Front.DTOs.TestResult;

namespace Front.Services
{
    public class UserService : IUserService
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly IAuth _auth;
        public UserService(HttpClient httpClient, ILocalStorageService localStorageService, IAuth auth)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _auth = auth;
        }
        public async Task<TestStatisticResponse> GetResultById(Guid resultId)
        {
            var response = await SendRequest<Guid>(resultId, HttpMethod.Put, "user/results/result");

            var result = await response.Content.ReadFromJsonAsync<TestStatisticResponse>();

            return result;
        }

        public async Task<ToDoTestResultRequest> GetTestById(Guid testId)
        {
            var response = await SendRequest<string>(testId.ToString(), HttpMethod.Put, "user/tests/test");

            return await response.Content.ReadFromJsonAsync<ToDoTestResultRequest>();
        }

        public async Task<List<TestResponse>> GetTests()
        {
            //проверить
            var response = await SendRequest(HttpMethod.Get, "user/tests");
            //проверить
            return await response.Content.ReadFromJsonAsync<List<TestResponse>>();
        }
        public async Task<bool> PostTestResalt(PutTestResultRequest requestBody)
        {
            var response = await SendRequest(requestBody, HttpMethod.Post, "user/tests/test");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<TestResultResponse>> GetResults(DateRange dateRange)
        {
            var response = await SendRequest<DateRange>(dateRange, HttpMethod.Put, "user/results");

            return await response.Content.ReadFromJsonAsync<List<TestResultResponse>>();
        }


        public async Task<HttpResponseMessage> SendRequest<K>(K requestBody,
                                                      HttpMethod httpMethod,
                                                      string uri)
        {
            await _auth.MarkByToken(_httpClient);
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = JsonContent.Create(requestBody),
                Method = httpMethod,
                RequestUri = new Uri(uri, UriKind.Relative)
            };

            var response = await _httpClient.SendAsync(request);

            return response;
        }
        public async Task<HttpResponseMessage> SendRequest(HttpMethod httpMethod,
                                                           string uri)
        {
            await _auth.MarkByToken(_httpClient);
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = httpMethod,
                RequestUri = new Uri(uri, UriKind.Relative)
            };

            var response = await _httpClient.SendAsync(request);

            return response;
        }

    }
}
