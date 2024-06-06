

using System.Data.Common;
using System.Dynamic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Front.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using MudBlazor.Extensions;
using Blazored.LocalStorage;
using System.Reflection.Metadata.Ecma335;


namespace Front.Services.Abstractions;

public class AuthService : IAuth
{
    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient http, ILocalStorageService localStorageService)
    {
        
        _http = http;
        _localStorage = localStorageService;
        _jsonSerializerOptions = new JsonSerializerOptions{
            PropertyNameCaseInsensitive = true
        };
    }
    public async Task<(HttpStatusCode httpCode, string? token, int? status)> Login(LoginUserRequest request)
    {
        try
        {

            var message = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            
            var respone = await _http.PostAsync("/login", message);

            var httpCode = respone.StatusCode;

            var responseBody = await respone.Content.ReadAsStreamAsync();

            var loginResponse = await JsonSerializer.DeserializeAsync<LoginResponse>(responseBody, _jsonSerializerOptions);

            return (httpCode, loginResponse.token, loginResponse.status);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<HttpStatusCode> Register(RegisterUserRequest request)
    {
        try
        {
            var message = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var respone = await _http.PostAsync("/register", message);  

            return respone.StatusCode;

        }
        catch (System.Exception)
        {
            
            throw;
        }
    }


    public async Task JwtToSessionStorage(string jwt)
    {
        if (_localStorage != null)
        {
            await _localStorage.RemoveItemAsync(nameof(jwt));
            await _localStorage.SetItemAsStringAsync(nameof(jwt), jwt);
        }
    }
    
    public async Task JwtSessionDelete()
    {
        await _localStorage.RemoveItemAsync("jwt");
    }
    public async Task MarkByToken(HttpClient http)
    {
        string jwtToken = await _localStorage.GetItemAsStringAsync("jwt");

        if (!string.IsNullOrEmpty(jwtToken))
        {
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }

    }
}
