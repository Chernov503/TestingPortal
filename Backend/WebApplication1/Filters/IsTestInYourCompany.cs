using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Abstractions;

namespace WebApplication1.Filters
{
    public class IsTestInYourCompany : Attribute, IAsyncActionFilter
    {
        private readonly ICustomFilterService _customFilterService;

        public IsTestInYourCompany(ICustomFilterService customFilterService)
        {
            _customFilterService = customFilterService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var testId = Guid.Parse(context.ActionArguments["testId"].ToString());

            var asckerCompany = context.HttpContext.Request.Headers["company"];
            var testCompany = await _customFilterService.GetTestCompanyOwner(testId);

            if (!String.Equals(asckerCompany, testCompany))
            { context.Result = new ForbidResult(); return; }

            await next();
        }
    }
}
