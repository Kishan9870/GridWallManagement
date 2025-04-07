using GridWallManagement.App.Database.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GridWallManagement.App.Api.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public AuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (Users)context.HttpContext.Items["User"];

            if (user == null || user.Roles == null || user.Roles.IsActive || !_roles.Contains(user.Roles.Name))
            {
                // not logged in or role not authorized
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
