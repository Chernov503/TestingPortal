using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using WebApplication1.Abstractions;

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
            
            var userId = Guid.Parse( context.ActionArguments["userId"].ToString() );

            var asckerCompany = context.HttpContext.Request.Headers["company"];
            var userCompany = await _customFilterService.GetUserCompanyName(userId);

            if (!String.Equals(asckerCompany, userCompany))
            { context.Result = new ForbidResult(); return; }

            await next();
        }
    }
}
