using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text;
using System.Text.Json;
using WebApplication1.Abstractions;
using WebApplication1.DTOs;

namespace WebApplication1.Filters
{
    public class RegisterFilter : Attribute, IAsyncActionFilter
    {
        private readonly ICustomFilterService _customFilterService;
        public RegisterFilter(ICustomFilterService customFilterService)
        {
            _customFilterService = customFilterService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var request = context.ActionArguments["request"] as RegisterUserRequest;
            var isRegistered = await _customFilterService.IsEmailRegistered(request.email);

            if (isRegistered) { context.Result = new StatusCodeResult(409); return; }

            await next();
        }
    }
}
