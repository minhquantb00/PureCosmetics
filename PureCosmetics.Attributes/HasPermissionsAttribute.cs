using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PureCosmetics.Attributes
{
    public class HasPermissionsAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public string[]? Permissions { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (Permissions != null)
            {
                if (!context.HttpContext.User.Claims.Any(c => c.Type == "permission" && Permissions.Contains(c.Value)))
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }
    }
}
