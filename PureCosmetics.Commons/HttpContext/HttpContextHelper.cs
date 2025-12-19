using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.HttpContext
{
    /// <summary>
    /// HttpContextHelper class provides utility methods for accessing information about the current HTTP context
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    public class HttpContextHelper
    {
        /// <summary>
        /// Checks if the current user is authenticated.
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        public static bool IsUserAuthenticated(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            return user != null && user.Identity != null && user.Identity.IsAuthenticated;
        }

        /// <summary>
        /// Gets the current authenticated user's ID.
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static int CurrentUserId(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            if (user != null && user.Identity != null && user.Identity.IsAuthenticated)
            {
                return int.Parse(user.FindFirst("Id")!.Value);
            }
            throw new InvalidOperationException("User is not authenticated.");
        }
    }
}
