using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RESTfullDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfullDemo.Filters
{
    public class CheckAuthorExistFilterAttribute : ActionFilterAttribute
    {
        public CheckAuthorExistFilterAttribute(IRepositoryWrapper repositoryWrapper)
        {
            RepositoryWrapper = repositoryWrapper;
        }
        public IRepositoryWrapper RepositoryWrapper { get; }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authorIdParameter = context.ActionArguments.Single(m => m.Key == "authorId");
            Guid authorId = (Guid)authorIdParameter.Value;
            var isExist = await RepositoryWrapper.Author.IsExistAsync(authorId);
            if (!isExist)
            {
                context.Result = new NotFoundResult();
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
