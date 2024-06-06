using Blazored.LocalStorage;
using Front.DTOs;
using Front.Models;
using Front.Services.Abstractions;
using System.Net.Http;
using System;
using System.Net.Http.Json;
using System.Text.Json;

namespace Front.Services
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly IAuth _auth;
        public AdminService(HttpClient httpClient, ILocalStorageService localStorageService, IAuth auth)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _auth = auth;
        }

        public async Task<bool> CreateTest(CreateTestRequest requestBody)
        {
            var response = await SendRequest<CreateTestRequest>(requestBody, HttpMethod.Post, "admin/tests/create");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTest(Guid testId)
        {
            var response = await SendRequest<Guid>(testId, HttpMethod.Delete, "admin/tests");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            var response = await SendRequest<Guid>(userId, HttpMethod.Delete, "admin/users");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<TestResponse>> GetTests()
        {
            //проверить
            var response = await SendRequest(HttpMethod.Get, "admin/tests");
            //проверить
            return await response.Content.ReadFromJsonAsync<List<TestResponse>>();
        }

        public async Task<List<UserResponse>> GetUsers()
        {//ПРОВЕРИТЬ!
            var response = await SendRequest(HttpMethod.Get, "admin/users");
            //ПРОВЕРИТЬ!
            return await response.Content.ReadFromJsonAsync<List<UserResponse>>();
        }

        public async Task<bool> PutUserStatus(ChangeStaffStatus changeStaff)
        {
            var response = await SendRequest<ChangeStaffStatus>(changeStaff, HttpMethod.Put, "admin/users");

            return response.IsSuccessStatusCode;
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
