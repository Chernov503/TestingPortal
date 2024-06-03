using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Abstractions;

namespace WebApplication1.Filters
{
    public class SimilarCompanyFilter : Attribute, IAsyncActionFilter
    {
        private readonly ICustomFilterService _customFilterService;
        public SimilarCompanyFilter(ICustomFilterService customFilterService)
        {
            _customFilterService = customFilterService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var asckerId = Guid.Parse(context.ActionArguments["id"].ToString());
            var userId = Guid.Parse(context.ActionArguments["staffId"].ToString());
            var isCompaniesSimilar = await _customFilterService.IsCompanySimilars(asckerId, userId);

            if (isCompaniesSimilar) { await next(); }
            else { context.Result = new NotFoundObjectResult("Staff not found"); return; }
        }
    }
}
