using GridWallManagement.App.Database.Entities;
using GridWallManagement.App.Services.Common;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GridWallManagement.App.Api.Infrastructure.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class HasCurrentUserIdAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var arg in context.ActionArguments)
            {
                if (arg.Value is IHasCurrentUserId hasCurrentUserIdRequest)
                    hasCurrentUserIdRequest.UserId = ((Users)context.HttpContext.Items["User"]).Id;
            }
            await next.Invoke();
        }
    }
}
