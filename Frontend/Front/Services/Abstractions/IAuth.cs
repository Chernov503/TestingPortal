
using Front.DTOs;
using Microsoft.JSInterop;
using System.Net;

namespace Front.Services.Abstractions;
public interface IAuth
{
    Task<HttpStatusCode> Register(RegisterUserRequest request);
    public Task<(HttpStatusCode httpCode, string? token, int? status)> Login(LoginUserRequest request);
    public Task JwtToSessionStorage(string jwt);
    public Task MarkByToken(HttpClient http);
    public Task JwtSessionDelete();
}