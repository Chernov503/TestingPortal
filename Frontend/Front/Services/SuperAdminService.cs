using Blazored.LocalStorage;
using Front.DTOs;
using Front.Services.Abstractions;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static MudBlazor.Colors;

namespace Front.Services
{
    public class SuperAdminService : ISudoService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly IAuth _auth;
        public SuperAdminService(HttpClient httpClient, ILocalStorageService localStorageService, IAuth auth) 
        { 
            _httpClient = httpClient;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _auth = auth;
        }

 
        public async Task<bool> DeleteUser(Guid userId)
        {
            await _auth.MarkByToken(_httpClient);
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = JsonContent.Create(userId),
                Method = HttpMethod.Delete,
                RequestUri = new Uri("/sudo", UriKind.Relative)
            };

            var response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<UserResponse>> GetUsers()
        {
            await _auth.MarkByToken(_httpClient);

            var response = await _httpClient.GetAsync("/sudo");

            var responseBody = await response.Content.ReadAsStreamAsync();

            var LoginResponse = await JsonSerializer.DeserializeAsync<List<UserResponse>>(responseBody, _jsonSerializerOptions);

            return LoginResponse;
        }

        public async Task<bool> SetAdminUser(Guid userId)
        {
            await _auth.MarkByToken(_httpClient);
            var body = new ChangeStaffStatus(userId, 2);

            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = JsonContent.Create(body),
                Method = HttpMethod.Put,
                RequestUri = new Uri("/sudo", UriKind.Relative)
            };

            var response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;

        }
    }
}
