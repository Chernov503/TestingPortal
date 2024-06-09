using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using WebApplication1.Abstractions;
using WebApplication1.DTOs;

namespace WebApplication1.Filters
{
    public class IsStaffInYourCompany : Attribute, IAsyncActionFilter
    {
        private readonly ICustomFilterService _customFilterService;

        public IsStaffInYourCompany(ICustomFilterService customFilterService)
        {
            _customFilterService = customFilterService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //TODO: Придумать защиту
            await next();
        }
    }
}
