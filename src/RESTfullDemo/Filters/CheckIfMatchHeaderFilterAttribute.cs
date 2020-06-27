using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using RESTfullDemo.Helpers;
using System.Threading.Tasks;

namespace RESTfullDemo.Filters
{
    public class CheckIfMatchHeaderFilterAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey(HeaderNames.IfMatch))
            {
                context.Result = new BadRequestObjectResult(new ApiError
                {
                    Message = "必须提供If-Match消息头"
                });
            }

            return base.OnActionExecutionAsync(context, next);
        }
    }
}
