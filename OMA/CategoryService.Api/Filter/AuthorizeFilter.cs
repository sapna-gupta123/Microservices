using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using SharedService;

namespace CategoryService.Api.Filter
{
    public class AuthorizeFilter : ActionFilterAttribute
    {

        public string Roles { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            HttpContext _httpContextAccessor = context.HttpContext;

            var userInfo = TokenManagerService.GetUserInfo(_httpContextAccessor);
            if (string.IsNullOrEmpty(userInfo.UserName))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new JsonResult("Pelase Send Valid Token In Request Header :| "); ;
            }
            if (!string.IsNullOrEmpty(Roles) && !string.IsNullOrEmpty(userInfo.UserName))
            {
                var AllUserrole = userInfo.Roles?.Split(",");
                if (!AllUserrole.Contains(Roles))
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult("This User Must have Role " + Roles + " :| "); ;
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
