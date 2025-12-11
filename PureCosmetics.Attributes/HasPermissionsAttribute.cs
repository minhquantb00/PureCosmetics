using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PureCosmetics.Attributes
{
    /// <summary>
    /// Attribute to check if the user has the required permissions.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    public class HasPermissionsAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// Permissions required to access the resource.
        /// </summary>
        public string[]? Permissions { get; set; }

        /// <summary>
        /// Authorization logic to check if the user has the required permissions.
        /// </summary>
        /// <param name="context"></param>
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
