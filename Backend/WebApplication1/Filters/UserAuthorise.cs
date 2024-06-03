using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace WebApplication1.Filters
{
    public class UserAuthorise : Attribute, IAsyncActionFilter
    {
        private readonly string StatusNeed;
        public UserAuthorise(string statusNeed)
        {
            StatusNeed = statusNeed; 
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var token = context.HttpContext.User.Identity as ClaimsIdentity;
            var claims = token.Claims;

            var statusClame = claims.SingleOrDefault(x => x.Type == "status");
            var idClame = claims.SingleOrDefault(x => x.Type == "userId");
            var companyClame = claims.SingleOrDefault(x => x.Type == "company");

            var status = statusClame.Value.ToString();
            var id = idClame.Value.ToString();
            var company = companyClame.Value.ToString();

            context.HttpContext.Request.Headers.Append("status", status);
            context.HttpContext.Request.Headers.Append("asckerId", id);
            context.HttpContext.Request.Headers.Append("company", company);

            if(String.Equals(StatusNeed, status))
            {
                await next();
            }
            else
            {
                context.Result = new ForbidResult();
            }


        }
    }
}
